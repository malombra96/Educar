using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L85_puntaje : MonoBehaviour
{
    public int count;
    public List<Text> textos;
    public Text textoPuntaje1;
    public GameObject layoutPuntaje;
    public ControlNavegacion control;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var t in textos) {
            t.text = textoPuntaje1.text;
        }

        if (layoutPuntaje.activeSelf) {
            control.Forward(3.0f);
        }
    }
}
