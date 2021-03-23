using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
public class M9A63BehaviourInputField : MonoBehaviour,IPointerClickHandler
{
    M9A63ManagerInputField _managerInputField;
    //InputField _input;
    public bool tecladoNumerico;
    [HideInInspector] [Header("State Interaction")] public bool _isEnabled;
    [HideInInspector] [Header("State Empty")] public bool _isEmpty;
    [HideInInspector] [Header("State Answer")] public bool _isRight;
    public GameObject infinito;
    [Header("Ingrese las respuestas correctas")] public List<string> respuestaCorrecta;


    void Awake()
    {
        _isEmpty = true;
        _managerInputField = FindObjectOfType<M9A63ManagerInputField>();        
    }   
    void Update()
    {
        if (GetComponent<InputField>().text != "" && _isEmpty)
        {
            _isEmpty = false;
            _managerInputField.SetStateValidarBTN();
        }
        if (infinito && FindObjectOfType<M9A63ManagerInputField>().gameObject.activeSelf)
            infinito.SetActive(true);                
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        _managerInputField._controlAudio.PlayAudio(0);
        _managerInputField._lightBox.GetComponent<M9A63Teclado>()._managerInputField = _managerInputField;
        _managerInputField._lightBox.GetComponent<M9A63Teclado>().inputField = GetComponent<InputField>();
        _managerInputField._lightBox.GetComponent<M9A63Teclado>().Activar(tecladoNumerico);        
    }   
}
