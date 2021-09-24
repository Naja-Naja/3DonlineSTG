using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretMove : MonoBehaviourPunCallbacks
{
    void Update()
    {
        //砲台がカメラの向いてる方向を向く
        if (photonView.IsMine)
        {
            this.transform.LookAt(Camera.main.transform);
        }
    }
}
