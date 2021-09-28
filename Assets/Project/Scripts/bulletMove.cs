using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    Rigidbody Rigidbody;
    Vector3 target;
    private int playernum = 0;
    public float damage = 50;
    float lifetime = 0;
    [SerializeField] weaponflame weaponDB;
    //生成時に渡される発射方向
    public void SetTargetPosition(Vector3 vector3, int playernumber,int ID)
    {
        //各種弾のステータスを設定
        damage = weaponDB.Equipment[ID].damage;
        //lifetime = weaponDB.Equipment[ID].lifetime;
        Invoke("Lifelimit", weaponDB.Equipment[ID].lifetime);

        playernum = playernumber;

        target = vector3;
        //AddForceでtargetに向かって飛ぶ

        //弾の初速を設定
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddForce(target.normalized * weaponDB.Equipment[ID].speed);
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
        //着弾のエフェクトはなんかバグの温床なので一旦オミット
        //var tmp = Instantiate(fire, this.transform);
        //fire.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        //fireEffect.SendEvent("OnPlay");
        //tmp.transform.SetParent(effectmother.transform);//ここでなんかエラーでるけどなんで？
        Destroy(this.gameObject);
    }
    private void Lifelimit()
    {
        Destroy(this.gameObject);
    }
}
