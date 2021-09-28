using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class weaponUI : MonoBehaviour
{
    weapon weapon;
    [SerializeField] weaponflame weaponflame;
    [SerializeField] Image bulletImage;
    [SerializeField] Text bullettext;
    [SerializeField] Text weaponname;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        weapon = weaponflame.Equipment[weaponflame.choiceWeapon];
        weaponname.text = weapon.Name;
        bulletImage.fillAmount = weapon.RuntimeNumberofBullet / weapon.numberofBullet;
        if (weapon.reloading == true)
        {
            bullettext.text = "Reloading. . .";
            return;
        }
        //文字列結合は重いらしいので今後負荷チェックしたい
        string str1 = weapon.RuntimeNumberofBullet.ToString();
        string str2 = "/";
        string str3 = weapon.numberofBullet.ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append(str1);
        sb.Append(str2);
        sb.Append(str3);
        string result = sb.ToString();
        bullettext.text = result;
    }
}