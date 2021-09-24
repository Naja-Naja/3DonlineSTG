using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPunCallbacks
{
	public float speed = 10.0f;
    public Rigidbody rb;
    public GameObject boost;
    bool isboost = false;
    public float LimitSpeed;
    Quaternion horizontalrotation;
    Vector3 velocity;
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            //���͂��󂯎��velocity���쐬
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            horizontalrotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            velocity = horizontalrotation * new Vector3(x, 0, z).normalized * speed;

            //Addforce�ŗ͂������Ĉړ�������
            //���Ȃ苭���������o�邪�d���������Ă����������̂ł���ł���
            if (isboost == true)
            {
                //�u�[�X�g����̈ړ�
                rb.AddForce(velocity * 3);
            }
            else
            {
                //�u�[�X�g�Ȃ��̈ړ�
                rb.AddForce(velocity*1.5f);
            }

            //�ō����x����
            if (rb.velocity.magnitude > LimitSpeed)
            {
                rb.velocity = rb.velocity.normalized * LimitSpeed;
            }
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            //����VFX��using����OnPlay��OnStop�g������������
            if (Input.GetKey(KeyCode.Space) == true)
            {
                //�u�[�X�g�̃G�t�F�N�g����͕����ɑΉ������ăA�N�e�B�u��
                isboost = true;
                boost.SetActive(true);
                boost.transform.rotation = Quaternion.LookRotation(horizontalrotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
                boost.transform.Rotate(new Vector3(1, 0, 0), -90);
            }
            else
            {
                isboost = false;
                boost.SetActive(false);
            }
        }
    }
}
