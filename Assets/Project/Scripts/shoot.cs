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
    [SerializeField] weapon weapon;//������weaponflame�ɂ���
    [SerializeField] weaponflame weaponflame;
    //[SerializeField] float scroll;
    private int playernum;
    void Start()
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < weaponflame.Equipment.Count; i++)
            {
                weaponflame.Equipment[i].init();
            }
            fire.Stop();
            playernum = PhotonNetwork.LocalPlayer.ActorNumber;
        }
    }
    private void Update()
    {
        //�U���n�̓��͂�����
        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
                Ray ray = Camera.main.ScreenPointToRay(center);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Mathf.Infinity);
                //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5, false);

                //�v���C���[����Ray�̓����������W�ւ�Vector3���쐬
                Vector3 playerVec = this.transform.position;
                Vector3 targetVec = hit.point;
                Vector3 bulletVec = targetVec - playerVec;
                //Debug.Log("playerVec=" + playerVec + "hit.point=" + hit.point);
                //if (weapon.Shot() == null) { return; }
                if (weaponflame.Equipment[weaponflame.choiceWeapon].Shot() == null) { return; }
                //RPC��p���ăv���C���[�ԂŔ��˃^�C�~���O�ƕ����𓯊�����
                photonView.RPC(nameof(shot), RpcTarget.All, bulletVec, playernum,weaponflame.choiceWeapon);
            }
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weaponflame.weaponchange(-1);
            Debug.Log("down");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            weaponflame.weaponchange(1);
            Debug.Log("up");
        }
    }
    //�ˌ������̊֐�
    [PunRPC]
    private void shot(Vector3 target, int playernumber, int weaponID)
    {
        //�e�̔��ˏ���
        var bulletobj = Instantiate(weaponflame.Equipment[weaponID].bullet, this.transform);
        bulletobj.transform.Rotate(new Vector3(1, 0, 0), 90);//TODO����܂��܂������ĂȂ��C������
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");
        bulletobj.GetComponent<bulletMove>().SetTargetPosition(target, playernumber, weaponID);
    }
}
