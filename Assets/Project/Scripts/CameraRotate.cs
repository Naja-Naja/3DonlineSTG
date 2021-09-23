using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float RotateSpeed;

    float yaw, pitch;

    private void Start()
    {
        RotateSpeed = 1;
    }

    void Update()
    {

        //ƒvƒ‰ƒCƒ„[ˆÊ’u‚ğ’Ç]‚·‚é
        transform.position = new Vector3(Player.position.x, transform.position.y, Player.position.z);

        yaw += Input.GetAxis("Mouse X") * RotateSpeed; //‰¡‰ñ“]“ü—Í
        pitch -= Input.GetAxis("Mouse Y") * RotateSpeed; //c‰ñ“]“ü—Í

        pitch = Mathf.Clamp(pitch, -80, 60); //c‰ñ“]Šp“x§ŒÀ‚·‚é

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f); //‰ñ“]‚ÌÀs
    }
}
