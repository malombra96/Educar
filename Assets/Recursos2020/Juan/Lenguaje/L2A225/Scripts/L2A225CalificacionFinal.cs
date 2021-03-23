using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2A225CalificacionFinal : MonoBehaviour
{
    int x;
    private void OnEnable()
    {
        transform.GetChild(x == 9 ? 0 : 1).gameObject.SetActive(true);
    }

    public void calificar() 
    {        
        x++;
        print(x);
    }

    public void resetAll()
    {
        x = 0;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
