using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/SimpleWeapon")]
public class weapon : ScriptableObject
{

    //実装が決まらんから全部publicだけど固まってきたら[SerializedField]に変える
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
    //public float changetime;
    //public bool pargable = false;
    //[System.NonSerialized]
    //public bool parged = false;
    //public float weaponweight = 0;
    public void init()
    {
        reloading = false;
        TimeManager=Instantiate(weaponTimeManager).GetComponent<WeaponTimeManager>();
        TimeManager.weapon = this;
        if (passiveWeapon == true)
        {
            //パッシブ武器起動処理
        }
        else
        {
            RuntimeNumberofBullet = numberofBullet;
        }
    }
    public GameObject Shot()
    {
        Debug.Log("shotcall");
        //射撃ができない諸々の条件
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

        //弾が0になったならリロード
        if (RuntimeNumberofBullet <= 0)
        {
            reloading = true;
            TimeManager.reload(reloadtime);
        }

        //弾を返す
        Debug.Log("shot!!");
        return bullet;
    }
}
