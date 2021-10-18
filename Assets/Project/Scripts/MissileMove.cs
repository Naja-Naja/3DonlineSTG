using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    Rigidbody Rigidbody;
    private int playernum = 0;
    public float damage = 50;
    //float lifetime = 0;
    [SerializeField] weaponflame weaponDB;
    int target = 0;
    //生成時に渡される発射方向
    public void SetTargetPosition(int targetnum, int playernumber, int ID)
    {
        //各種弾のステータスを設定
        damage = weaponDB.Equipment[ID].damage;
        //lifetime = weaponDB.Equipment[ID].lifetime;
        Destroy(this.gameObject, weaponDB.Equipment[ID].lifetime);
        //Invoke("Lifelimit", weaponDB.Equipment[ID].lifetime);

        playernum = playernumber;

        //AddForceでtargetに向かって飛ぶ

        //弾の初速を設定
        Rigidbody = this.GetComponent<Rigidbody>();
    }

    //着弾時に爆発して消える
    private void OnTriggerEnter(Collider obj)
    {
        //相手がダメージを受けるインターフェースを備えていればダメージを与えて消える
        var idamage = obj.GetComponent<Idamage>();
        if (idamage != null)
        {
            if (playernum == idamage.GetPlayerNum()) { return; }
            else
            {
                idamage.AddDamage(damage);
                Destroy(this.gameObject);
            }
        }


        //壁にぶつかって弾が消える処理を書く

    }
}
