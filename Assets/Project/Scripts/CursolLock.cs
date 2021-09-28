using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursolLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // カーソル非表示
        Cursor.visible = false;

        // カーソルを画面中央にロックする
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible == false)
            {
                // カーソル表示
                Cursor.visible = true;

                // カーソルを自由に動かせる
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.visible == true)
            {
                // カーソル非表示
                Cursor.visible = false;

                // カーソルを画面中央にロックする
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
