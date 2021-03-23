using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M4A114ObjetoMovible : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    M4A114ControlSeleccionYTexto controlSeleccionYTexto;
    Canvas canvas;
    public Animator animator;
    [HideInInspector] public bool indrop;
    public bool individial;
    public bool click;
    [HideInInspector] public Vector2 posDefault;
    public int numero;
    public string animacionPlay;
    [HideInInspector] public int x;
    // Start is called before the first frame update
    void Start()
    {
        controlSeleccionYTexto = FindObjectOfType<M4A114ControlSeleccionYTexto>();
        controlSeleccionYTexto.drags.Add(this);

        posDefault = GetComponent<RectTransform>().anchoredPosition;
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!click)
            GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!click)
        {
            if (!animator && !individial)
            {
                controlSeleccionYTexto.controlAudio.PlayAudio(0);
                GetComponent<CanvasGroup>().alpha = 0.8f;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                transform.SetAsLastSibling();
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!click)
        {
            if (!animator && !individial)
            {
                GetComponent<CanvasGroup>().alpha = 1;
                GetComponent<CanvasGroup>().blocksRaycasts = true;

                if (!indrop)
                    GetComponent<RectTransform>().anchoredPosition = posDefault;
                else
                    controlSeleccionYTexto.activarOpciones(numero);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var other = eventData.pointerDrag;
        if (other.GetComponent<M4A114ObjetoMovible>())
        {
            if (indrop)
            {
                if (other.GetComponent<M4A114ObjetoMovible>().indrop)
                {
                    Vector2 tempPos = other.GetComponent<RectTransform>().anchoredPosition;
                    other.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    GetComponent<RectTransform>().anchoredPosition = tempPos;
                }
                else
                {
                    other.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    GetComponent<RectTransform>().anchoredPosition = posDefault;
                    indrop = false;
                }
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!animator && !individial)
        {
            controlSeleccionYTexto.controlAudio.PlayAudio(0);
            if (!click)
            {
                indrop = false;
                GetComponent<RectTransform>().anchoredPosition = posDefault;
            }
            else
            {                
                if (!indrop)
                {
                    foreach (var drop in controlSeleccionYTexto.drops)
                    {
                        if (!drop._drag)
                        {
                            indrop = true;
                            drop._drag = gameObject;
                            GetComponent<RectTransform>().anchoredPosition = drop.GetComponent<RectTransform>().anchoredPosition;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var drop in controlSeleccionYTexto.drops)
                    {
                        if (drop._drag == gameObject)
                        {
                            indrop = false;
                            drop._drag = null;
                            GetComponent<RectTransform>().anchoredPosition = posDefault;
                            break;
                        }
                    }
                }
            }
            controlSeleccionYTexto.activarOpciones(numero);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (animator)
        {
            if (individial)
            {
                if (collision.GetComponent<Animator>())
                {
                    x++;
                    collision.GetComponent<Animator>().Play(animacionPlay);
                }

                if (x == 2)
                {
                    GetComponent<Rigidbody2D>().simulated = false;
                    controlSeleccionYTexto.activarOpciones();
                }
            }
            else
            {
                animator.GetComponent<Animator>().Play(animacionPlay);
                GetComponent<Rigidbody2D>().simulated = false;
                controlSeleccionYTexto.activarOpciones();
            }           
        }        
    }
}
