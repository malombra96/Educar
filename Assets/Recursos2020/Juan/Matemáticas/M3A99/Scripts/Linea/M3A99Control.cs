using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M3A99Control : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    public Color32 colorCorrecto, colorIncorrecto;    
    public GameObject Lineas;    
    public Button validar;
    public GameObject lineaGenerar;   
    public GameObject pregunta;   
    public GameObject respuesta;   
    [HideInInspector] public bool dibujar;
    int punto;
    int TempIndex1;
    int TempIndex2;

    LineRenderer linea;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();       
        
        validar.onClick.AddListener(Calificar);
        validar.interactable = false;
    }

    void Update()
    {
        if (dibujar && linea)
            linea.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    public void invocar(GameObject Obj)
    {
        Obj.transform.GetChild(0).GetComponent<Button>().interactable = false;
        controlAudio.PlayAudio(0);
        if (!dibujar)
        {
            GameObject LineaActial = Instantiate(lineaGenerar, Lineas.transform);            
            linea = LineaActial.GetComponent<LineRenderer>();
            linea.positionCount = 2;
            Vector2 posInicial = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            linea.SetPosition(0, posInicial);
            dibujar = true;            
        }
        else
        {
            dibujar = false;
            Vector2 posFinal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            linea.SetPosition(1, posFinal);              
        }        
        unionLinia(Obj.transform.GetSiblingIndex());
    }
    void unionLinia(int index)
    {
        int g = 0;
        punto++;        

        switch (punto)
        {
            case 1:
                TempIndex1 = index;
                break;
            case 2:
                TempIndex2 = index;

                for (int x = 0; x < Lineas.transform.childCount; x++)
                {
                    g++;
                    validar.interactable = g >= 5 ? true : false;
                }

                if (TempIndex2 == TempIndex1)
                    linea.GetComponent<M3A99Linea>().correcto = true;

                linea = null;
                punto = 0;
                break;
        }        
    }
    void Calificar()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;
        
        int right = 0;
        for(int x = 0; x < Lineas.transform.childCount; x++)
        {
            var linea = Lineas.transform.GetChild(x).GetComponent<M3A99Linea>();
            if (linea.correcto)
            {
                right++;
                linea.GetComponent<LineRenderer>().startColor = colorCorrecto;
                linea.GetComponent<LineRenderer>().endColor = colorCorrecto;
            }
            else
            {
                linea.GetComponent<LineRenderer>().startColor = colorIncorrecto;
                linea.GetComponent<LineRenderer>().endColor = colorIncorrecto;
            }
        }

        controlAudio.PlayAudio(right == 5 ? 1 : 2);
        controlPuntaje.IncreaseScore(right);
        controlNavegacion.Forward(2);        
    }
    public void borrar()
    {
        controlAudio.PlayAudio(0);
        int g = 0;
        if (Lineas.transform.childCount != 0)
        {
            GameObject i = Lineas.transform.GetChild(Lineas.transform.childCount - 1).gameObject;
            Destroy(i);
        }

        for(int x = 0; x < pregunta.transform.childCount; x++)
        {
            if (!pregunta.transform.GetChild(x).transform.GetChild(0).GetComponent<Button>().interactable)
            {
                pregunta.transform.GetChild(x).transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
            if (!respuesta.transform.GetChild(x).transform.GetChild(0).GetComponent<Button>().interactable)
            {                
                respuesta.transform.GetChild(x).transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
        for (int x = 0; x < Lineas.transform.childCount; x++)
        {            
            validar.interactable = g >= 5 ? true : false;
            g++;
        }

        dibujar = false;
        linea = null;
        punto = 0;
        
    }
    public void resetAll()
    {
        punto = 0;
        validar.interactable = false;
        dibujar = false;
        linea = null;
        for (int x = 0; x < pregunta.transform.childCount; x++)
        {
            if (!pregunta.transform.GetChild(x).transform.GetChild(0).GetComponent<Button>().interactable)
            {
                pregunta.transform.GetChild(x).transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
            if (!respuesta.transform.GetChild(x).transform.GetChild(0).GetComponent<Button>().interactable)
            {
                respuesta.transform.GetChild(x).transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
        for (int x = 0; x < Lineas.transform.childCount; x++)
             Destroy(Lineas.transform.GetChild(x).gameObject);
    }
}
