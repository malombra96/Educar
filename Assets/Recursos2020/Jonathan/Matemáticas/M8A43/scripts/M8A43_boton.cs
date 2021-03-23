using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A43_boton : MonoBehaviour
{
    public List<GameObject> pareja;
    public Button boton;
    public M8A43_conozco _manager;
    public ControlAudio controlAudio;
    public int caso;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        boton.onClick.AddListener(clickBoton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickBoton() {
        controlAudio.PlayAudio(0);
        _manager.TrazarLinea(boton.gameObject);
    }
}
