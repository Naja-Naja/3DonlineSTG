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
    //�������ɓn����锭�˕���
    public void SetTargetPosition(Vector3 vector3, int playernumber,int ID)
    {
        //�e��e�̃X�e�[�^�X��ݒ�
        damage = weaponDB.Equipment[ID].damage;
        //lifetime = weaponDB.Equipment[ID].lifetime;
        Invoke("Lifelimit", weaponDB.Equipment[ID].lifetime);

        playernum = playernumber;

        target = vector3;
        //AddForce��target�Ɍ������Ĕ��

        //�e�̏�����ݒ�
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddForce(target.normalized * weaponDB.Equipment[ID].speed);
    }

    //���e���ɔ������ď�����
    private void OnTriggerEnter(Collider obj)
    {
        //���肪�_���[�W���󂯂�C���^�[�t�F�[�X������Ă���΃_���[�W��^���ď�����
        var idamage = obj.GetComponent<Idamage>();
        if (idamage != null)
        {
            if (playernum == idamage.GetPlayerNum()) { }
            else
            {
                idamage.AddDamage(damage);
            }
        }
        //���e�̃G�t�F�N�g�͂Ȃ񂩃o�O�̉����Ȃ̂ň�U�I�~�b�g
        //var tmp = Instantiate(fire, this.transform);
        //fire.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        //fireEffect.SendEvent("OnPlay");
        //tmp.transform.SetParent(effectmother.transform);//�����łȂ񂩃G���[�ł邯�ǂȂ�ŁH
        Destroy(this.gameObject);
    }
    private void Lifelimit()
    {
        Destroy(this.gameObject);
    }
}
