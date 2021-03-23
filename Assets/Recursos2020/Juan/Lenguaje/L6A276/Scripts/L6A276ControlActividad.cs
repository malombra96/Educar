using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L6A276ControlActividad : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;


    public L6A276ControlDeActividades controlDeActividades;
    [HideInInspector] public List<L6A276Drop> _drops;    
    public Button validar;
    public Image fondo, avatar;
    public Text TextoGanacias;
    public int ganacias;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();

        validar.onClick.AddListener(Calificar);
        validar.interactable = false;        
    }   
    public void activarValidar()
    {        
        int i = 0;        
        foreach (var drop in _drops)
        {            
            if (drop.drag)
                i++;
            drop.BuscarDragCorrecto();
        }

        validar.interactable = (i == _drops.Count);
    }
    void Calificar()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;

        int right = 0;
        foreach(var drop in _drops)
        {
            if (drop.drag)
            {
                if (drop.drag.name == drop.dragCorrecto.name)
                {
                    right++;                    
                    drop.drag.GetComponent<Image>().sprite = drop.drag.GetComponent<BehaviourSprite>()._right;                   
                }
                else
                    drop.drag.GetComponent<Image>().sprite = drop.drag.GetComponent<BehaviourSprite>()._wrong;
            }
        }
        aumentoGanancias(right == _drops.Count);
        controlAudio.PlayAudio(right == _drops.Count ? 1 : 2);

        controlDeActividades.respuesta(right == _drops.Count, ganacias);

        controlPuntaje.IncreaseScore(right);
        controlNavegacion.GoToLayout(controlDeActividades.transform.GetSiblingIndex(), 2);        
    }

    void aumentoGanancias(bool b)
    {
        if (b)        
            ganacias += 2;        
        else        
            ganacias -= 2;                  

        if (ganacias > 0)        
            TextoGanacias.text = Convert.ToString(ganacias) + "00 000";
        else
            TextoGanacias.text = "0";
    }

    public void ResetAll()
    {
        ganacias = 0;
        validar.interactable = false;
        foreach (var drop in _drops)
        {
            if (drop.drag)
                Destroy(drop.drag);
        }       
    }
}
