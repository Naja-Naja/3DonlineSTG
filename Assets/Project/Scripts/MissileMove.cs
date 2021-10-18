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
    //�������ɓn����锭�˕���
    public void SetTargetPosition(int targetnum, int playernumber, int ID)
    {
        //�e��e�̃X�e�[�^�X��ݒ�
        damage = weaponDB.Equipment[ID].damage;
        //lifetime = weaponDB.Equipment[ID].lifetime;
        Destroy(this.gameObject, weaponDB.Equipment[ID].lifetime);
        //Invoke("Lifelimit", weaponDB.Equipment[ID].lifetime);

        playernum = playernumber;

        //AddForce��target�Ɍ������Ĕ��

        //�e�̏�����ݒ�
        Rigidbody = this.GetComponent<Rigidbody>();
    }

    //���e���ɔ������ď�����
    private void OnTriggerEnter(Collider obj)
    {
        //���肪�_���[�W���󂯂�C���^�[�t�F�[�X������Ă���΃_���[�W��^���ď�����
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


        //�ǂɂԂ����Ēe�������鏈��������

    }
}
