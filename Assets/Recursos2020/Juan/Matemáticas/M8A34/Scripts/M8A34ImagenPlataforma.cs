using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A34ImagenPlataforma : MonoBehaviour
{
    public Image imagenDeCambio;
    public Sprite imagenPc, imagenMovil;    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            imagenDeCambio.sprite = imagenMovil;
        }
        else
        {
            imagenDeCambio.sprite = imagenPc;
        }
    }
}
