using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L11A269Seleccion : MonoBehaviour
{
    public L11A269Manager manager;
    public GameObject hermano;
    public bool inicio;
    private void Start()
    {
        manager = transform.GetComponentInParent<L11A269Manager>();
        //manager.toggles.Add(this.GetComponent<Toggle>());
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { manager.Calificar(this.gameObject); });
        //StartCoroutine(Inicio());
    }
    private void OnEnable()
    {
        if (inicio)
        {
            inicio = false;            
            StartCoroutine(Inicio());
        }
        else
            GetComponent<Animator>().Play("AbrirCofre");
    }
    public IEnumerator StateClificar(Image i,bool f)
    {
        yield return new WaitForSeconds(1);
        i.sprite = f ? transform.GetChild(0).GetComponent<BehaviourSprite>()._right : transform.GetChild(0).GetComponent<BehaviourSprite>()._wrong;

        if (!f)
        {
            yield return new WaitForSeconds(1);
            GetComponent<Animator>().Play("CerrarCofre");
            i.sprite = transform.GetChild(0).GetComponent<BehaviourSprite>()._default;

            yield return new WaitForSeconds(1);
            GetComponent<Animator>().Play("CofreQuieto");
        }        
    }    
    public IEnumerator Inicio()
    {
        GetComponent<Toggle>().interactable = false;
        GetComponent<Animator>().Play("AbrirCofre");

        yield return new WaitForSeconds(5);        
        GetComponent<Animator>().Play("CerrarCofre");
        
        yield return new WaitForSeconds(1);
        GetComponent<Toggle>().interactable = true;        
        GetComponent<Animator>().Play("CofreQuieto");        
    }
}
