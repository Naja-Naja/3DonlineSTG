using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    Rigidbody Rigidbody;
    Vector3 target;
    [SerializeField] GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        //AddForce��target�Ɍ������Ĕ��
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddForce(target.normalized * 2500);
    }

    //�������ɓn����锭�˕���
    public void SetTargetPosition(Vector3 vector3)
    {
        target = vector3;
    }

    //���e���ɔ������ď�����
    private void OnTriggerEnter(Collider other)
    {
        var tmp = Instantiate(fire, this.transform);
        tmp.transform.SetParent(transform.root);
        Destroy(this.gameObject);
    }
}
