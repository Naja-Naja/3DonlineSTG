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

        //�v���C���[�ʒu��Ǐ]����
        transform.position = new Vector3(Player.position.x, transform.position.y, Player.position.z);

        yaw += Input.GetAxis("Mouse X") * RotateSpeed; //����]����
        pitch -= Input.GetAxis("Mouse Y") * RotateSpeed; //�c��]����

        pitch = Mathf.Clamp(pitch, -80, 60); //�c��]�p�x��������

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f); //��]�̎��s
    }
}
