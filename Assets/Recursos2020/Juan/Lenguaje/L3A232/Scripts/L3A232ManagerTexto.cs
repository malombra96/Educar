using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.UI;

public class L3A232ManagerTexto : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;

    public GameObject bar;
    public List<L3A232ManagerTexto> managerTextos;
    public List<Button> botonesTexto;
    public List<Toggle> toggles;
    public List<int> desactivar;
    public L3A232VideoController controllerVideo;
    public string link;
    int j;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controllerVideo.gameObject.SetActive(false);

        foreach (var boton in botonesTexto)
            boton.onClick.AddListener(delegate { activarTexto(boton.transform.GetSiblingIndex()); });

        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate {StartCoroutine(calificar()); });
            toggle.interactable = false;
        }        
    }
    private void OnEnable()
    { 
        foreach (var x in desactivar)
            botonesTexto[x].interactable = false;
    }
    private void OnDisable() 
    {
        bar.gameObject.SetActive(true);
        controllerVideo.gameObject.SetActive(false); 
    }

    void activarTexto(int i)
    {
        j = i;
        controlAudio.PlayAudio(0);
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).transform.GetChild(i + 1).gameObject.SetActive(true);

        if (i == 0 || i == 2)
            toggles[1].GetComponent<L3A232Seleccionado>().correcto = true;
        else
            toggles[0].GetComponent<L3A232Seleccionado>().correcto = true;

        foreach (var toggle in toggles)
            toggle.interactable = true;
    }
    IEnumerator calificar()
    {
        foreach (var toggle in toggles)
        {
            toggle.interactable = false;
            if (toggle.isOn)
            {                
                controlAudio.PlayAudio(0);
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;

                //transform.GetChild(2).transform.GetChild(j+1).transform.GetChild().GetComponent<Button>().interactable = false;
                
                yield return new WaitForSeconds(1);
                
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<L3A232Seleccionado>().correcto ? 
                    toggle.GetComponent<BehaviourSprite>()._right : toggle.GetComponent<BehaviourSprite>()._wrong;

                controlAudio.PlayAudio(toggle.GetComponent<L3A232Seleccionado>().correcto ? 1 : 2);

                yield return new WaitForSeconds(1);
                controllerVideo.gameObject.SetActive(true);
                bar.gameObject.SetActive(false);
                if (toggle.GetComponent<L3A232Seleccionado>().correcto)
                {
                    controlPuntaje.IncreaseScore();
                    controllerVideo.PlayWebGL(link + botonesTexto[j].gameObject.name + " Correcto.mp4");
                }
                else
                    controllerVideo.PlayWebGL(link + botonesTexto[j].gameObject.name + " Incorrecto.mp4");
            }            
        }

        foreach (var x in managerTextos)
            x.desactivar.Add(j);        

        controlNavegacion.Forward(7);
    }
    public void cerrar()
    {        
        controlAudio.PlayAudio(0);

        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);

        for(int x=1;x< transform.GetChild(2).transform.childCount; x++) 
            transform.GetChild(2).transform.GetChild(x).gameObject.SetActive(false);
        foreach (var toggle in toggles)
        {
            toggle.interactable = false;            
            toggle.isOn = false;
            toggle.GetComponent<L3A232Seleccionado>().correcto = false;
        }
    }
    public void resetAll()
    {
        controllerVideo.gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;

        for (int x = 1; x < transform.GetChild(2).transform.childCount; x++)
            transform.GetChild(2).transform.GetChild(x).gameObject.SetActive(false);

        foreach (var boton in botonesTexto)
            boton.interactable = true;

        foreach (var toggle in toggles)
        {
            toggle.interactable = false;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
            toggle.isOn = false;
            toggle.GetComponent<L3A232Seleccionado>().correcto = false;
        }
        foreach (var x in managerTextos)
            x.desactivar.Clear();
    }
}
