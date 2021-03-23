using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M4A114OpcionesInput : MonoBehaviour,IPointerClickHandler
{
    [HideInInspector] public M4A114ControlSeleccionYTexto ControlSeleccionYTexto;
    public List<string> numeroCorrecto;
    private void Update()
    {
        if (GetComponent<InputField>())
        {
            if (GetComponent<InputField>().text != "")
                ControlSeleccionYTexto.activarValidar();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<InputField>().interactable)
            ControlSeleccionYTexto.controlAudio.PlayAudio(0);
        if (Application.isMobilePlatform)
        {            
            ManagerDisplay teclado = ControlSeleccionYTexto.Teclado.transform.GetChild(0).GetChild(0).GetComponent<ManagerDisplay>();

            teclado.currentInput = GetComponent<InputField>();
            teclado.limiteCaracteres = GetComponent<InputField>().characterLimit;

            ControlSeleccionYTexto.Teclado.SetActive(true);
            ControlSeleccionYTexto.Teclado.GetComponent<Animator>().Play("NumPad_in");
        }
    }
}
