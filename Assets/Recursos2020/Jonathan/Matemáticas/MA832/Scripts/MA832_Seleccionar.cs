using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MA832_Seleccionar : MonoBehaviour
{

    [Header("Control Seleccionar :")] public GameObject control;

    [Header("Validar :")] public Button btn_validar;

    [Header("Toggle correcpondiente :")] public Toggle cable;

    [Header("NavBar :")] public GameObject NavBar;
    
    [Header("Cantidad de entradas :")] public int inputs;

    [HideInInspector] public ControlPuntaje puntaje;

    private double muestra; 
    void Start()
    {
        btn_validar.interactable = false;
        btn_validar.onClick.AddListener(Ready);
        control = GameObject.Find("ControlSeleccion");
        NavBar.SetActive(false);
        puntaje = FindObjectOfType<ControlPuntaje>();
        muestra = puntaje._rightAnswers;
    }

    public void Ready()
    {
        StartCoroutine(tiempo());
    }

    public void Check()
    {

        if ((muestra + inputs) == puntaje._rightAnswers)
        {
            cable.interactable = false;
            cable.GetComponent<Image>().sprite = cable.GetComponent<BehaviourSprite>()._right;
            control.GetComponent<MA832_ControlSeleccionar>().hits++;
            control.GetComponent<MA832_ControlSeleccionar>().Check();
            NavBar.SetActive(true);
            btn_validar.GetComponent<MA832_Go>().Go();
        }
        else
        {
            StartCoroutine(tiemp());
            control.GetComponent<MA832_ControlSeleccionar>().mistakes++;
            control.GetComponent<MA832_ControlSeleccionar>().Marker();
            
        }
    }

    IEnumerator tiemp()
    {
        yield return new  WaitForSeconds(1f);

        NavBar.SetActive(true);
        GetComponent<ManagerInputField>().resetAll();
        btn_validar.GetComponent<MA832_Go>().Go();
    }

    IEnumerator tiempo()
    {
        yield return new  WaitForSeconds(1f);
        Check();
    }
}
