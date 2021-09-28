using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTimeManager : MonoBehaviour
{
    public weapon weapon;

    //�����[�h�^�C�}�[
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


    //���ˊԊu�^�C�}�[
    public void nextbullet(float fireRate)
    {
        StartCoroutine("firerateCoroutine", fireRate);
    }
    IEnumerator firerateCoroutine(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        weapon.readyNextBullet = true;
    }
}
