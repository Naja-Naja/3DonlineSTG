using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    Rigidbody Rigidbody;
    Vector3 target;
    GameObject effectmother;
    [SerializeField] GameObject fire;
    private int playernum = 0;
    public float damage = 50;
    // Start is called before the first frame update
    void Start()
    {
        //AddForceでtargetに向かって飛ぶ
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddForce(target.normalized * 2500);

        effectmother = GameObject.Find("bulletmother");
    }
    //生成時に渡される発射方向
    public void SetTargetPosition(Vector3 vector3, int playernumber)
    {
        target = vector3;
        playernum = playernumber;
    }

    //着弾時に爆発して消える
    private void OnTriggerEnter(Collider obj)
    {
        //相手がダメージを受けるインターフェースを備えていればダメージを与えて消える
        var idamage = obj.GetComponent<Idamage>();
        if (idamage != null)
        {
            if (playernum == idamage.GetPlayerNum()) { }
            else
            {
                idamage.AddDamage(damage);
            }
        }
        var tmp = Instantiate(fire, this.transform);
        tmp.transform.SetParent(effectmother.transform);
        Destroy(this.gameObject);
    }
}
