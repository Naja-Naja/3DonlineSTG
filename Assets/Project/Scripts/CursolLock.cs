using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursolLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // �J�[�\����\��
        Cursor.visible = false;

        // �J�[�\������ʒ����Ƀ��b�N����
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible == false)
            {
                // �J�[�\���\��
                Cursor.visible = true;

                // �J�[�\�������R�ɓ�������
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.visible == true)
            {
                // �J�[�\����\��
                Cursor.visible = false;

                // �J�[�\������ʒ����Ƀ��b�N����
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
