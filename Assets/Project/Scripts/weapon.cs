using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/SimpleWeapon")]
public class weapon : ScriptableObject
{

    //���������܂�񂩂�S��public�����ǌł܂��Ă�����[SerializedField]�ɕς���
    public int weaponID = 0;
    public string Name = "TestWeapon";
    public Sprite icon = null;
    public GameObject weaponTimeManager = null;
    private WeaponTimeManager TimeManager = null;
    public GameObject bullet = null;
    public float damage = 10;
    public float speed = 2500;
    public float lifetime = 10;
    //public bool reloadable = true;
    public bool reloading = false;
    public float reloadtime = 1;
    public float fireRate;
    [System.NonSerialized]
    public bool readyNextBullet = true;
    public float numberofBullet = 1;
    [System.NonSerialized]
    public float RuntimeNumberofBullet = 0;
    public bool passiveWeapon = false;
    public bool Homing = false;
    [System.NonSerialized]
    public Transform lockOnTarget;
    [System.NonSerialized]
    public bool targeting = false;
    public float locktime = 1;
    //public float changetime;
    //public bool pargable = false;
    //[System.NonSerialized]
    //public bool parged = false;
    //public float weaponweight = 0;
    public void init()
    {
        reloading = false;
        TimeManager = Instantiate(weaponTimeManager).GetComponent<WeaponTimeManager>();
        TimeManager.weapon = this;
        if (passiveWeapon == true)
        {
            //�p�b�V�u����N������
        }
        else
        {
            RuntimeNumberofBullet = numberofBullet;
        }
        if (Homing)
        {
            readyNextBullet = false;
        }
    }
    public GameObject Shot()
    {
        Debug.Log("shotcall");
        //�ˌ����ł��Ȃ����X�̏���
        if (reloading)
        {
            return null;
        }
        if (readyNextBullet == false)
        {
            return null;
        }
        RuntimeNumberofBullet--;
        readyNextBullet = false;
        TimeManager.nextbullet(fireRate);

        //�e��0�ɂȂ����Ȃ烊���[�h
        if (RuntimeNumberofBullet <= 0)
        {
            reloading = true;
            TimeManager.reload(reloadtime);
        }

        //�e��Ԃ�
        Debug.Log("shot!!");
        return bullet;
    }
    public void Targetting(Transform targetposition = null)
    {
        if (targetposition == null)
        {
            StopTargeting();
        }
        //else if (targetposition != lockOnTarget && targetposition != null)
        //{
        //    StopTargeting();
        //}
        else if (targeting == true) { }
        else
        {
            lockOnTarget = targetposition;
            TimeManager.LockOnTimer(locktime);
            Debug.Log(targetposition.gameObject);
        }
    }
    public void StopTargeting()
    {
        //readyNextBullet = false;
        //lockOnTarget = null;
        //TimeManager.LockOnTimerStop();
        if (readyNextBullet == true) { return; }
        else
        {
            readyNextBullet = false;
            lockOnTarget = null;
            TimeManager.LockOnTimerStop();
        }
    }
}
