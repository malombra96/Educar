using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class M6L108BehaviourDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    M6L108ManagerDragDrop _managerDragDrop;

    public List<GameObject> grupoCorrecto;

    #region DefaultState
    //[HideInInspector] public bool grupo;
    [HideInInspector] public Vector2 _defaultPos;

    #endregion

    [HideInInspector] public Vector2 _currentPos;

    [Tooltip("Arrastre aquí los drop correctos")] [Header("Correct Drop")] public List<M6L108BehaviourDrop> _DropRight;
    /*[HideInInspector]*/ [Header("Current Drop")] public GameObject _drop;

    [Header("States")]
    [HideInInspector] public bool inDrop;
    /*[HideInInspector]*/
    public bool swap;


    [Header("Math Setup")]
    [Tooltip("Ingrese el index correspondiente a la fila o columna")] public int _row;


    [Header("Dragging on pivot")] public bool _pivotDraging;

    int indexSibling;
    RectTransform _rectTransform;
    Image _image;

    private void Awake()
    {
        inDrop = false;
        swap = false;

        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        _defaultPos = _rectTransform.anchoredPosition;

        UpdateCurrentPosition();

        _managerDragDrop = FindObjectOfType<M6L108ManagerDragDrop>();
        _managerDragDrop._drags.Add(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _managerDragDrop._controlAudio.PlayAudio(0);

        inDrop = false;
        UpdateCurrentPosition();

        indexSibling = _rectTransform.GetSiblingIndex(); // Obtiene el index layout respecto a los demas objetos del mismo nivel
        var max = _rectTransform.parent.transform.childCount; // Obtiene el index max
        _rectTransform.SetSiblingIndex(max - 1); // Posiciona el objeto sobre todos los demas

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        SetSpriteState(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_pivotDraging)
            _rectTransform.anchoredPosition += eventData.delta / _managerDragDrop.canvas.scaleFactor;
        else
        {
            float Y = Display.main.systemHeight / 2;
            float X = Display.main.systemWidth / 2;

            Vector2 newEventData = new Vector2((eventData.position.x - X), (eventData.position.y - Y)) / _managerDragDrop.canvas.scaleFactor;
            _rectTransform.anchoredPosition = newEventData;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("EndDrag"+name);

        if (!swap)
        {
            if (!inDrop)
            {
                //print(name+"return");
                _rectTransform.anchoredPosition = _currentPos;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                SetSpriteState(false);
            }
            else
            {
                //print(name+"Did drop");
                //inDrop = false;
                UpdateCurrentPosition();
                SetSpriteState(false);
                Invoke("verificar_grupo", 0.1f);
            }
        }
        else
        {
            if (_drop)
                Invoke("verificar_grupo", 0.1f);

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            UpdateCurrentPosition();
            SetSpriteState(false);
            GetComponent<M6L108BehaviourDrag>().swap = false;
        }
    }

    public void verificar_grupo()
    {
        int Tempint = 0;        
        for (int x = 0; x < grupoCorrecto.Count; x++)
        {
            if (_drop && _drop.GetComponent<M6L108BehaviourDrop>()._group.name != grupoCorrecto[x].name)
                Tempint++;
            else
            {
                grupoCorrecto[x].GetComponent<M6L108BehaviourDropGroup>().verificar(gameObject);
                if (grupoCorrecto[x].GetComponent<M6L108BehaviourDropGroup>().Confirmo)
                {
                    grupoCorrecto[x].GetComponent<M6L108BehaviourDropGroup>().spritesDrags.Add(GetComponent<Image>().sprite);
                    break;
                }
            }
        }
        if (Tempint == grupoCorrecto.Count)
            Regresar();
    }
    public void Regresar()
    {
        inDrop = false;
        swap = false;
        if (_drop)
        {
            _drop.GetComponent<M6L108BehaviourDrop>()._group.remover(gameObject);
            _drop.GetComponent<M6L108BehaviourDrop>()._drag = null;
            _drop = null;
        }

        GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        UpdateCurrentPosition();
        StartCoroutine(_managerDragDrop.StateBtnValidar());
        _DropRight.Clear();
    }
    public void OnDrop(PointerEventData eventData)
    {
        //print("OnDrop"+name);

        GameObject other = eventData.pointerDrag; // Get object drag        

        if (_drop)
        {
            if (other.GetComponent<M6L108BehaviourDrag>())
            {
                other.GetComponent<M6L108BehaviourDrag>()._DropRight.Clear();
                _DropRight.Clear();
                SwapCurrentPosition(other);
                SwapIndexRow(other);
                SwapStateDrop(other);
                SwapDrop(other);
                SwapDrag(other);
                UpdateCurrentPosition();
                SetSpriteState(false);
                if (_drop)
                    Invoke("verificar_grupo", 0.1f);                    
                //other.GetComponent<M6L108BehaviourDrag>().verificar_grupo();

            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inDrop = false;
        swap = false;
        _DropRight.Clear();
        if (_drop)
        {
            //_drop.GetComponent<M6L108BehaviourDrop>()._group.verificar(gameObject);
            _drop.GetComponent<M6L108BehaviourDrop>()._group.remover(gameObject);
            _drop.GetComponent<M6L108BehaviourDrop>()._drag = null;
            _drop = null;
        }

        GetComponent<RectTransform>().anchoredPosition = _defaultPos;
        UpdateCurrentPosition();
        StartCoroutine(_managerDragDrop.StateBtnValidar());

    }

    /// Actualiza la posicion default del drag 
    public void UpdateDefaultPosition() => _defaultPos = GetComponent<RectTransform>().anchoredPosition;
    /// Actualiza la posicion actual del drag 
    public void UpdateCurrentPosition() => _currentPos = GetComponent<RectTransform>().anchoredPosition;

    /// Intercambia la posicion actual entre los drag
    void SwapCurrentPosition(GameObject other)
    {
        other.GetComponent<M6L108BehaviourDrag>().swap = true; // Estado de intercambio 
        if (other.GetComponent<M6L108BehaviourDrag>()._drop)
        {
            Vector2 a = other.GetComponent<M6L108BehaviourDrag>()._currentPos;
            other.GetComponent<RectTransform>().anchoredPosition = _currentPos;
            GetComponent<RectTransform>().anchoredPosition = a;
        }
        else
        {
            Vector2 a = GetComponent<M6L108BehaviourDrag>()._defaultPos;
            other.GetComponent<RectTransform>().anchoredPosition = _currentPos;
            GetComponent<RectTransform>().anchoredPosition = a;
        }
    }

    ///Intercambia el objeto drag asociado a los drop
    void SwapDrag(GameObject other)
    {
        //print(other.name+"Sobre"+name);

        GameObject m = other.GetComponent<M6L108BehaviourDrag>()._drop;
        GameObject n = _drop;

        if (m)
            other.GetComponent<M6L108BehaviourDrag>()._drop.GetComponent<M6L108BehaviourDrop>()._drag = other;

        if (n)
            _drop.GetComponent<M6L108BehaviourDrop>()._drag = gameObject;

    }

    /// Intercambia el objeto drop entre los drag
    void SwapDrop(GameObject other)
    {
        GameObject x = other.GetComponent<M6L108BehaviourDrag>()._drop;
        other.GetComponent<M6L108BehaviourDrag>()._drop = _drop;
        _drop = x;

        //SwapDefaultPosition(other);
    }

    void SwapIndexRow(GameObject other)
    {
        if (_managerDragDrop._OperatingMethod == M6L108ManagerDragDrop.OperatingMethod.Match)
        {
            int x = other.GetComponent<M6L108BehaviourDrag>()._row;
            other.GetComponent<M6L108BehaviourDrag>()._row = this._row;
            this._row = x;
        }

    }

    void SwapStateDrop(GameObject other)
    {
        bool x = other.GetComponent<M6L108BehaviourDrag>().inDrop;
        other.GetComponent<M6L108BehaviourDrag>().inDrop = this.inDrop;
        this.inDrop = x;
    }

    void SetSpriteState(bool state)
    {
        GetComponent<Image>().sprite = state ?
            GetComponent<BehaviourSprite>()._selection :
            GetComponent<BehaviourSprite>()._default;

    }
}
