using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M7A76CartaSeleccionada : MonoBehaviour,IPointerClickHandler
{
    public GameObject carataPareja;
    [HideInInspector] public M7A76ControlCartas controlCartas;    
    [HideInInspector] public Animator animator;
    bool mover;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator tiempoCambio(float t)
    {
        yield return new WaitForSeconds(t);        
        StartCoroutine(SpriteCambio(GetComponent<BehaviourSprite>()._default));        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {        
        if (controlCartas.mover && mover)
        {
            mover = false;
            print("precionado");            
            StartCoroutine(controlCartas.cartaSeleccionada(this.gameObject));
            StartCoroutine(SpriteCambio(GetComponent<BehaviourSprite>()._selection));            
        }
    }
    public IEnumerator SpriteCambio(Sprite spriteCambio)
    {
        animator.SetBool("Voltear", true);

        yield return new WaitForSeconds(1f);
        GetComponent<Image>().sprite = spriteCambio;
        animator.SetBool("Voltear", false);

        yield return new WaitForSeconds(1f);
        if (GetComponent<Image>().sprite == GetComponent<BehaviourSprite>()._default)
            mover = true;

    }
}
