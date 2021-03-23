using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M06A088_Conozco : MonoBehaviour
{
    public Slider slider;
    public Text a,b;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(delegate{SetValue(slider.value);});
        slider.value = 100000f;
    }

    void SetValue(float value)
    {
        a.text = "$"+(slider.maxValue - value).ToString();
        b.text = "$"+value.ToString();

    }
}
