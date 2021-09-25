using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] FloatVariable ScriptableHP;
    // Start is called before the first frame update
    void Start()
    {
        ScriptableHP.init();
    }

    //private void FixedUpdate()
    //{
    //    GetDamage(1);
    //}

    void GetDamage(float damage)
    {
        ScriptableHP.RuntimeValue = ScriptableHP.RuntimeValue - damage;
        if (ScriptableHP.RuntimeValue < 0)
        {
            ScriptableHP.RuntimeValue = 0;
        }
    }
}
