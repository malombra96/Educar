using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M8L40_Input_Numpad : MonoBehaviour, IPointerClickHandler
{
    [Header("LightBox :")] public GameObject _lightBox;

    [HideInInspector] public ControlAudio audio;

    public bool calificado;

    public Button validar;
     [Header("State Empty")] public bool _isEmpty;

    public M8L40_group grupo;


    private void Awake()
    {
        audio = FindObjectOfType<ControlAudio>();
        gameObject.GetComponent<InputField>().onValueChanged.AddListener(delegate { SetStateEmpty(gameObject.GetComponent<InputField>().text); });
        _isEmpty = true;

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Application.isMobilePlatform  && GetComponent<InputField>().interactable)
        {
            audio.PlayAudio(0);

            ManagerDisplay _managerDisplay = _lightBox.transform.GetChild(0).GetChild(0).GetComponent<ManagerDisplay>();

            _managerDisplay.currentInput = GetComponent<InputField>();
            _managerDisplay.limiteCaracteres = GetComponent<InputField>().characterLimit;

            _lightBox.SetActive(true);
            _lightBox.GetComponent<Animator>().Play("NumPad_in");
        }
    }
    public void SetStateEmpty(string s)
    {
        if (grupo.contador > -1) {
            if (s == "?" || s == "??" || s == "" || s == " " || s== "? " || s == " ?" || s== "  " || s == " 1" || s == " 2" || s == " 3" || s == " 4" || s == " 5" || s == " 6" || s == " 7" || s == " 8" || s == " 9" || s == " 0"||
                s == "?1" || s == "?2" || s == "?3" || s == "?4" || s == "?5" || s == "?6" || s == "?7" || s == "?8" || s == "?9" || s == "?0")
            {
                if (!_isEmpty) {
                    grupo.SetStateValidarBTN(false);
                    _isEmpty = true;
                }
                
                //grupo.contador--;
            }
            else
            {
                if (_isEmpty) {
                    grupo.SetStateValidarBTN(true);
                    _isEmpty = false;
                }
                
                //grupo.contador++;

            }
            
        }
        
        

    }
}
