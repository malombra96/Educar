using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L5A257Input : MonoBehaviour,IPointerClickHandler
{
    public L5A257ManagerTextoPrint managerTextoPrint;
    public GameObject inputField;
    public bool grande;
    public GameObject pantalla2;
    public GameObject pantalla3;

    void Update()
    {
        if (Application.isMobilePlatform && inputField)
        {
            if (inputField.activeSelf)
            {
                if (grande)
                    GetComponent<InputField>().text = inputField.transform.GetChild(0).GetComponent<InputField>().text;
                else
                    GetComponent<InputField>().text = inputField.GetComponent<InputField>().text;
            }
            //inputField.SetActive(managerTextoPrint.teclado.activeSelf);
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
                inputField.gameObject.SetActive(true);
                if (grande)
                {                    
                    inputField.transform.GetChild(0).GetComponent<InputField>().text = GetComponent<InputField>().text;
                    teclado._InputName = inputField.transform.GetChild(0).GetComponent<InputField>();
                }
                else
                {                    
                    inputField.GetComponent<InputField>().text = GetComponent<InputField>().text;
                    teclado._InputName = inputField.GetComponent<InputField>();
                }
            }
            else
                teclado._InputName = GetComponent<InputField>();
            
            if (pantalla2)
                pantalla2.SetActive(false);
            if (pantalla3)
                pantalla3.SetActive(false);
        }
    }
    public void apagar()
    {
        if(inputField)
            inputField.SetActive(false);
    }

}
