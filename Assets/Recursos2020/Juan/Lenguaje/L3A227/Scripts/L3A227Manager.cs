using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class L3A227Manager : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;
    public List<L3A227Manager> managers;
    public GameObject start;
    public GameObject indefinido;
    public GameObject definido;
    [HideInInspector] public List<L3A227Drag> _drag;
    [HideInInspector] public L3A227Drop _drop;
    public Button botonLanzar;
    public bool mostrarInstruccion;    

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        
        for(int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).GetComponent<L3A227Drag>())            
                _drag.Add(transform.GetChild(0).GetChild(i).GetComponent<L3A227Drag>());
        }

        botonLanzar.onClick.AddListener(CargarTiro);      

        botonLanzar.interactable = false;
        transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(abrirInformacion);
        if (mostrarInstruccion)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }        
    }
    public void abrirInformacion()
    {
        controlAudio.PlayAudio(0);
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }

    }    

    void CargarTiro()
    {
        botonLanzar.interactable = false;
        controlAudio.PlayAudio(0);
        if (_drop.drag)
        {
            _drop.GetComponent<Image>().raycastTarget = false;
            _drop.drag.GetComponent<CanvasGroup>().blocksRaycasts = false;
            for (int i = 0; i < _drop.espacios.Count; i++)
            {
                if (!_drop.espacios[i].GetComponent<L3A227UltimaPos>().tengoDrag)
                {
                    _drop.drag.target = _drop.espacios[i];
                    _drop.drag.GetComponent<L3A227Drag>()._drop = _drop.espacios[i].gameObject;
                    _drop.drag = null;
                    break;
                }
            }            
        }        
    }

    public IEnumerator calificar()
    {
        yield return new WaitForSeconds(1);
        int index = 0;
        int right = 0;
        foreach (var drag in _drag)
        {
            index++;
            if (drag._drop) 
            {                
                drag.GetComponent<Image>().sprite = 
                    (drag._drop == drag.dropCorrecto) ?
                    drag.GetComponent<BehaviourSprite>()._right : 
                    drag.GetComponent<BehaviourSprite>()._wrong;
                                
                if (drag._drop == drag.dropCorrecto)
                    right++;

                if (drag.GetComponent<Image>().raycastTarget)
                {
                    drag.GetComponent<Image>().raycastTarget = false;
                    controlAudio.PlayAudio(drag._drop == drag.dropCorrecto ? 1 : 2);
                }
                if (index > 4)
                {
                    definido.GetComponent<Image>().sprite=
                        (drag._drop == drag.dropCorrecto) ?
                        definido.GetComponent<BehaviourSprite>()._right :
                        definido.GetComponent<BehaviourSprite>()._wrong;
                }
                else
                {
                    indefinido.GetComponent<Image>().sprite =
                        (drag._drop == drag.dropCorrecto) ?
                        indefinido.GetComponent<BehaviourSprite>()._right :
                        indefinido.GetComponent<BehaviourSprite>()._wrong;
                }
            }            
        }
        
        if (right == _drop.espacios.Count)
        {
            controlPuntaje.IncreaseScore();
            start.GetComponent<Image>().sprite = start.GetComponent<BehaviourSprite>()._default;
            foreach(var m in managers)
            {
                for(int x=0; x < m.start.transform.parent.childCount; x++)
                {
                    if (x == start.transform.GetSiblingIndex())
                        m.start.transform.parent.GetChild(x).GetComponent<Image>().sprite = start.GetComponent<BehaviourSprite>()._default;
                }
            }
        }

        if (_drop.x == _drop.espacios.Count)
            controlNavegacion.Forward(2);                    
        else
            _drop.GetComponent<Image>().raycastTarget = true;
    }
    public void resetAll()
    {
        for (int x = 0; x < start.transform.parent.childCount; x++)        
            start.transform.parent.GetChild(x).GetComponent<Image>().sprite = start.GetComponent<BehaviourSprite>()._selection;       

        definido.GetComponent<Image>().sprite = definido.GetComponent<BehaviourSprite>()._default;
        indefinido.GetComponent<Image>().sprite = indefinido.GetComponent<BehaviourSprite>()._default;

        if (mostrarInstruccion)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        foreach (var drag in _drag)
        {
            drag.GetComponent<RectTransform>().anchoredPosition = drag.posDefault;
            drag.GetComponent<Image>().sprite = drag.GetComponent<BehaviourSprite>()._default;
            drag.GetComponent<Image>().raycastTarget = true;
            drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            drag._drop = null;
        }
        if (_drop)
        {
            _drop.x = 0;
            _drop.GetComponent<Image>().raycastTarget = true;
            _drop.drag = null;

            for (int i = 0; i < _drop.espacios.Count; i++)            
                _drop.espacios[i].GetComponent<L3A227UltimaPos>().tengoDrag = false;            
        }
        
    }
}
