using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class testdamage : MonoBehaviour
{
    float damage = 5;
    [SerializeField] VisualEffect hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider obj)
    {
        Debug.Log("call");
        //���肪�_���[�W���󂯂�C���^�[�t�F�[�X������Ă���΃_���[�W��^���ď�����
        var idamage = obj.GetComponent<Idamage>();
        if (idamage != null)
        {
                idamage.AddDamage(damage);
        }
        hit.Play();
        //Destroy(this.gameObject);
    }
}
