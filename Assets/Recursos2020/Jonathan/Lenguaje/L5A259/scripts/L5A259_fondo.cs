using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A259_fondo : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite pc, movil;
    public L5A259_barco barco;
    void Start()
    {
        barco.mover = false;
        if (Application.isMobilePlatform) {
            GetComponent<Image>().sprite = movil;
        } 
        else {
            GetComponent<Image>().sprite = pc;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
