using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class HPBar : MonoBehaviour
{
    [SerializeField] FloatVariable ScriptableHP;
    [SerializeField] Image HPImage;
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HPImage.fillAmount = ScriptableHP.RuntimeValue / ScriptableHP.Value;

        //文字列結合は重いらしいので今後負荷チェックしたい
        string str1 = ScriptableHP.RuntimeValue.ToString();
        string str2 = "/";
        string str3 = ScriptableHP.Value.ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append(str1);
        sb.Append(str2);
        sb.Append(str3);
        string result = sb.ToString();
        text.text = result;
    }
}
