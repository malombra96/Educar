using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlInteractividad : MonoBehaviour
{
    [Header("Cantidad de objetos interactuados")] public int currentInteraction;
    [Tooltip("Ingrese el total de objetos que debe interactuar")] 
    [Header("Total de objetos por interactuar")] [Range(0,40)] public int totalInteraction;
    
    [Header("Texto de porcentaje ")] public Text porcentaje;

    [HideInInspector][Header("Objetos Interactuables")] public List<BehaviourInteraccion> _objects; 

    public void SetInteraction(bool estado, GameObject objeto)
    {
        if (estado)
        {
            currentInteraction++;
            objeto.GetComponent<BehaviourInteraccion>()._state = false;
            var percent = (currentInteraction * 100) / totalInteraction;
            SetPercent(percent);
        }
    }

    public void SetPercent(int percent) => porcentaje.text = percent.ToString() + " %";
}
