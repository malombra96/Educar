using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8L40_ControlInsignias : MonoBehaviour
{
    [HideInInspector] public List<GameObject> insignias = null;
    public M8L40_Insignias control;

    private void Awake()
    {
        control = FindObjectOfType<M8L40_Insignias>();
        for (int i = 0; i < transform.childCount - 1; i++)
            insignias.Add(this.transform.GetChild(i).gameObject);
    }

    public void Refresh()
    {
        for (int i = 0; i < insignias.Count; i++)
            if (control.contador > -1 && control.contador >= i )
            {
                insignias[i].SetActive(true);
            }
            else
            {
                insignias[i].SetActive(false);
            }
    }
    
    public void Rechange()
    {
        control.contador++;
        Refresh();
    }
}
