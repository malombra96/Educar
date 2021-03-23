using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L40_Manager_aplico3 : MonoBehaviour
{
    public int v;
    private ControlAudio audio;
    private ControlNavegacion navegacion;
    private ControlPuntaje score;
    private Button btn_validar;
    public bool arranque;
    [HideInInspector] public GameObject X;
    [HideInInspector] public M8L40_ControlInsignias insigniascontrol;
     public List<InputField> inputFields = null;

    [Header("Enunciado : ")] public GameObject enunciado;
    [Header("Respuestas : ")] public List<string> respuestas;
    [Header("Resultado : ")] public GameObject resultnotice;
    [Header("lifes : ")] public M8L40_Lifes life;
    public bool primero;
    public enum tipo
    {
        normal,
        ultimo
    }

    [Header("tipo de aplico :")] public tipo aplico;

    public bool y = true;
    public Button informacion, pregunta;
    private void Awake()
    {
       
    }

    public void s() {
        y = false;
        GetComponent<Animator>().enabled = false;
        enunciado.SetActive(true);
        resultnotice.SetActive(false);
        
    }
    private void Start()
    {
        if (y) { 
            score = FindObjectOfType<ControlPuntaje>();
            navegacion = FindObjectOfType<ControlNavegacion>();
            audio = FindObjectOfType<ControlAudio>();
            insigniascontrol = FindObjectOfType<M8L40_ControlInsignias>();
            enunciado.SetActive(false);
            X = GameObject.Find("_X");
            X.SetActive(false);
            arranque = true;
            primero = true;
            v = 3;
        }
      
    }

    public void  inicio()
    {
        if (y)
        {
            enunciado.SetActive(true);
            btn_validar = enunciado.transform.GetChild(1).GetComponent<Button>();
            btn_validar.onClick.AddListener(Check);
            btn_validar.interactable = false;
            if (arranque)
            {
                for (int i = 3; i < enunciado.transform.childCount; i++)
                {
                    inputFields.Add(enunciado.transform.GetChild(i).GetComponent<InputField>());
                }
                arranque = false;
            }

            insigniascontrol.Refresh();

            foreach (var input in inputFields)
                input.onValueChanged.AddListener(delegate { Ready(); });
        }
        else {
            enunciado.SetActive(true);
        }
       
        
    }

    public void Check()
    {
        informacion.enabled = false;
        pregunta.enabled = false;
        int aciertos = 0;
        btn_validar.interactable = false;
        for (int i = 0; i < respuestas.Count; i++) {
            if (inputFields[i].text == respuestas[i])
            {
                inputFields[i].transform.GetChild(1).GetComponent<Text>().color = Color.green;
                aciertos++;
            }
            else
            {
                inputFields[i].transform.GetChild(1).GetComponent<Text>().color = Color.red;
            }
        }
           

        if (aciertos == respuestas.Count)
        {
            audio.PlayAudio(1);
            btn_validar.GetComponent<Image>().sprite = btn_validar.GetComponent<BehaviourSprite>()._right; 
            StartCoroutine(tiempo_right());
            
        }
        else
        {
           audio.PlayAudio(2);
           btn_validar.GetComponent<Image>().sprite = btn_validar.GetComponent<BehaviourSprite>()._wrong;
           
           StartCoroutine(tiempo_wrong());
           
        }
    }

    IEnumerator tiempo_right()
    {
        yield return new WaitForSeconds(2f);
        
        insigniascontrol.Rechange();
        enunciado.SetActive(false);
        score.IncreaseScore();
        resultnotice.SetActive(true);
        resultnotice.GetComponent<Image>().sprite = resultnotice.GetComponent<BehaviourSprite>()._right;
        yield return new WaitForSeconds(2f);
        switch (aplico)
        {
            case tipo.normal:
                navegacion.Forward();
                break;
            case tipo.ultimo:
                StartCoroutine(tiempo_final());
                break;
        }
        informacion.enabled = true;
        pregunta.enabled = true;

    }

    IEnumerator tiempo_wrong()
    {
        v--;
        
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < inputFields.Count; i++)
        {
            inputFields[i].contentType = InputField.ContentType.Standard;
            inputFields[i].transform.GetChild(1).GetComponent<Text>().color = Color.white;
            inputFields[i].text = "?";
            inputFields[i].contentType = InputField.ContentType.DecimalNumber;
        }
        enunciado.SetActive(false);
        X.SetActive(true);
        resultnotice.SetActive(true);
        resultnotice.GetComponent<Image>().sprite = resultnotice.GetComponent<BehaviourSprite>()._wrong;
        yield return new WaitForSeconds(2f);
        btn_validar.GetComponent<Image>().sprite = btn_validar.GetComponent<BehaviourSprite>()._default;
        
        
        if (v > 0) {
            enunciado.SetActive(true);
            life.Refresh();
            X.SetActive(false);
            resultnotice.SetActive(false);
            btn_validar.interactable = true;
            informacion.enabled = true;
            pregunta.enabled = true;
        }
        if (v == 0) {
            life.Refresh();
        }
        
       // 
        
    }

    public void Ready()
    {
        int avilitar = 0;
        foreach (var inputField in inputFields)
            if (inputField.text != "" && inputField.text != "?" )
                avilitar++;

        if (avilitar == inputFields.Count)
            btn_validar.interactable = true;
    }

    IEnumerator tiempo_final()
    {
        yield return  new WaitForSeconds(.2f);
        navegacion.Forward();
    }

    public void RestartAll()
    {
        if (primero) {
            for (int i = 0; i < inputFields.Count; i++)
            {
                inputFields[i].transform.GetChild(1).GetComponent<Text>().color = Color.white;
                inputFields[i].text = "?";
            }

            enunciado.SetActive(false);
            resultnotice.SetActive(false);
            life.contador = 3;
            insigniascontrol.control.contador = -1;
            informacion.enabled = true;
            pregunta.enabled = true;
            v = 3;
        }
        
    }
}
