using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTimeManager : MonoBehaviour
{
    public weapon weapon;
    public void reload(float reloadtime)
    {
        Debug.Log("coroutinestart");
        StartCoroutine("reloadCoroutine",reloadtime);
    }
    IEnumerator reloadCoroutine(float reloadtime)
    {
        yield return new WaitForSeconds(reloadtime);
        weapon.reloading = false;
    }
}
