using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A255_resultado : MonoBehaviour
{
    public ControlPuntaje controlPuntaje;
    public GameObject bien, mal,man,woman;
    public L4A255_Inicio inicio;
    public GameObject ControlNavegacion;
    public List<GameObject> todo;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var t in todo) {
            t.SetActive(false);
        }
        StartCoroutine(x());
    }


    IEnumerator x() {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        ControlNavegacion.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (controlPuntaje._rightAnswers == controlPuntaje.questions)
        {
            bien.SetActive(true);
            if (inicio.cadena == "ToggleMan")
            {
                man.SetActive(true);
                man.GetComponent<Image>().sprite = man.GetComponent<BehaviourSprite>()._right;
            }
            //else {
            //    man.SetActive(false);
            //}
            if (inicio.cadena == "ToggleWoman")
            {
                woman.SetActive(true);
                woman.GetComponent<Image>().sprite = woman.GetComponent<BehaviourSprite>()._right;
            }
            //else {
            //    woman.SetActive(false);
            //}
           
            
        }
        else {
            mal.SetActive(true);
            if (inicio.cadena == "ToggleMan")
            {
                man.SetActive(true);
                man.GetComponent<Image>().sprite = man.GetComponent<BehaviourSprite>()._wrong;
            }
            if (inicio.cadena == "ToggleWoman")
            {
                woman.SetActive(true);
                woman.GetComponent<Image>().sprite = woman.GetComponent<BehaviourSprite>()._wrong;
            }
        }
    }
}
