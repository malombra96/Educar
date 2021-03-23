using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A259_instruccion : MonoBehaviour
{
    public Sprite pc, movil;
    public L5A259_barco barco;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            gameObject.GetComponent<Image>().sprite = movil;
        }
        else {
            gameObject.GetComponent<Image>().sprite = pc;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void INiciar() {
        barco.mover = true;
        gameObject.SetActive(false);
    }
}
