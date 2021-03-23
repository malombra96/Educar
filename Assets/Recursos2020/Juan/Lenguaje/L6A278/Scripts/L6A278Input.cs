using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L6A278Input : MonoBehaviour, IPointerClickHandler
{
    public L5A258ManagerTextoPrint managerTextoPrint;
    public GameObject inputField;
    
    void Update()
    {
        if (Application.isMobilePlatform && inputField)
        {
            if (inputField.activeSelf)            
                GetComponent<InputField>().text = inputField.GetComponent<InputField>().text;            
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        managerTextoPrint.controlAudio.PlayAudio(0);
        if (Application.isMobilePlatform)
        {
            L5A257BehaviourKeyBoard teclado = managerTextoPrint.teclado.GetComponent<L5A257BehaviourKeyBoard>();
            teclado.gameObject.SetActive(true);
            teclado._name = GetComponent<InputField>().text;

            if (inputField)
            {
                managerTextoPrint.inputSecundario(gameObject);
                teclado._InputName = inputField.GetComponent<InputField>();                
            }
            else
                teclado._InputName = GetComponent<InputField>();            
        }
    }
}
