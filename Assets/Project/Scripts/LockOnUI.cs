using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnUI : MonoBehaviour
{
    [SerializeField] weaponflame weaponflame;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image image;
    [SerializeField] GameObject LockOnCircle;
    [SerializeField] GameObject LockOnTarget;
    [SerializeField] Color color1;
    [SerializeField] Color color2;

    // Update is called once per frame
    void Update()
    {
        if (weaponflame.Equipment[weaponflame.choiceWeapon].Homing == true)
        {
            LockOnCircle.SetActive(true);
            LockOnTarget.SetActive(true);
            if (weaponflame.Equipment[weaponflame.choiceWeapon].lockOnTarget == null)
            {
                rectTransform.position = new Vector3(-100, -100, -100);
                return;
            }
            rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main,  weaponflame.Equipment[weaponflame.choiceWeapon].lockOnTarget.position);
            //position=RectTransformUtility.WorldToScreenPoint(Camera.main, /*cube.transform.position*/).x;
        }
        else
        {
            LockOnCircle.SetActive(false);
            LockOnTarget.SetActive(false);
        }
        if (weaponflame.Equipment[weaponflame.choiceWeapon].readyNextBullet)
        {
            image.color = color1;
        }
        else
        {
            image.color = color2;
        }
    }
}
