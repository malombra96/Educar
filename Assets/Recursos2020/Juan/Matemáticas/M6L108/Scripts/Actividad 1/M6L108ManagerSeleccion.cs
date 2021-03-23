using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L108ManagerSeleccion : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;

    public GameObject Dardos;
    public GameObject toggles;
    public GameObject Vidas;
    public int pregunta;
    [HideInInspector] public int intento;
    public List<M6L108ManagerSeleccion> seleccion;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();

        for (int x = 0; x < toggles.transform.childCount; x++)
            toggles.transform.GetChild(x).GetComponent<Toggle>().onValueChanged.AddListener(delegate { Seleccion(); });
    }
    private void OnEnable()
    {
        if (gameObject.activeSelf)
        {
            for (int x = 0; x < pregunta; x++)
            {
                GameObject dardo = Dardos.transform.GetChild(x).gameObject;
                dardo.SetActive(false);                
            }
        }
    }
    void Seleccion()
    {        
        GameObject toggle;
        for (int x = 0; x < toggles.transform.childCount; x++) 
        {
            toggle = toggles.transform.GetChild(x).gameObject;                        
            if (toggle.GetComponent<Toggle>().isOn)
            {
                controlAudio.PlayAudio(0);
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
                Dardos.transform.GetChild(pregunta).GetComponent<M6L108Dardo>().hubicacion = toggle.GetComponent<RectTransform>();                
                for (int i = 0; i < toggles.transform.childCount; i++)
                    toggles.transform.GetChild(i).GetComponent<Toggle>().interactable = false;
                StartCoroutine(Calificar(2, toggle.GetComponent<M6L108Respuesta>().Correcta, toggle.GetComponent<Image>()));                
            }                       
        }
             
    }
    IEnumerator Calificar(float t, bool respuesta,Image i)
    {
        yield return new WaitForSeconds(t);        
        controlAudio.PlayAudio(respuesta ? 1 : 2);
        i.sprite = respuesta ? i.GetComponent<BehaviourSprite>()._right : i.GetComponent<BehaviourSprite>()._wrong;
        if (!respuesta)
        {
            Vidas.transform.GetChild(intento).GetComponent<Image>().sprite = Vidas.transform.GetChild(intento).GetComponent<BehaviourSprite>()._wrong;
            if (intento < Vidas.transform.childCount)
            {
                intento++;
                Invoke("resetTemporal", 2);
            }

            if (intento == Vidas.transform.childCount)
                controlNavegacion.GoToLayout(11, 2);

            for (int x = 0; x < seleccion.Count; x++)
            {
                seleccion[x].intento = intento;
                seleccion[x].Vidas.transform.GetChild(intento - 1).GetComponent<Image>().sprite = seleccion[x].Vidas.transform.GetChild(intento - 1).GetComponent<BehaviourSprite>()._wrong;
            }            
        }
        else
        {
            controlPuntaje.IncreaseScore();
            controlNavegacion.Forward(2);
        }
                   
    }
    void resetTemporal()
    {
        Dardos.transform.GetChild(pregunta).GetComponent<M6L108Dardo>().hubicacion = null;
        Dardos.transform.GetChild(pregunta).GetComponent<RectTransform>().anchoredPosition = Dardos.transform.GetChild(pregunta).GetComponent<M6L108Dardo>().posicionInicial;
        //Dardos.transform.GetChild(intento - 1).gameObject.SetActive(false);
        
        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            var toggle = toggles.transform.GetChild(x).gameObject;
            toggle.GetComponent<Toggle>().isOn = false;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
            toggle.GetComponent<Toggle>().interactable = true;
            //toggle.GetComponent<Image>().raycastTarget = true;           

        }
    }
    public void ResetAll()
    {
        intento = 0;
        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            GameObject toggle = toggles.transform.GetChild(x).gameObject;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
            toggle.GetComponent<Toggle>().isOn = false;
            toggle.GetComponent<Toggle>().interactable = true;
            //toggle.GetComponent<Image>().raycastTarget = true;
        }
        for (int x = 0; x < Dardos.transform.childCount; x++)
        {
            GameObject dardo = Dardos.transform.GetChild(x).gameObject;
            dardo.SetActive(true);
            dardo.GetComponent<M6L108Dardo>().hubicacion = null;            
            dardo.GetComponent<RectTransform>().anchoredPosition = dardo.GetComponent<M6L108Dardo>().posicionInicial;            
        }
        for (int x = 0; x < Vidas.transform.childCount; x++)
            Vidas.transform.GetChild(x).GetComponent<Image>().sprite = Vidas.transform.GetChild(x).GetComponent<BehaviourSprite>()._default;
    }
}
