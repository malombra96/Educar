using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A130_cuadratica : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Slider sliderA, sliderB, sliderC;
    public int a, b, c;
    public float x,y;
    [Header("Coordenadas")]
    public Vector3[] coordenadas;
    public Toggle toggle;
    public GameObject instrucciones;
    public Text textoA, textoB, textoC, textoA1, textoB1, textoC1;
    void Start()
    {
        coordenadas = new Vector3[240];
        lineRenderer.positionCount = coordenadas.Length;
        toggle.onValueChanged.AddListener(delegate { mostrar(); });
    }
    
    void mostrar() {
        if (toggle.isOn)
        {
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
            instrucciones.SetActive(true);
        }
        else {
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
            instrucciones.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        a = (int)sliderA.value;
        b = (int)sliderB.value;
        c = (int)sliderC.value;
        textoA.text = a.ToString();
        textoB.text = b.ToString();
        textoC.text = c.ToString();
        textoA1.text = "a = " + a.ToString();
        textoB1.text = "b = " + b.ToString();
        textoC1.text = "c = " + c.ToString();
        var pointList = new List<Vector3>();
        float v = -12f;
        Vector3 position;

       
        for (int i = 0; i < 240; i++)
        {
            x = v;
            y = a*(x*x)+ b*x +c;
            coordenadas[i] = new Vector3(x ,y, -9.6f);
            v += 0.1f;
            v = (float)Math.Round(v, 2);
        }
        lineRenderer.SetPositions(coordenadas);
    }
}
