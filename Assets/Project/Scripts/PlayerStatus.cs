using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.VFX;

public class PlayerStatus : MonoBehaviourPunCallbacks
{
    [SerializeField] FloatVariable HP;
    [SerializeField] PlayerEN EN;
    void Start()
    {
        if (photonView.IsMine)
        {
            HP.init();
            EN.init();
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            if (EN.RuntimeValue <= 0)
            {
                EN.RuntimeValue = 0;
                EN.OverHeat = true;
                Debug.Log("overheat");
            }
            else if (EN.RuntimeValue >= EN.Value)
            {
                EN.OverHeat = false;
                EN.RuntimeValue = EN.Value;
            }
        }
    }
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            if (EN.OverHeat)
            {
                EN.RuntimeValue = EN.RuntimeValue + 2;
            }
            //ÉGÉlÉãÉMÅ[âÒïú
            else if (EN.RuntimeValue < EN.Value)
            {
                EN.RuntimeValue = EN.RuntimeValue + 1;
            }
        }
    }
}
