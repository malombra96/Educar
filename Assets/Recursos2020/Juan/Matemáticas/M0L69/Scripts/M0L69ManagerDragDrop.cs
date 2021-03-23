using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M0L69ManagerDragDrop : MonoBehaviour
{
    public GameObject[] niño;
    ControlPuntaje _controlPuntaje;
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public Canvas canvas;
    bool activado;
    [Header("Item List")]
    /*[HideInInspector]*/
    public List<M0L69BehaviourDrag> _drags;
    [HideInInspector] public List<M0L69BehaviourDrop> _drops;

    [HideInInspector] public List<M0L69BehaviourDropGroup> _groups;

    [HideInInspector] public Dictionary<GameObject, bool> answers = new Dictionary<GameObject, bool>();
    int rights;
    int evaluated;
    
    [Header("Requiere Random")] public bool needRandom;

    [Header("Arrastre Boton Validar")]
    public Button _validar;

    [Tooltip("Si el valor es 0 tomará validación por defecto")]
    [Header("Activacion Manual de Boton Validar")] [Range(0, 10)] public int _customValidar;

    #region GeneralOptionsDragDrop

    public enum OperatingMethod
    {
        Default,
        Match,
        Group,
        Create
    }

    [Header("Tipo de funcionamiento")] public OperatingMethod _OperatingMethod;

    public enum TypeValidation
    {
        Inmediata,
        Button
    }

    [Header("Tipo de validacion")] public TypeValidation _TypeValidation;

    [Flags]
    public enum TypeCalification
    {
        Drag,
        Drop,
        DragDrop,
        Group,
        Symbol,
        Multiple
    }

    [Header("Tipo de calificacion")] public TypeCalification _TypeCalification;
    [Header("Tipo de calificacion Multiple")] public UnityEvent _CallFunction;

    #endregion

    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        canvas = FindObjectOfType<Canvas>();
        _validar.gameObject.SetActive(_TypeValidation == TypeValidation.Button);
        _validar.onClick.AddListener(delegate { ValidarDragDrop(); });
        StartCoroutine(StateBtnValidar());
        activado = true;
        if (needRandom)
            StartCoroutine(RandomDrag());

    }
   
    public IEnumerator StateBtnValidar()
    {
        yield return new WaitForSeconds(.1f);

        int i = 0;

        foreach (var drag in _drags)
            if (drag._drop != null)
            {
                i++;
            }

        //print(i+"//"+_drags.Count);

        if (_customValidar == 0)
            _validar.interactable = (i == _drags.Count);
        else
            _validar.interactable = (i > 0);
    }

    IEnumerator RandomDrag()
    {
        yield return new WaitForSeconds(.2f);

        GameObject randomTemp;
        Vector3 targetTemp;
        int statePos, placeTemp;
        int indexTemp;
        int rowTemp;

        statePos = 0;

        for (int j = 0; j < _drags.Count; j++)
        {
            int posnew = UnityEngine.Random.Range(0, _drags.Count - 1);

            if (statePos != posnew)
            {
                indexTemp = _drags[j].transform.GetSiblingIndex();
                rowTemp = _drags[j]._row;

                targetTemp = _drags[j].transform.localPosition;
                placeTemp = j;

                randomTemp = _drags[j].gameObject;

                _drags[j].transform.SetSiblingIndex(_drags[posnew].transform.GetSiblingIndex());
                _drags[j]._row = _drags[posnew]._row;


                _drags[j].transform.localPosition = _drags[posnew].transform.localPosition;


                _drags[posnew].transform.SetSiblingIndex(indexTemp);
                _drags[posnew]._row = rowTemp;

                _drags[posnew].transform.localPosition = targetTemp;
                _drags[j] = _drags[posnew];
                _drags[posnew] = randomTemp.GetComponent<M0L69BehaviourDrag>();
            }
        }

        foreach (var drag in _drags)
        {
            drag.UpdateDefaultPosition();
            drag.UpdateCurrentPosition();
        }
    }

    /// <summary>
    /// Agrega a un diccionario c/u drag y drop con su respectivo estado de respuesta
    /// </summary>
    void ValidarDragDrop()
    {
        _validar.interactable = false;

        foreach (var drag in _drags)
            if (drag._drop && drag)
                answers.Add(drag.gameObject, (drag._DropRight.Contains(drag._drop.GetComponent<M0L69BehaviourDrop>())));


        foreach (var drop in _drops)
            if (drop._drag)
                answers.Add(drop.gameObject, (drop._drag.GetComponent<M0L69BehaviourDrag>()._DropRight.Contains(drop)));

        foreach (var answer in answers)
        {
            var BhDrop = answer.Key.GetComponent<M0L69BehaviourDrop>();

            if (BhDrop)
            {
                if (BhDrop._group)
                {
                    if (answer.Value)
                        BhDrop._group.GetComponent<M0L69BehaviourDropGroup>()._currentRight++;
                    else
                        BhDrop._group.GetComponent<M0L69BehaviourDropGroup>()._currentWrong++;
                }
            }
            //print(answer.Key.GetComponent<BehaviourDrop>()._group+""+answer.Key + answer);
        }


        SetTypeCalification();

    }

    /// <summary>
    /// Recibe el drag y drop interactuados,y los agrega a un diccionario con su estado de respuesta 
    /// </summary>
    /// <param name="drag"></param>
    /// <param name="drop"></param>
    public void ImmediatelyValidation(M0L69BehaviourDrag drag, M0L69BehaviourDrop drop)
    {

        answers.Add(drag.gameObject, drag._DropRight.Contains(drop));
        answers.Add(drop.gameObject, drop._drag.GetComponent<M0L69BehaviourDrag>()._DropRight.Contains(drop));
        evaluated++;
        SetTypeCalification();

    }

    /// <summary>
    /// Itera el diccionario y según el tipo de calificacion envia la imagen y respuesta
    /// </summary>
    void SetTypeCalification()
    {
        switch (_TypeCalification)
        {
            case TypeCalification.Drag:

                TypeCalificationDrag();
                SetPuntaje();

                break;

            case TypeCalification.Drop:

                TypeCalificationDrop();
                SetPuntaje();

                break;

            case TypeCalification.DragDrop:

                TypeCalificationDrag();
                TypeCalificationDrop();
                SetPuntaje();
                /* foreach (var answer in answers)
                    SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value); */

                break;

            case TypeCalification.Symbol:

                TypeCalificationSymbol();
                SetPuntaje();

                break;

            case TypeCalification.Group:

                TypeCalificationGroup();
                SetPuntajeGroups();


                break;

            case TypeCalification.Multiple:

                _CallFunction.Invoke();

                break;
        }

    }

    #region MethodsCalification

    public void TypeCalificationDrag()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M0L69BehaviourDrag>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationDrop()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M0L69BehaviourDrop>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationSymbol()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M0L69BehaviourDrag>())
                SetSpriteAnswer(answer.Key.transform.GetChild(0).GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationGroup()
    {
        int A = 0;
        int B = 0;
        foreach (var group in _groups)
        {
            if (group.name == "Group_A")
            {
                if (group._currentWrong == 0)
                {
                    A++;
                }
                SetSpriteAnswer(niño[0].GetComponent<Image>(), (group._needRight == group._currentRight && A == 3 && group._currentWrong == 0));                
            }
            else if (group.name == "Group_B")
            {
                if (group._currentWrong == 0)
                {
                    B++;
                }
                SetSpriteAnswer(niño[1].GetComponent<Image>(), (group._needRight == group._currentRight && B == 3 && group._currentWrong == 0));                
            }

        }
    }

    #endregion


    /// <summary>
    /// Recibe la imagen y respuesta, para asignar el correspondiente sprite respuesta
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    public void SetSpriteAnswer(Image i, bool state)
    {
        i.gameObject.SetActive(true);

        i.sprite = state ?
            i.GetComponent<BehaviourSprite>()._right :
            i.GetComponent<BehaviourSprite>()._wrong;
    }

    /// <summary>
    /// Asigna sprite default de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    void SetDefaultSprite(Image i)
    {
        i.sprite = i.GetComponent<BehaviourSprite>()._default;
    }

    void SetDefaultSymbol(Image i)
    {
        i.gameObject.SetActive(false);
        i.sprite = i.GetComponent<BehaviourSprite>()._default;
    }


    /// <summary>
    /// Obtiene las respuestas correstas e instancia el audio y puntaje correspondiente
    /// </summary>
    public void SetPuntaje()
    {
        rights = 0;

        foreach (var answer in answers)
        {
            M0L69BehaviourDrag temp = answer.Key.GetComponent<M0L69BehaviourDrag>();

            if (temp)
            {
                temp.enabled = false;

                if (answer.Value)
                    rights++;
            }
        }

        if (_TypeValidation == TypeValidation.Inmediata)
        {
            _controlAudio.PlayAudio((rights == 1) ? 1 : 2);
            _controlPuntaje.IncreaseScore();
            answers.Clear();
            rights = 0;

            if (evaluated == _drags.Count) { }
            _controlNavegacion.Forward(2);

        }
        else
        {
            if (_customValidar == 0)
                _controlAudio.PlayAudio((rights == _drags.Count) ? 1 : 2);
            else
                _controlAudio.PlayAudio((rights == _customValidar) ? 1 : 2);

            _controlPuntaje.IncreaseScore(rights);
            _controlNavegacion.Forward(2);
        }

    }

    public void SetPuntajeGroups()
    {
        rights = 0;
        int i = 0;
        int d = 0;
        foreach (var group in _groups)
        {
            if (group._needRight == group._currentRight && group._currentWrong == 0 && group.name == "Group_A")
            {
                i++;
            }
            if (group._needRight == group._currentRight && group._currentWrong == 0 && group.name == "Group_B")
            {
                d++;
            }
        }

        if ((i + d) == _groups.Count)
        {
            rights = 2;
        }
        else if (i == 3 || d == 3)
        {
            rights = 1;
        }
        _controlNavegacion.Forward(2);
        _controlAudio.PlayAudio((rights == 2) ? 1 : 2);
        _controlPuntaje.IncreaseScore(rights);
    }

    public void ResetDragDrop()
    {
        rights = 0;
        _validar.interactable = false;

        foreach (var drag in _drags)
        {
            if (drag._drop && drag)
            {
                Destroy(drag.gameObject);
            }
        }

        foreach (var drop in _drops)
        {
            SetDefaultSprite(drop.GetComponent<Image>());
            drop._drag = null;
        }

        foreach (var group in _groups)
        {
            SetDefaultSprite(group.GetComponent<Image>());
            group.GetComponent<M0L69BehaviourDropGroup>().j = 0;
            group._currentRight = 0;
            group._currentWrong = 0;

            for (int x = 0; x < group.transform.childCount; x++)
            {
                Destroy(group.transform.GetChild(x).GetComponent<M0L69BehaviourDrop>()._drag);
                group.transform.GetChild(x).GetComponent<M0L69BehaviourDrop>()._drag = null;
            }
        }
        _drags.Clear();
        if (activado)
        {
            _drags.Add(transform.GetChild(13).GetComponent<M0L69BehaviourDrag>());
            _drags.Add(transform.GetChild(12).GetComponent<M0L69BehaviourDrag>());
            _drags.Add(transform.GetChild(11).GetComponent<M0L69BehaviourDrag>());
        }
        answers.Clear();
       

    }
}
