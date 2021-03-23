using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A34ControlTotal : MonoBehaviour 
{ 
    ControlNavegacion controlNavegacion;
    public GameObject control_2, control_1, corazon;
    public bool activaControl_2 = true;
    /*[HideInInspector]*/ public double puntos_1, puntos_2;
    [HideInInspector] public int vidas = 3;
    void Start()
    {
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        InvokeRepeating("activar", 0.1f, 0.1f);
    }
    bool n=true;
    public void activarControl()
    {
        activaControl_2 = true;       
    }
    void activar()
    {
        foreach( var layout in controlNavegacion._Layouts)
        {
            if (layout.gameObject.activeSelf)
            {
                if (layout.GetComponent<M8A34Control>())
                {      
                    
                    Activador(corazon, false);

                    if (Application.isMobilePlatform && layout.GetComponent<M8A34Control>().puedoLanzar)
                    {
                        Activador(control_1, true);
                        control(layout.gameObject);
                    }
                    else if (Application.isMobilePlatform)
                    {
                        Activador(control_2, false);
                        Activador(control_1, false);
                    }
                }
                else if (layout.GetComponent<M8A34Manager>() && layout.GetComponent<M8A34Manager>().activar)
                {               
                    
                    Activador(corazon, true);
                    corazon.transform.SetParent(layout.transform);

                    if (Application.isMobilePlatform && activaControl_2) 
                    {
                        Activador(control_2, true);
                    }
                    else if (Application.isMobilePlatform)
                    {
                        Activador(control_1, false);
                    }

                }
                else
                {
                    corazon.SetActive(false);

                    if (!Application.isMobilePlatform)
                    {
                        Activador(control_2, false);  
                        Activador(control_1, false); 
                        activaControl_2 = false;                        
                    }
                }
            }
        }
    }
    void Activador(GameObject Object,bool estado)
    {
        Object.SetActive(estado);
    }
    GameObject temp;
    void control(GameObject x)
    {
        
        if (temp != x)
        {
            for (int j = 0; j < 3; j++)
                control_1.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            n = true;
            temp = x;
            if (n)
            {
                control_1.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(x.GetComponent<M8A34Control>().izquierda);
                control_1.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(x.GetComponent<M8A34Control>().derecha);
                control_1.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(x.GetComponent<M8A34Control>().lanzar);
                n = false;
            }
        }
    }    
    public void pierdoVidas()
    {
        vidas--;
        corazon.transform.GetChild(vidas).gameObject.SetActive(false);
    }
    public void resetVidas()
    {
        vidas = 3;
        for (int x = 0; x < 3; x++)
            corazon.transform.GetChild(x).gameObject.SetActive(true);
    }
   
}
