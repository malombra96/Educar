using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L5A260TextoSeleccionado : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    L5A260ManagerSeleccionTexto managerSeleccionTexto;

    [HideInInspector] public bool isOn;
    public bool correcto;

    // Start is called before the first frame update
    void Start()
    {
        managerSeleccionTexto = GetComponentInParent<L5A260ManagerSeleccionTexto>();
        if (managerSeleccionTexto.texts.Count == 0)
            gameObject.AddComponent<BehaviourPuntero>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (managerSeleccionTexto.texts.Count > 0)
        {
            if (!isOn)
                GetComponent<Text>().color = managerSeleccionTexto._ove;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (managerSeleccionTexto.texts.Count > 0)
        {
            if (!isOn)
                GetComponent<Text>().color = managerSeleccionTexto._default;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (managerSeleccionTexto.texts.Count > 0)
        {            
            GetComponent<Text>().color = managerSeleccionTexto._seleccion;
            isOn = true;            
        }
        StartCoroutine(managerSeleccionTexto.calificar(correcto));
    }
}
