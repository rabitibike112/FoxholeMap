using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    private Dropdown SelfDropDown;
    public Color CurrectColor;
    public int CurrentColorId;
    // Start is called before the first frame update
    void Start()
    {
        CurrectColor = Color.blue; 
        CurrectColor.a = 0.3f;
        CurrentColorId = 0;
        SelfDropDown = transform.Find("Panel").transform.Find("ColorPicker").GetComponent<Dropdown>();
        SelfDropDown.onValueChanged.AddListener(delegate { ChangeCurrentColor(SelfDropDown.value); });
    }

    public Color GetColorById(int ID)
    {
        Color Temp;
        switch (ID)
        {
            case 0: Temp = Color.blue; Temp.a = 0.3f;break;
            case 1: Temp = Color.red; Temp.a = 0.3f;break;
            case 2: Temp = Color.green; Temp.a = 0.3f;break;
            case 3: Temp = Color.magenta; Temp.a = 0.3f;break;
            case 4: Temp = Color.cyan; Temp.a = 0.3f;break;
            case 5: Temp = Color.gray; Temp.a = 0.3f;break;
            default: Temp = Color.blue; Temp.a = 0.3f;break;
        }
        return Temp;
    }

    private void ChangeCurrentColor(int value)
    {
        switch (value)
        {
            case 0:CurrectColor = Color.blue; CurrectColor.a = 0.3f; CurrentColorId = 0; break;
            case 1:CurrectColor = Color.red; CurrectColor.a = 0.3f; CurrentColorId = 1; break;
            case 2:CurrectColor = Color.green; CurrectColor.a = 0.3f; CurrentColorId = 2; break;
            case 3:CurrectColor = Color.magenta; CurrectColor.a = 0.3f; CurrentColorId = 3; break;
            case 4:CurrectColor = Color.cyan; CurrectColor.a = 0.3f; CurrentColorId = 4; break;
            case 5:CurrectColor = Color.gray; CurrectColor.a = 0.3f; CurrentColorId = 5; break;
            default:CurrectColor = Color.blue; CurrectColor.a = 0.3f; CurrentColorId = 0; break;
        }
    }
}
