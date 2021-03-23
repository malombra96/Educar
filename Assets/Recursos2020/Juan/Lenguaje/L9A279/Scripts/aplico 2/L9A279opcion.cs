using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class L9A279opcion : MonoBehaviour,IPointerClickHandler
{
    public bool correcto;
    [HideInInspector] public bool seleccionado;
    L9A279Mira mira;
    L9A279Manager manager;
    public GameObject otraOpcion;
    // Start is called before the first frame update
    void Start()
    {
        mira = FindObjectOfType<L9A279Mira>();
        manager = transform.GetComponentInParent<L9A279Manager>();
        manager.opciones.Add(this.gameObject);
    }
   
    public void OnPointerClick(PointerEventData eventData)
    {
        mira.seleccion(GetComponent<RectTransform>());        
        seleccionado = true;
        otraOpcion.GetComponent<L9A279opcion>().seleccionado = false;
        manager.controlAudio.PlayAudio(0);
        manager.activarBoton();
    }
}
