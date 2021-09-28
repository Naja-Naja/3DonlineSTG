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
        //AddForce��target�Ɍ������Ĕ��
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddForce(target.normalized * 2500);

        effectmother = GameObject.Find("bulletmother");
    }
    //�������ɓn����锭�˕���
    public void SetTargetPosition(Vector3 vector3, int playernumber)
    {
        target = vector3;
        playernum = playernumber;
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
        var tmp = Instantiate(fire, this.transform);
        tmp.transform.SetParent(effectmother.transform);
        Destroy(this.gameObject);
    }
}
