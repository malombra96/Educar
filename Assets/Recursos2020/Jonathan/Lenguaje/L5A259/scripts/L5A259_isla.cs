using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A259_isla : MonoBehaviour
{
    public L5A259_barco barco;
    public GameObject ejercicio;
    public bool habilitado,op;
    public GameObject elemento;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activar() {
        barco.mover = false;
        elemento.GetComponent<Image>().sprite = elemento.GetComponent<BehaviourSprite>()._selection;
        StartCoroutine(x());
    }

    IEnumerator x() {
        yield return new WaitForSeconds(3f);
        ejercicio.SetActive(true);
    }
    

    public void Calificar(bool value) {
        if (op) {
            op = false;
        }
        if (value)
        {
            //print("s");
            elemento.GetComponent<Image>().sprite = elemento.GetComponent<BehaviourSprite>()._right;
        }
        else {
            elemento.GetComponent<Image>().sprite = elemento.GetComponent<BehaviourSprite>()._wrong;
        }
        barco.mover = true;
        habilitado = false;
    }
}
