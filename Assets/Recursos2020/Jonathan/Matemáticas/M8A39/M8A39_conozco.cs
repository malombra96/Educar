using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A39_conozco : MonoBehaviour
{

    public BehaviourInteraccion bnt_1, bnt_2;
    public bool C1, C2;

    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
        }
        if (!bnt_1._state && !C1)
        {
            C1 = true;
            int x = 0;
            PlayerPrefs.SetInt("C1", x + 1);
        }
        if (!bnt_2._state && !C2)
        {
            C2 = true;
            int x1 = 0;
            PlayerPrefs.SetInt("C2", x1 + 1);
        }
    }
}
