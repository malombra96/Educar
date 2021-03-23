using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class L7A281_input : MonoBehaviour, IPointerClickHandler
{
    public GameObject keyboard;
    public int caracteres;
    L7A281_managerInput _L7A281_managerInput;
    InputField _input;

    [HideInInspector] [Header("State Interaction")] public bool _isEnabled;
    [HideInInspector] [Header("State Empty")] public bool _isEmpty,op;
    [HideInInspector] [Header("State Answer")] public bool _isRight;

    [Header("Ingrese las respuestas correctas")] public List<string> respuestaCorrecta;


    void Awake()
    {
        _isEmpty = true;
        _isEnabled = true;
        _isRight = false;

        _L7A281_managerInput = FindObjectOfType<L7A281_managerInput>();

        _input = GetComponent<InputField>();

        _input.onValueChanged.AddListener(delegate { SetEmptyStandard(_input.text); });
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Application.isMobilePlatform)
        {
            keyboard.SetActive(true);
            keyboard.GetComponent<L7A281_keyboard>().contentTExt = gameObject.GetComponent<InputField>().textComponent;
            keyboard.GetComponent<L7A281_keyboard>()._name = gameObject.GetComponent<InputField>().text;
            op = true;
        }
    }


    public void SetStateEmpty(string s)
    {
        //print("s");
        _isEmpty = string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
        _L7A281_managerInput.SetStateValidarBTN();
    }

    /// Cuando no es contenido matemático, solo verifica que no este vacio
    public void SetEmptyStandard(string s)
    {
        //print("ss");
        _isEmpty = string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);

        _L7A281_managerInput.SetStateValidarBTN();


        
        
    }
    private void Update()
    {

        if (Application.isMobilePlatform)
        {
            if (keyboard.activeSelf && op)
            {
                gameObject.GetComponent<InputField>().text= keyboard.GetComponent<L7A281_keyboard>()._name;
            }
        }
    }
}
