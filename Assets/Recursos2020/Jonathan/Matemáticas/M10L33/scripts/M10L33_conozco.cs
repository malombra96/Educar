using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_conozco : MonoBehaviour
{
    public BehaviourInteraccion conozco_1, conozco_2, conozco_3, conozco_4;
    public bool C1, C2, C3, C4;

    void Update()
    {

        if (Input.GetKey(KeyCode.Escape)) {
            PlayerPrefs.DeleteAll();
        }
        if (!conozco_1._state && !C1) {
            C1 = true; 
            int x = 0;
            PlayerPrefs.SetInt("C1",x+1);
        }
        if (!conozco_2._state && !C2)
        {
            C2 = true;
            int x1 = 0;
            PlayerPrefs.SetInt("C2", x1 + 1);
        }
        if (!conozco_3._state && !C3)
        {
            C3 = true;
            int x2 = 0;
            PlayerPrefs.SetInt("C3", x2+ 1);
        }
        if (!conozco_4._state && !C4)
        {
            C4 = true;
            int x3 = 0;
            PlayerPrefs.SetInt("C4", x3 + 1);
        }
    }
}
