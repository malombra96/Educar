using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L2A224SeleccionActividad : MonoBehaviour
{
    public GameObject posciones;
    public GameObject ciclista;
    public GameObject informacion;
    public Button[] buttons;
    
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    int x;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(delegate { irActividad(button.transform.GetSiblingIndex()); });
            button.gameObject.SetActive(false);
        }
        buttons[0].gameObject.SetActive(true);
        informacion.gameObject.SetActive(true);
        //ciclista.GetComponent<Animator>().Play("quieto");

    }

    void irActividad(int i)
    {
        controlAudio.PlayAudio(0);
        controlNavegacion.GoToLayout(i + 3);
        for (int z = 0; z < buttons.Length; z++) 
        {
            if (z == i)
            {
                buttons[i].interactable = false;
                buttons[i].GetComponent<Image>().color = new Color32(144, 144, 144, 255);                
                break;
            }
        }
       
    }
    public void VolverAlMenu()
    {
        x++;
        if (x < buttons.Length)
        {
            controlNavegacion.GoToLayout(2, 2f);
            buttons[x - 1].gameObject.SetActive(false);
            buttons[x].gameObject.SetActive(true);
            //ciclista.GetComponent<Animator>().Play("pos 1");
            Invoke("activarAnimacion", 2.1f);
        }
        else
        {
            ciclista.SetActive(false);
            controlNavegacion.GoToLayout(9, 2f);
        }
    }
    void activarAnimacion()=> ciclista.GetComponent<Animator>().Play("pos " + x);   
    public void resetAll()
    {
        x = 0;
        ciclista.SetActive(true);
        foreach (var button in buttons)
        {
            button.interactable = true;
            button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            button.gameObject.SetActive(false);
        }
        buttons[0].gameObject.SetActive(true);
        informacion.SetActive(true);
        ciclista.GetComponent<Animator>().Play("quieto");
    }
}
