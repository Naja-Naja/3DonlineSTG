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
    void Start()
    {
        if (photonView.IsMine)
        {     
            StartCoroutine("bulletshoot");
            fire.Stop();
        }
    }
    IEnumerator bulletshoot()
    {
        while (true)
        {
            //�J�����̒��S���牜�ւ̃��C���쐬���Ĕ���
            //TODO raycast�������ɂ��������Ă���ۂ����炻�̂������C���[�t���܂��傤
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

            //RPC��p���ăv���C���[�ԂŔ��˃^�C�~���O�ƕ����𓯊�����
            photonView.RPC(nameof(shot), RpcTarget.All, bulletVec);
            //���ˊԊu�̎��Ԃ����ҋ@����
            yield return new WaitForSeconds(1f);
        }
        
    }

    //�ˌ������̊֐�
    [PunRPC]
    private void shot(Vector3 target)
    {
        //�e�̔��ˏ���
        var bulletobj = Instantiate(bullet, this.transform);
        bulletobj.transform.Rotate(new Vector3(1, 0, 0), 90);//TODO����܂��܂������ĂȂ��C������
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");
        bulletobj.GetComponent<bulletMove>().SetTargetPosition(target);
    }
}
