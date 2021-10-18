using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;

public class shoot : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject cameraobj;
    [SerializeField] VisualEffect fire;
    [SerializeField] weaponflame weaponflame;
    [SerializeField] GameObject cube;
    int playernum;
    void Start()
    {
        cube = GameObject.Find("Cube");
        if (photonView.IsMine)
        {
            for (int i = 0; i < weaponflame.Equipment.Count; i++)
            {
                weaponflame.Equipment[i].init();
            }
            fire.Stop();
            playernum = photonView.OwnerActorNr;
        }
    }
    private void Update()
    {
        //�U���n�̓��͂�����
        if (photonView.IsMine)
        {
            //�N���b�N�����������̏���
            if (Input.GetMouseButton(0))
            {
                //�z�[�~���O����
                if (weaponflame.Equipment[weaponflame.choiceWeapon].Homing == true)
                {
                    Debug.Log("call2");
                    LockOnTarget();
                }
                //���i����
                else
                {
                    StraightShot();
                }

            }
            //�N���b�N�𗣂����������������̏���
            else if (Input.GetMouseButtonUp(0))
            {
                //�z�[�~���O����
                if (weaponflame.Equipment[weaponflame.choiceWeapon].Homing == true)
                {
                    HomingShot();
                }
            }

            //�}�E�X�z�C�[�����͂ɂ�镐���ύX
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                weaponflame.weaponchange(1);
                Debug.Log("down");
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                weaponflame.weaponchange(-1);
                Debug.Log("up");
            }
        }
    }

    //�J�����̒��S�̍��W���\���Ă��镐��̒e�ɓn��
    void StraightShot()
    {
        //�����ˌ����̏���
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity/*,layerMask*/);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5, false);
        //Debug.Log(hit.collider.gameObject);

        //�v���C���[����Ray�̓����������W�ւ�Vector3���쐬
        Vector3 playerVec = this.transform.position;
        Vector3 targetVec = hit.point;
        Vector3 bulletVec = targetVec - playerVec;
        if (weaponflame.Equipment[weaponflame.choiceWeapon].Shot() == null) { return; }
        //RPC��p���ăv���C���[�ԂŔ��˃^�C�~���O�ƕ����𓯊�����
        photonView.RPC(nameof(shotRPC), RpcTarget.All, bulletVec, playernum, weaponflame.choiceWeapon);
    }

    //�ˌ������̊֐�
    [PunRPC]
    private void shotRPC(Vector3 target, int playernumber, int weaponID)
    {
        //�e�̔��ˏ���
        var bulletobj = Instantiate(weaponflame.Equipment[weaponID].bullet, this.transform);
        bulletobj.GetComponent<bulletMove>().SetTargetPosition(target, playernumber, weaponID);
        //bulletobj.SetActive(true);
        bulletobj.transform.Rotate(new Vector3(1, 0, 0), 90);//TODO����܂��܂������ĂȂ��C������
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");

    }

    //�J�����̒��S�ɍł��߂��A�T�[�N���͈͓��̃I�u�W�F�N�g��playernum��n��
    void LockOnTarget()
    {
        GameObject[] tmp2 = GameObject.FindGameObjectsWithTag("Player");
        RaycastHit Hit;
        Vector3 TargetPos;
        weapon weapon = weaponflame.Equipment[weaponflame.choiceWeapon];
        float objdistance = 30000;
        Transform tmptarget = null;
        Debug.Log("call");
        //���b�N�I���������͂Ȃɂ����Ȃ�
        if (weapon.readyNextBullet) { return; }

        //���łɃ��b�N�I����
        if (weapon.lockOnTarget != null)
        {
            //���C���΂�
            if (Physics.Raycast(Camera.main.transform.position, weapon.lockOnTarget.position - Camera.main.transform.position, out Hit, (weapon.lockOnTarget.position - Camera.main.transform.position).magnitude, LayerMask.GetMask("Field")) == false)
            {
                //�T�[�N����������
                TargetPos = RectTransformUtility.WorldToScreenPoint(Camera.main, weapon.lockOnTarget.position);
                TargetPos.x = TargetPos.x - (Screen.width / 2);
                TargetPos.y = TargetPos.y - (Screen.height / 2);

                //�T�[�N�����Ȃ珈�����p���B�T�[�N���O�Ȃ�L�����Z��
                if (TargetPos.magnitude < 300)
                {
                    weaponflame.Equipment[weaponflame.choiceWeapon].Targetting(weapon.lockOnTarget);
                    Debug.Log("Targetting...");
                    return;
                }
                else
                {
                    weaponflame.Equipment[weaponflame.choiceWeapon].Targetting();
                }
            }

        }

        //���b�N�I�����ł͂Ȃ����A�V�������b�N�I���^�[�Q�b�g��T��
        //��Q�����Ȃ��A���S�ɍł��߂��v���C���[�^�O�̂����I�u�W�F�N�g�����߂�
        for (int i = 0; i < tmp2.Length; i++)
        {
            //�Օ����Ȃ�������
            if (Physics.Raycast(Camera.main.transform.position, tmp2[i].transform.position - Camera.main.transform.position, out Hit, (tmp2[i].transform.position - Camera.main.transform.position).magnitude, LayerMask.GetMask("Field")) == false)
            {
                //�����ł͂Ȃ����Ƃ��m�F
                if (tmp2[i].GetComponent<PlayerStatus>().playernum == this.playernum) { }
                //�J���������O���ɂ��邱�Ƃ��m�F
                else if (Camera.main.transform.InverseTransformPoint(tmp2[i].transform.position).z < 0) { }
                //���S����̋��������߂�
                else
                {
                    TargetPos = RectTransformUtility.WorldToScreenPoint(Camera.main, tmp2[i].transform.position);
                    TargetPos.x = TargetPos.x - (Screen.width / 2);
                    TargetPos.y = TargetPos.y - (Screen.height / 2);
                    //�ł����S�ɋ߂��I�u�W�F�N�g��ێ�
                    if (TargetPos.magnitude < objdistance)
                    {
                        objdistance = TargetPos.magnitude;
                        tmptarget = tmp2[i].transform;
                    }
                }
            }
        }
        //�^�[�Q�b�g�Ɖ�ʂ̒��S�̋��������ȉ��ł���΃��b�N�I��
        if (objdistance < 300)
        {
            weaponflame.Equipment[weaponflame.choiceWeapon].Targetting(tmptarget);
            Debug.Log("Targetting...");
            return;
        }
        //���݂��Ȃ���΃��b�N�I�����邱�Ƃ͂Ȃ�
        else
        {
            weaponflame.Equipment[weaponflame.choiceWeapon].Targetting();
        }
    }

    void HomingShot()
    {
        //�z�[�~���O�e�����Ă�Ȃ甭��
        if (weaponflame.Equipment[weaponflame.choiceWeapon].readyNextBullet == true)
        {
            var targetnum = weaponflame.Equipment[weaponflame.choiceWeapon].lockOnTarget.gameObject.GetComponent<PlayerStatus>().playernum;
            Debug.Log("HomingShot target is " + targetnum);
            photonView.RPC(nameof(HomingshotRPC), RpcTarget.All, targetnum, playernum, weaponflame.choiceWeapon);
            weaponflame.Equipment[weaponflame.choiceWeapon].readyNextBullet = false;
        }
        //���ĂȂ���΃��b�N�I��������
        weaponflame.Equipment[weaponflame.choiceWeapon].StopTargeting();
    }


    [PunRPC]
    private void HomingshotRPC(int targetnumber, int playernumber, int weaponID)
    {
        //�e�̔��ˏ���
        var bulletobj = Instantiate(weaponflame.Equipment[weaponID].bullet, this.transform);
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");
        Debug.Log("call homingshot");
        bulletobj.GetComponent<MissileMove>().SetTargetPosition(targetnumber, playernumber, weaponID);
    }
}
