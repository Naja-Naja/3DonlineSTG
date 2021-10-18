using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTimeManager : MonoBehaviour
{
    public weapon weapon;

    //リロードタイマー
    public void reload(float reloadtime)
    {
        Debug.Log("coroutinestart");
        StartCoroutine("reloadCoroutine",reloadtime);
    }
    IEnumerator reloadCoroutine(float reloadtime)
    {
        yield return new WaitForSeconds(reloadtime);
        weapon.reloading = false;
        weapon.RuntimeNumberofBullet = weapon.numberofBullet;
    }


    //発射間隔タイマー
    public void nextbullet(float fireRate)
    {
        StartCoroutine("firerateCoroutine", fireRate);
    }
    IEnumerator firerateCoroutine(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        weapon.readyNextBullet = true;
    }

    //ロックオンタイマー
    public void LockOnTimer(float Locktime)
    {
        Debug.Log("coroutinestart");
        StartCoroutine("LockOnCoroutine", Locktime);
    }
    public void LockOnTimerStop()
    {
        Debug.Log("coroutinestop");
        StopCoroutine("LockOnCoroutine");
    }
    IEnumerator LockOnCoroutine(float LockTime)
    {
        yield return new WaitForSeconds(LockTime);
        weapon.readyNextBullet = true;
    }
}
