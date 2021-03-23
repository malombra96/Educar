using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M7A76Opciones : MonoBehaviour,IPointerClickHandler
{
    M7A76GameControler gameControler;
    public bool respuestaCorrecta;
    [HideInInspector]public Animator animator;

    public GameObject flecha;
    public Transform shoop;
    Rigidbody2D rig;
    float next, disparar;
    bool disparo;

    private void Awake()
    {
        gameControler = FindObjectOfType<M7A76GameControler>();
        if(GetComponent<Animator>())
            animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        next = Time.time;
        if (disparo && Time.time >= disparar)
        {
            disparo = false;
            disparar = Time.time + 2f;
            GameObject Flecha = Instantiate(flecha, shoop);
            rig = Flecha.GetComponent<Rigidbody2D>();
            rig.AddForce(shoop.right * 0.1f, ForceMode2D.Impulse);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameControler.seguirDisparando)
        {
            disparo = true;
            if(GetComponent<BehaviourSprite>())
                GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;

            else
                transform.GetChild(0).GetComponent<Image>().sprite= transform.GetChild(0).GetComponent<BehaviourSprite>()._selection;

        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Animator>())
            animator.SetBool("Explotar", true);

        gameControler.Calificar(respuestaCorrecta);        
        Destroy(collision.gameObject);

        if (gameControler.recurso == M7A76GameControler.Recurso.globo)
            transform.GetChild(0).GetComponent<Image>().sprite = respuestaCorrecta ? transform.GetChild(0).GetComponent<BehaviourSprite>()._right :
                transform.GetChild(0).GetComponent<BehaviourSprite>()._wrong;
        else
            GetComponent<Image>().sprite = respuestaCorrecta ? GetComponent<BehaviourSprite>()._right : 
                GetComponent<BehaviourSprite>()._wrong;
    }
}
