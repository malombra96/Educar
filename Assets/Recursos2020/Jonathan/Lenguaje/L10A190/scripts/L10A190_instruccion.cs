using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10A190_instruccion : MonoBehaviour
{
    public Sprite pc, movil;
    // Start is called before the first frame update
    void Start()
    {
        
        if (Application.isMobilePlatform)
        {
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
