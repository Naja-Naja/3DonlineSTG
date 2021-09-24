using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretMove : MonoBehaviourPunCallbacks
{
    void Update()
    {
        //–C‘ä‚ªƒJƒƒ‰‚ÌŒü‚¢‚Ä‚é•ûŒü‚ğŒü‚­
        if (photonView.IsMine)
        {
            this.transform.LookAt(Camera.main.transform);
        }
    }
}
