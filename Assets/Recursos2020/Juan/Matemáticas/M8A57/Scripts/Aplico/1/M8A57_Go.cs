using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A57_Go : MonoBehaviour
{
    [Header("Control navegación : ")] public ControlNavegacion ControlNavegacion;
    [Header("numero del layout : ")] public int ir;
    //[Header("layout : ")] public GameObject padre;


    public enum tipo
    {
        button = 0,
        toggel = 1,
    }

    [field: Header("tipo de entrada :")] public tipo Tipo;

    void Start()
    {
        switch (Tipo)
        {
            case tipo.button:

                break;
            
            case tipo.toggel:
                gameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate{Go();});
                break;
        }
    }

    

    public void Go()
    {
        switch (Tipo)
        {
            case tipo.button:
                ControlNavegacion.GoToLayout(ir);
                //StartCoroutine(tiempo());
                break;
            
            case tipo.toggel:
                if (GetComponent<Toggle>().isOn)
                {
                    GetComponent<Toggle>().interactable = false;
                    GetComponent<Toggle>().isOn = false;
                    ControlNavegacion.GoToLayout(ir);
                    //StartCoroutine(tiempo());
                }
                break;
        }
    }

    //IEnumerator tiempo()
    //{
    //    yield return new WaitForSeconds(2f);
    //    padre.SetActive(false);
    //}
}
