using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L6A276ControlDeActividades : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;

    [Header("Seleccion de actividad")]

     public GameObject monedas;
     public List<Toggle> toggles;
     public List<GameObject> objects;
     int actividadResueltas = 0;
     int objeto = 0;

    [Header("Seleccion de avatar")]

     public Button comenzar;
     public List<Toggle> avatares;
     public GameObject seleccionDeAvatar;
     public List<GameObject> avatarSeleccionado;
     
     public List<L6A276ControlActividad> controlActividads;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();        

        foreach (var toggle in toggles)
            toggle.onValueChanged.AddListener(delegate { StartCoroutine(SeleccionActividad()); });

        foreach (var avatar in avatares)
            avatar.onValueChanged.AddListener(delegate { toggleAvatar(); });

        comenzar.onClick.AddListener(SeleccionAvatar);
        comenzar.interactable = false;
    }
    void toggleAvatar()
    {
        int i = 0;
        controlAudio.PlayAudio(0);
        foreach (var avatar in avatares)
        {
            if (avatar.isOn)
            {
                i++;
                avatar.GetComponent<Image>().sprite = avatar.GetComponent<BehaviourSprite>()._selection;
            }
            else
                avatar.GetComponent<Image>().sprite = avatar.GetComponent<BehaviourSprite>()._default;
        }
        comenzar.interactable = (i > 0);
    }
    void SeleccionAvatar()
    {
        controlAudio.PlayAudio(0);
        var i = GetComponent<Image>();
        for (int x= 0;x < avatares.Count;x++)
        {
            if (avatares[x].isOn)
            {
                i = avatarSeleccionado[x].GetComponent<Image>();
                avatarSeleccionado[x].SetActive(true);
            }
            else
            {
                avatarSeleccionado[x].SetActive(false);
            }
        }

        foreach(var controlActividad in controlActividads)
        {
            controlActividad.fondo.sprite = i.sprite;
            controlActividad.avatar.sprite = i.transform.GetChild(1).GetComponent<Image>().sprite;
        }
        seleccionDeAvatar.SetActive(false);
    }
    IEnumerator SeleccionActividad()
    {
        controlAudio.PlayAudio(0);
        int actividad = 0;
        foreach (var toggle in toggles)
        {
            toggle.interactable = false;
            if (toggle.isOn && toggle.GetComponent<Image>().raycastTarget)
            {
                actividad = Convert.ToInt16(toggle.name);
                toggle.GetComponent<Image>().raycastTarget = false;
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
            }
        }
        yield return new WaitForSeconds(1);
        controlNavegacion.GoToLayout(actividad);
    }
    void OnDisable()
    {
        foreach (var toggle in toggles)
        {
            toggle.interactable = true;
            if (toggle.isOn)
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._disabled;
        }                
    }
    public void respuesta(bool b,int i)
    {
        if (actividadResueltas == 4)
        {
            objects[4].SetActive(true);
            objects[4].GetComponent<Image>().sprite = (objeto == 4) ? objects[4].GetComponent<BehaviourSprite>()._right : objects[4].GetComponent<BehaviourSprite>()._wrong;
            controlNavegacion.GoToLayout(8, 3);
        }
        if (b)
        {
            monedas.transform.GetChild(actividadResueltas).GetComponent<Image>().sprite =
                monedas.transform.GetChild(actividadResueltas).GetComponent<BehaviourSprite>()._selection;
            if (actividadResueltas < 4)
            {
                objects[objeto].SetActive(true);

                foreach (var controlActividad in controlActividads)
                {
                    controlActividad.avatar.transform.GetChild(objeto).gameObject.SetActive(true);
                    controlActividad.ganacias = i;
                    if (i == 0)
                        controlActividad.TextoGanacias.text = "0";
                    else
                        controlActividad.TextoGanacias.text = Convert.ToString(i) + "00 000";
                }
               
                objeto++;
            }
        }
        else
        {
            if (actividadResueltas < 4)
            {
                if (objeto > 0)
                {
                    print(objects[objeto]);
                    objects[objeto].SetActive(false);
                    foreach (var controlActividad in controlActividads)
                    {
                        controlActividad.avatar.transform.GetChild(objeto).gameObject.SetActive(false);
                        controlActividad.ganacias = i;
                        if (i == 0)
                            controlActividad.TextoGanacias.text = "0";
                        else if (i > 0)
                            controlActividad.TextoGanacias.text = Convert.ToString(i) + "00 000";
                        
                    }
                    objeto--;
                }                
            }
        }
        actividadResueltas++;
    }   
    public void ResetAll()
    {
        actividadResueltas = 0;
        objeto = 0;
        comenzar.interactable = false;
        
        foreach (var toggle in toggles)
        {
            toggle.isOn = false;
            toggle.interactable = true;
            toggle.GetComponent<Image>().raycastTarget = true;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
        }
        for (int x = 0; x < monedas.transform.childCount; x++)
        {
            monedas.transform.GetChild(x).GetComponent<Image>().sprite = 
                monedas.transform.GetChild(x).GetComponent<BehaviourSprite>()._default;
        }
        seleccionDeAvatar.SetActive(true);
        foreach (var avatar in avatares)
        {
            avatar.isOn = false;
            avatar.GetComponent<Image>().sprite = avatar.GetComponent<BehaviourSprite>()._default;
        }
        foreach (var controlActividad in controlActividads)
        {
            controlActividad.TextoGanacias.text = "0";
            for (int i = 0; i < controlActividad.avatar.transform.childCount; i++)
                controlActividad.avatar.transform.GetChild(i).gameObject.SetActive(false);
        }

    }
}
