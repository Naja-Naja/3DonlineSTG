using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretMove : MonoBehaviourPunCallbacks
{
    void Update()
    {
        //�C�䂪�J�����̌����Ă����������
        if (photonView.IsMine)
        {
            this.transform.LookAt(Camera.main.transform);
        }
    }
}
