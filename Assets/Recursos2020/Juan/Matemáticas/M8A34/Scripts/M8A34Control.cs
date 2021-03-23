using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A34Control : MonoBehaviour
{
    public GameObject controlesMoviles, portada, aplico1, aplico2;
    ControlAudio controlAudio;
    public bool puedoLanzar,acitvar;

    M8A34ControlTotal controlTotal;

    ControlPuntaje controlPuntaje;
    public GameObject opciones;
    M8A34balon balon;
    ControlNavegacion controlNavegacion;  
    double punto;   
    public Text textpuntaje;
    public int x = 1;
    bool moverme = true;
    void Start()
    {
        controlTotal = FindObjectOfType<M8A34ControlTotal>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlAudio = FindObjectOfType<ControlAudio>();
        balon = FindObjectOfType<M8A34balon>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();               
        textpuntaje.text = "" + (controlTotal.puntos_1 * 20);
    }
    void FixedUpdate()
    {    
        if (moverme)
        {           
            textpuntaje.text = "" + (controlTotal.puntos_1 * 20);
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                derecha();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                izquierda();
            }
            if (Input.GetKeyDown(KeyCode.Space) && puedoLanzar)
            {
                lanzar();
            }
            balon.GetComponent<Animator>().SetInteger("pos", x);
        }
        
    }
    public void derecha()
    {
        if (x < 2)
            x++;              
    }
    public void izquierda()
    {
        if (x > 0)
            x--;      
       
    }
    public void lanzar()
    {        
        puedoLanzar = false;
        if (moverme && gameObject.activeSelf)
        {
            moverme = false;
            for (int i = 0; i < 3; i++)
            {
                if (i == x)
                {
                   
                    balon.animator.SetBool("lanzar", true);
                    if (opciones.transform.GetChild(x).GetComponent<M8A34Opciones>().respuesta)
                    {
                        controlPuntaje.IncreaseScore(1);
                        controlTotal.puntos_1++;
                        controlAudio.PlayAudio(1);
                        punto = controlTotal.puntos_1;
                        textpuntaje.text = "" + (punto * 20);                       

                        opciones.transform.GetChild(x).GetChild(0).GetComponent<Image>().sprite = opciones.transform.GetChild(x).GetComponent<M8A34Opciones>().correcto;
                        opciones.transform.GetChild(x).GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        opciones.transform.GetChild(x).GetChild(0).GetComponent<Image>().sprite = opciones.transform.GetChild(x).GetComponent<M8A34Opciones>().incorrecto;
                        opciones.transform.GetChild(x).GetChild(0).gameObject.SetActive(true);
                        
                        controlAudio.PlayAudio(2);
                    }
                    
                    break;
                }
            }            
            controlNavegacion.Forward(2.1f);
            controlesMoviles.SetActive(false);
        }                    
       
    }
    
    public void activador() 
    {
        puedoLanzar = true;        
    }    
   

    public void resetAll()
    { 
        for (int x = 0; x < 3; x++)
        {
            opciones.transform.GetChild(x).GetChild(0).gameObject.SetActive(false);
            controlesMoviles.transform.GetChild(x).GetComponent<Button>().onClick.RemoveAllListeners();
        }      
        
        puedoLanzar = false;

        if (acitvar)
            activador();
        if(controlTotal)
            controlTotal.puntos_1 = 0;
        punto = 0;
        textpuntaje.text = "0";        
        portada.SetActive(true);        
        moverme = true;
        x = 1;
       
    }

}
