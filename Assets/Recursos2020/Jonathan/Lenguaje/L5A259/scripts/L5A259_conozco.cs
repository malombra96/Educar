using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A259_conozco : MonoBehaviour
{
    public int aplico, dese;
    [Header("Cantidad de objetos interactuados")] public int currentInteraction;
    [Tooltip("Ingrese el total de objetos que debe interactuar")]
    [Header("Total de objetos por interactuar")] [Range(0, 40)] public int totalInteraction;

    [Header("Texto de porcentaje ")] public Text porcentaje;

    [HideInInspector] [Header("Objetos Interactuables")] public List<L5A259_interaccion> _objects;
    string m_PlayerName;

    public void SetInteraction(bool estado, GameObject objeto)
    {
        if (estado)
        {
            currentInteraction++;
            objeto.GetComponent<L5A259_interaccion>()._state = false;
            var percent = (currentInteraction * 100) / totalInteraction;
            SetPercent(percent);
            PlayerPrefs.SetInt("p", percent);
            print("sdasdsa: " + PlayerPrefs.GetInt("p")); 
        }
    }

    public void SetPercent(int percent) {
       
        
    }

    public void aplico1() {
        aplico = 1;
        PlayerPrefs.SetInt("aplico1",aplico);
        print("a"+aplico);
    }
    public void desempeño1() {
        dese = 1;
        PlayerPrefs.SetInt("desmepeño1", dese);
        print("d" + dese);
    }
}
