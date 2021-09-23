using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectDestroy : MonoBehaviour
{
    void Start()
    {
        Invoke("destroyself", 5f);   
    }

    void destroyself()
    {
        Destroy(this.gameObject);
    }
}
