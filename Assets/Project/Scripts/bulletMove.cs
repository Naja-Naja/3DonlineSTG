using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class bulletMove : MonoBehaviour
{
    Rigidbody Rigidbody;
    Vector3 target;
    GameObject effectmother;
    [SerializeField] GameObject fire;
    [SerializeField] VisualEffect fireEffect;
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
        //���e�̃G�t�F�N�g�͂Ȃ񂩃o�O�̉����Ȃ̂ň�U�I�~�b�g
        //var tmp = Instantiate(fire, this.transform);
        //fire.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        //fireEffect.SendEvent("OnPlay");
        //tmp.transform.SetParent(effectmother.transform);//�����łȂ񂩃G���[�ł邯�ǂȂ�ŁH
        Destroy(this.gameObject);
    }
}
