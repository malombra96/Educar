using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A255_informacion : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite pc, movil;
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
