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
        //AddForceでtargetに向かって飛ぶ
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddForce(target.normalized * 2500);
    }

    //生成時に渡される発射方向
    public void SetTargetPosition(Vector3 vector3)
    {
        target = vector3;
    }

    //着弾時に爆発して消える
    private void OnTriggerEnter(Collider other)
    {
        var tmp = Instantiate(fire, this.transform);
        tmp.transform.SetParent(transform.root);
        Destroy(this.gameObject);
    }
}
