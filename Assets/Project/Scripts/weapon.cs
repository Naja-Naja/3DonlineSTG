using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/SimpleWeapon")]
public class weapon : ScriptableObject
{

    //���������܂�񂩂�S��public�����ǌł܂��Ă�����[SerializedField]�ɕς���
    public string Name = "TestWeapon";
    public GameObject weaponTimeManager = null;
    private WeaponTimeManager TimeManager = null;
    public GameObject bullet = null;
    public bool reloadabe = true;
    public bool reloading = false; 
    public float reloadtime = 1;
    public float fireRate;
    [System.NonSerialized]
    public bool readyNextBullet = true;
    public float numberofBullet = 1;
    [System.NonSerialized]
    public float RuntimeNumberofBullet = 0;
    public bool passiveWeapon = false;
    public float changetime;
    public bool pargable = false;
    [System.NonSerialized]
    public bool parged = false;
    public float weaponweight = 0;
    public void init()
    {
        reloading = false;
        TimeManager=Instantiate(weaponTimeManager).GetComponent<WeaponTimeManager>();
        TimeManager.weapon = this;
        if (passiveWeapon == true)
        {
            //�p�b�V�u����N������
        }
        else
        {
            RuntimeNumberofBullet = numberofBullet;
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
        if (readyNextBullet==false)
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
}
