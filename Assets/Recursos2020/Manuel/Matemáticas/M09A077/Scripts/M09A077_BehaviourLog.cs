using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A077_BehaviourLog : MonoBehaviour
{
    [Header("Slider")] public Slider _slider;
    [Header("Text")] public Text _text;

    [Header("LineRenderer")] public LineRenderer _graphic;

    [Header("Coordenadas")] 
    public Vector3[] coordenadas; 


    void Start()
    {
        coordenadas = new Vector3[51];
        _graphic.positionCount = coordenadas.Length;
        _slider.onValueChanged.AddListener(delegate{GetCoordenadas(_slider.value);});
        GetCoordenadas(0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        double n =  Math.Truncate(_slider.value*100)/100;
        _text.text =  $" a = {n.ToString()}";
    }

    void GetCoordenadas(float b)
    {
        float v = 0.001f;
        

        for (int i = 0; i < 51; i++)
        {
            float log = Mathf.Log(v,b);
            //print($"Base={b},Value={v},Log={log}");
            coordenadas[i] = new Vector3(v,log,-9.6f);
            v+=0.1f;
            v = (float) Math.Round(v,1);
        }

        _graphic.SetPositions(coordenadas);

    }
}
