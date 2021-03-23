﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class M6A92_manager : MonoBehaviour
{
    public GameObject hand;
    public List<Slider> sliders;
    public GameObject validar_1;
    public M6A92_managerBarra _ManagerBarra;
    public bool first;
    ControlPuntaje _controlPuntaje;
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public Canvas canvas;

    [Header("Item List")]
    public List<M6A92_drag> _drags;
    public List<M6A92_Drop> _drops;

    public List<M6A92_grupo> _groups;

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
        hand.SetActive(false);
        validar_1.SetActive(false);
        first = true;
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        canvas = FindObjectOfType<Canvas>();
        _validar.gameObject.SetActive(_TypeValidation == TypeValidation.Button);
        _validar.onClick.AddListener(delegate { ValidarDragDrop(); });
        StartCoroutine(StateBtnValidar());
        foreach(var s in sliders)
            s.gameObject.SetActive(false);


        if (needRandom)
            RandomDrag();

    }

    public IEnumerator StateBtnValidar()
    {
        yield return new WaitForSeconds(.1f);

        int i = 0;

        foreach (var drag in _drags)
            if (drag._drop != null)
                i++;

        //print(i+"//"+_drags.Count);

        if (_customValidar == 0)
            _validar.interactable = (i == _drags.Count);
        else
            _validar.interactable = (i == _customValidar);
    }

    void RandomDrag()
    {


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
                _drags[posnew] = randomTemp.GetComponent<M6A92_drag>();
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
            if (drag._drop)
                answers.Add(drag.gameObject, (drag._DropRight.Contains(drag._drop.GetComponent<M6A92_Drop>())));


        foreach (var drop in _drops)
            if (drop._drag)
                answers.Add(drop.gameObject, (drop._drag.GetComponent<M6A92_drag>()._DropRight.Contains(drop)));

        foreach (var answer in answers)
        {
            var BhDrop = answer.Key.GetComponent<M6A92_Drop>();

            if (BhDrop)
            {
                if (BhDrop._group)
                {
                    if (answer.Value)
                        BhDrop._group.GetComponent<M6A92_grupo>()._currentRight++;
                    else
                        BhDrop._group.GetComponent<M6A92_grupo>()._currentWrong++;
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
    public void ImmediatelyValidation(M6A92_drag drag, M6A92_Drop drop)
    {

        answers.Add(drag.gameObject, drag._DropRight.Contains(drop));
        answers.Add(drop.gameObject, drop._drag.GetComponent<M6A92_drag>()._DropRight.Contains(drop));
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
        hand.SetActive(true);
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M6A92_drag>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
        StartCoroutine(x());
    }

    IEnumerator x() {
        yield return new WaitForSeconds(3f);
        hand.SetActive(false);
        validar_1.SetActive(true);
        foreach (var s in sliders) {
            s.gameObject.SetActive(true);
            s.transform.GetChild(0).GetComponent<Image>().color = new Color32(197,197,197,37);
        }
            
    }

    public void TypeCalificationDrop()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M6A92_Drop>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationSymbol()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M6A92_drag>())
                SetSpriteAnswer(answer.Key.transform.GetChild(0).GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationGroup()
    {
        foreach (var group in _groups)
            SetSpriteAnswer(group.GetComponent<Image>(), (group._needRight == group._currentRight && group._currentWrong == 0));
    }

    #endregion


    /// <summary>
    /// Recibe la imagen y respuesta, para asignar el correspondiente sprite respuesta
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    public void SetSpriteAnswer(Image i, bool state)
    {

        if (_TypeCalification == TypeCalification.Symbol || _TypeCalification == TypeCalification.Multiple)
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
            M6A92_drag temp = answer.Key.GetComponent<M6A92_drag>();

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
            //_controlNavegacion.Forward(2);
        }

    }

    public void SetPuntajeGroups()
    {
        rights = 0;

        foreach (var group in _groups)
        {
            if (group._needRight == group._currentRight && group._currentWrong == 0)
                rights++;
        }

        _controlAudio.PlayAudio((rights == _groups.Count) ? 1 : 2);
        _controlPuntaje.IncreaseScore(rights);
        _controlNavegacion.Forward(2);

    }

    public void ResetDragDrop()
    {
        if (first)
        {
            hand.SetActive(false);
            validar_1.SetActive(false);
            _ManagerBarra.ResetAll();
            _controlPuntaje.resetScore();
            rights = 0;

            foreach (var drag in _drags)
            {
                SetDefaultSprite(drag.GetComponent<Image>());
                drag.enabled = true;
                drag._drop = null;
                drag.GetComponent<RectTransform>().anchoredPosition = drag._defaultPos;
                drag.UpdateCurrentPosition();

                if (_TypeCalification == TypeCalification.Symbol || _TypeCalification == TypeCalification.Multiple)
                    SetDefaultSymbol(drag.transform.GetChild(0).GetComponent<Image>());

            }

            foreach (var drop in _drops)
            {
                SetDefaultSprite(drop.GetComponent<Image>());
                drop._drag = null;
                drop.value = -1;
                drop._barra.value = -1;
            }

            foreach (var group in _groups)
            {
                SetDefaultSprite(group.GetComponent<Image>());

                group._currentRight = 0;
                group._currentWrong = 0;

            }


            answers.Clear();
            foreach (var s in sliders)
            {
                s.gameObject.SetActive(false);
                s.transform.GetChild(0).GetComponent<Image>().color = new Color32(197, 197, 197, 0);
            }
            RandomDrag();

        }
    }

}
