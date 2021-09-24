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
            //入力を受け取りvelocityを作成
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            horizontalrotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            velocity = horizontalrotation * new Vector3(x, 0, z).normalized * speed;

            //Addforceで力を加えて移動させる
            //かなり強い慣性が出るが重厚感あってかっこいいのでこれでいい
            if (isboost == true)
            {
                //ブーストありの移動
                rb.AddForce(velocity * 3);
            }
            else
            {
                //ブーストなしの移動
                rb.AddForce(velocity*1.5f);
            }

            //最高速度制限
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
            //多分VFXをusingしてOnPlayとOnStop使った方がいい
            if (Input.GetKey(KeyCode.Space) == true)
            {
                //ブーストのエフェクトを入力方向に対応させてアクティブ化
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
