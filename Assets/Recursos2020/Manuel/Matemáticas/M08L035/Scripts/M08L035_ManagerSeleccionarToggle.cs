﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class M08L035_ManagerSeleccionarToggle : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;
    ControlPuntaje _controlPuntaje;

    [HideInInspector] public List<M08L035_BehaviourToggleGroup> _groupToggle;
    
    int scoreTotal;
    int rightTotal; // Cantidad de toggles correctos, solo para calificacion individual
    int evaluated;

    #region Configure Parametres SelectionToggle
    
    public enum TypeValidation
    {
        inmediata,
        button
    }

    [Tooltip("Seleccione el tipo de validacion")] [Header("Tipo de Validacion")] public TypeValidation _typeVal;
    public Button _validarBTN;

    public enum TypeSelection
    {
        onexgroup,
        variousxgroup
    }

    [Tooltip("Seleccione el tipo de Selection Toggle")] [Header("Tipo de Selection")] public TypeSelection _TypeSelect;
    [Tooltip("Determine la cantidad de clicks/selecciones")] [Range(1,10)] public int _options;

    public enum TypeCalification 
    {
        individual,
        grupo
    }

    [Tooltip("Seleccione el tipo de calificaccion")] [Header("Tipo de Calificación")] public TypeCalification _TypeCalification;

    public enum TypeQualify
    {
        onlySelection,
        all
    }
    [Tooltip("Seleccione el tipo de validacion en las respuestas")] [Header("¿Como calificar?")] public TypeQualify _TypeQualify;
    
    #endregion
    
    [Header("Player")] public Animator _player;
    [Header("Goalkeeper")] public Animator _goalkeeper;
    private void Start()
    {
        evaluated = 0;
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        SetValidar();
        StartCoroutine(StateBtnValidar());
        
    }

    
    /// Determina el estado del boton validar
    void SetValidar()
    {
        switch (_typeVal)
        {
            case TypeValidation.button:

                _validarBTN.gameObject.SetActive(true);
                _validarBTN.onClick.AddListener(EvaluateGroupsToggle);
                    
                break;

            case TypeValidation.inmediata:

               _validarBTN.gameObject.SetActive(false);

                break;
        }
    }

    /// Determina si la seleccion de toggles es la necesaria para habilitar el boton Validar
    public IEnumerator StateBtnValidar()
    {
        yield return new WaitForSeconds(.2f);
        int n = 0;

        switch (_TypeSelect)
        {
            case TypeSelection.onexgroup:

            foreach (var group in _groupToggle)
                    if(group._dictionarySelection._toggle.Count == 1)
                         n++;

                break;

            case TypeSelection.variousxgroup:

                foreach (var group in _groupToggle)
                    if(group._dictionarySelection._toggle.Count == _options)
                         n++;

                break;
        }

        _validarBTN.interactable = (n==_groupToggle.Count);  
        
    }

    void EvaluateGroupsToggle()
    {
        _validarBTN.interactable = false;

        int[] rights = new int[_groupToggle.Count];
        int[] a = new int[_groupToggle.Count];
        int[] b = new int[_groupToggle.Count];

        scoreTotal = 0;

        for (int j = 0; j < _groupToggle.Count; j++)
        {
            a[j] = 0;
            b[j] = _groupToggle[j]._sizeCorrect;

            for (int i = 0; i < _groupToggle[j].transform.childCount; i++)
            {
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

                Image img = _groupToggle[j].transform.GetChild(i).GetComponent<Image>();
                bool isON = _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().isOn;
                bool state = _groupToggle[j].transform.GetChild(i).GetComponent<M08L035_BehaviourToggle>().isRight;

                if(isON && state)
                    a[j]++;

                if(_TypeQualify == TypeQualify.all)
                    SetSpriteAnswer(img,state);
                else
                    if(isON)
                        SetSpriteAnswer(img,state);      
                
            }
            
            if(_TypeCalification == TypeCalification.grupo)
            {
                if(a[j]==b[j])
                    rights[j]++;

                scoreTotal += rights[j];
            }
            else
            {
                rights[j] = a[j];
                scoreTotal += rights[j];
            }

            rightTotal += b[j];
        }

        SetPuntaje();
    }

    public void EvaluateGroupsToggleImmediately(M08L035_BehaviourToggleGroup completed)
    {
        bool answer = false;
        int indexGroup = _groupToggle.IndexOf(completed);

        for (int i = 0; i < _groupToggle[indexGroup].transform.childCount; i++)
            _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

        Toggle t = _groupToggle[indexGroup].GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
        answer = (t.isOn && t.GetComponent<M08L035_BehaviourToggle>().isRight);

        //print($"Select {t.name} is {answer}");

        StartCoroutine(SetAnimations(completed,t.name.Split('_')[1],answer));

    }

    IEnumerator SetAnimations(M08L035_BehaviourToggleGroup completed,string dir,bool answer)
    {
        yield return new WaitForSeconds(0);

        _player.SetInteger("direction", dir=="Left"? 1 : dir=="Center"? 2 : dir=="Right"? 3 : 0);

        yield return new WaitForSeconds(2);

        if(dir == "Left")
            _goalkeeper.SetInteger("cover", answer? 3:1);
        else if(dir == "Center")
            _goalkeeper.SetInteger("cover", answer? 0:2);
        else if(dir == "Right")
            _goalkeeper.SetInteger("cover", answer? 1:3);


        SetSpriteAnswer(completed);


    }

    void SetSpriteAnswer(M08L035_BehaviourToggleGroup completed)
    {
        bool answer = false;
        int indexGroup = _groupToggle.IndexOf(completed);

        for (int i = 0; i < _groupToggle[indexGroup].transform.childCount; i++)
        {
            Image img = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Image>();
            bool state = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<M08L035_BehaviourToggle>().isRight;
            bool isON = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().isOn;

            if(_TypeQualify == TypeQualify.all)
                    SetSpriteAnswer(img,state);
                else
                    if(isON)
                        SetSpriteAnswer(img,state);

        }

        Toggle t = _groupToggle[indexGroup].GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
        answer = (t.isOn && t.GetComponent<M08L035_BehaviourToggle>().isRight);

        SetPuntaje(answer);
    }

    //// Instancia el puntaje y audio, al validar por boton
    void SetPuntaje()
    {
        if(_TypeCalification == TypeCalification.grupo)
            _controlAudio.PlayAudio(scoreTotal ==_groupToggle.Count? 1:2);
        else
            _controlAudio.PlayAudio(scoreTotal == rightTotal? 1:2);
        
        _controlPuntaje.IncreaseScore(scoreTotal);
        _controlNavegacion.Forward(2);
    }

    /// Instancia el puntaje y audio, al seleccionar validacion inmediata
    void SetPuntaje(bool answer)
    {
        evaluated++;

        if(answer)
        {
            _controlAudio.PlayAudio(1);
            _controlPuntaje.IncreaseScore();
        }
        else
        {
            _controlAudio.PlayAudio(2);
        }

        if(evaluated == _groupToggle.Count)
            _controlNavegacion.Forward(2); 
        
    }

    /// <summary>
    /// Recibe la imagen y respuesta, para asignar el correspondiente sprite respuesta
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    public void SetSpriteAnswer(Image i,bool state)
    {
        i.sprite = state? 
            i.GetComponent<BehaviourSprite>()._right : 
            i.GetComponent<BehaviourSprite>()._wrong;
    }

    /// <summary>
    /// Asigna sprite default de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    void SetDefaultSprite(Image i)
    {
        i.sprite = i.GetComponent<BehaviourSprite>()._default;
    }

    public void ResetSeleccionarToggle()
    {
        _validarBTN.interactable = true;
        evaluated=0;

        _player.SetInteger("direction",0);
         _goalkeeper.SetInteger("cover",0);

        for (int j = 0; j < _groupToggle.Count; j++)
        {
            for (int i = 0; i < _groupToggle[j].transform.childCount; i++)
            {
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().interactable = true;
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().isOn = false;

                Image img = _groupToggle[j].transform.GetChild(i).GetComponent<Image>();
                SetDefaultSprite(img); 
                
            }
            
        }
    }
    
}
