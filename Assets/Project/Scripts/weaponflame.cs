using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/flame")]
public class weaponflame : ScriptableObject
{
    public List<weapon> Equipment = new List<weapon>();
    [System.NonSerialized]
    public int choiceWeapon = 0;
    public void weaponchange(int num)
    {
        choiceWeapon = choiceWeapon + num;
        if (choiceWeapon < 0)
        {
            choiceWeapon = Equipment.Count-1;
        }
        else if (choiceWeapon >= Equipment.Count)
        {
            choiceWeapon = 0;
        }
        Debug.Log(choiceWeapon);
    }
}
