using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.VFX;

public class PlayerDamage : MonoBehaviourPunCallbacks, Idamage
{
    [SerializeField] FloatVariable HP;
    int playernum;
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject hitobj;
    private VisualEffect hit = null;
    void Start()
    {
        //hitobj.SetActive(true);
        //hit.SendMessage("OnStop");
        playernum = PhotonNetwork.LocalPlayer.ActorNumber;
        //hit = hitobj.GetComponent<VisualEffect>();
    }
    public void AddDamage(float damage)
    {
        HP.RuntimeValue = HP.RuntimeValue - damage;
        photonView.RPC(nameof(Hit), RpcTarget.All);
        if (HP.RuntimeValue <= 0)
        {
            HP.RuntimeValue = 0;
            photonView.RPC(nameof(Death), RpcTarget.All);
        }
    }
    [PunRPC]
    private void Death()
    {
        bomb.SetActive(true);
        //Destroy(this.gameObject.GetComponent<PlayerMove>());

    }
    [PunRPC]
    private void Hit()
    {
        if (hit == null) { hit = hitobj.GetComponent<VisualEffect>(); }
        hit.Play();
        Debug.Log("call");
        
        //Destroy(this.gameObject.GetComponent<PlayerMove>());

    }
    public int GetPlayerNum()
    {
        return playernum;
    }
}
