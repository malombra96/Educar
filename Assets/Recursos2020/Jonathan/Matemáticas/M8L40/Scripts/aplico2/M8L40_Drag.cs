using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M8L40_Drag  : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
   // [SerializeField] private M10L001_MANAGERDrag _managerDrag;

     public Vector3 posInicial;
    [HideInInspector] public Vector2 posDefault;
    private int indexSibling;
    private float Xmin, Ymin, Xmax, Ymax;

    [Header("State Drop")] public bool stateDrop = false;
    public ControlAudio controlAudio;
    public bool op;
   // [Header("Drop Correct")] public GameObject _dropCorrect;


    private void Start()
    {
     //   _managerDrag.DragList.Add(gameObject);
        posInicial = GetComponent<RectTransform>().anchoredPosition;
        posInicial = transform.position;
        op = true;

    }
    private void Update()
    {
        //GetComponent<Image>().SetNativeSize();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (op) {
            GetDimensionDisplay();

            posDefault = GetComponent<RectTransform>().anchoredPosition; // Guarda la position default al comenzar el drag 

            GetComponent<CanvasGroup>().blocksRaycasts = false;

            stateDrop = false;
            controlAudio.PlayAudio(0);
        }
        

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (op) {
            transform.position = eventData.position; // Posiciona el objeto sobre la posicion del puntero

            Vector3 pos = transform.position;

            if (pos.x < Xmin || pos.x > Xmax || pos.y < Ymin || pos.y > Ymax) // Evalua posicion del objeto con los limites
            {
                pos = new Vector2(Mathf.Clamp(pos.x, Xmin, Xmax), Mathf.Clamp(pos.y, Ymin, Ymax)); // Limita el movimiento dentro del canvas
                transform.position = pos; // Actualiza la posicion
            }
        }
       
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {

        
       // _managerDrag._inDrag = null; // Limpia el temporal cuando termina el drag
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (!stateDrop)
        {
           // posInicial.z = -9.5f;
          //  GetComponent<RectTransform>().anchoredPosition = posInicial; // Retorna el objeto a su posicion inicial
          transform.position = posInicial;
           // transform.SetSiblingIndex(indexSibling); // Retorna el objeto a su index inicial
        }
        
    }
    private void GetDimensionDisplay()
    {
        if (op) {
            Xmin = (transform.GetComponent<RectTransform>().rect.width / 2);
            Xmax = Display.main.systemWidth - (transform.GetComponent<RectTransform>().rect.width / 2);
            Ymin = (transform.GetComponent<RectTransform>().rect.height / 2);
            Ymax = Display.main.systemHeight - (transform.GetComponent<RectTransform>().rect.height / 2);
        }
       
    }
    
}
