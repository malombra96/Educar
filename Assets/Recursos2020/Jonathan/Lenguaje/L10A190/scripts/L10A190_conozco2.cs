using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class L10A190_conozco2 : MonoBehaviour
{
    public ControlPuntaje puntaje;
    public Text textConozco;
    int a, b;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("C11") == 0)
        {
            a = 0;
        }
        else
        {
            a = 1;
        }
        if (PlayerPrefs.GetInt("C22") == 0)
        {
            b = 0;
        }
        else
        {
            b = 1;
        }


        if ((a + b) == 2)
        {
            textConozco.text = "100%";
        }
        else if ((a + b) == 1)
        {
            textConozco.text = "50%";
        }
        else
        {
            textConozco.text = "0%";

        }


    }

    public void DeleteValues()
    {
        PlayerPrefs.DeleteAll();
    }
}
