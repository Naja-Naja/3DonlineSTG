using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
	public float speed = 10.0f;
    public Rigidbody rb;
    public GameObject boost;
    bool isboost = false;
    public float LimitSpeed;
    Quaternion horizontalrotation;
    Vector3 velocity;

    void Start()
    {
        var camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        camera.Follow = this.transform;
        camera.LookAt = this.transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        horizontalrotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        velocity = horizontalrotation * new Vector3(x, 0, z).normalized*speed;
        
        if (isboost==true)
        {
            rb.AddForce(velocity*2);

            //rb.AddForce(x, 0, z+10);
        }
        else
        {
            rb.AddForce(velocity);
            //rb.AddForce(x, 0, z);
        }
        if(rb.velocity.magnitude > LimitSpeed)
        {
            rb.velocity = rb.velocity.normalized * LimitSpeed;
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            
            //Debug.Log("push");
            isboost = true;
            boost.SetActive(true);

            boost.transform.rotation=Quaternion.LookRotation(horizontalrotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
           // //boost.transform.rotation = Quaternion.LookRotation(velocity,Vector3.up);
           // //transform.LookAt(boost.transform,Camera.main.transform);
           // //boost.transform.rotation = Quaternion.Euler(-90, 0, 0);
           // //boost.transform.Rotate(new Vector3(1, 0, 0), -90);
           // //boost.transform.Rotate(new Vector3(0, 0, 1), Camera.main.transform.eulerAngles.y);
           // boost.transform.LookAt(Camera.main.transform);
           // //boost.transform.rotation = Quaternion.Euler(0, 0, 0);

            //// boost.transform.rotation = Quaternion.Euler(boost.transform.rotation.x, boost.transform.rotation.y, boost.transform.rotation.z);
             boost.transform.Rotate(new Vector3(1, 0, 0), -90);
            // boost.transform.rotation = Quaternion.Euler(/*boost.transform.localEulerAngles.x*/90, boost.transform.localEulerAngles.y,/* boost.transform.localEulerAngles.z*/0);
            // Debug.Log(boost.transform.localEulerAngles.y);
            // //boost.transform.Rotate(new Vector3(1, 0, 0), transform.rotation.x);
            // //transform.rotation = Quaternion.Euler(/*transform.rotation.x*/0, transform.rotation.y, 0/*transform.rotation.z*/);
        }
        else
        {
            isboost = false;
            boost.SetActive(false);
        }
    }
}
