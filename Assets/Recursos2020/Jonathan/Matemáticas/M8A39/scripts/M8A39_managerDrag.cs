using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M8A39_managerDrag : MonoBehaviour
{
    public M8A39_player player;
    public M8A39_manager manager;
    ControlPuntaje _controlPuntaje;
    
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public Canvas canvas;

    [Header("Item List")]
    public List<M8A39_drag> _drags,dragsTemp1, dragsTemp2, dragsTemp3, dragsTemp4;
    public List<M8A39_drop> _drops;

    public List<M8A39_group> _groups;

    Dictionary<GameObject, bool> answers = new Dictionary<GameObject, bool>();
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
        Math,
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
        
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        canvas = FindObjectOfType<Canvas>();
        _validar.gameObject.SetActive(_TypeValidation == TypeValidation.Button);
        _validar.onClick.AddListener(delegate { ValidarDragDrop(); });
        StartCoroutine(StateBtnValidar());

        if (needRandom)
            StartCoroutine(RandomDrag());

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

    IEnumerator RandomDrag()
    {
        yield return new WaitForSeconds(.2f);

        if (dragsTemp4.Count == 0)
        {
            GameObject randomTemp;
            Vector3 targetTemp;
            int statePos, placeTemp;
            int indexTemp;
            int rowTemp;

            statePos = 0;

            for (int j = 0; j < dragsTemp1.Count; j++)
            {
                int posnew = UnityEngine.Random.Range(0, dragsTemp1.Count - 1);

                if (statePos != posnew)
                {
                    indexTemp = dragsTemp1[j].transform.GetSiblingIndex();
                    rowTemp = dragsTemp1[j]._row;

                    targetTemp = dragsTemp1[j].transform.localPosition;
                    placeTemp = j;

                    randomTemp = dragsTemp1[j].gameObject;

                    dragsTemp1[j].transform.SetSiblingIndex(dragsTemp1[posnew].transform.GetSiblingIndex());
                    dragsTemp1[j]._row = dragsTemp1[posnew]._row;


                    dragsTemp1[j].transform.localPosition = dragsTemp1[posnew].transform.localPosition;


                    dragsTemp1[posnew].transform.SetSiblingIndex(indexTemp);
                    dragsTemp1[posnew]._row = rowTemp;

                    dragsTemp1[posnew].transform.localPosition = targetTemp;
                    dragsTemp1[j] = dragsTemp1[posnew];
                    dragsTemp1[posnew] = randomTemp.GetComponent<M8A39_drag>();
                }
            }

            foreach (var drag in dragsTemp1)
            {
                drag.UpdateDefaultPosition();
                drag.UpdateCurrentPosition();
            }
            for (int i = 0; i < dragsTemp2.Count; i++)
            {
                dragsTemp2[i].GetComponent<RectTransform>().anchoredPosition = dragsTemp1[i].GetComponent<RectTransform>().anchoredPosition;
            }
            for (int i = 0; i < dragsTemp3.Count; i++)
            {
                dragsTemp3[i].GetComponent<RectTransform>().anchoredPosition = dragsTemp1[i].GetComponent<RectTransform>().anchoredPosition;
            }
            for (int i = 0; i < dragsTemp2.Count; i++)
            {
                dragsTemp2[i].UpdateDefaultPosition();
                dragsTemp2[i].UpdateCurrentPosition();
            }
            for (int i = 0; i < dragsTemp3.Count; i++)
            {
                dragsTemp3[i].UpdateDefaultPosition();
                dragsTemp3[i].UpdateCurrentPosition();
            }
        }
        else {
            GameObject randomTemp;
            Vector3 targetTemp;
            int statePos, placeTemp;
            int indexTemp;
            int rowTemp;

            statePos = 0;

            for (int j = 0; j < dragsTemp1.Count; j++)
            {
                int posnew = UnityEngine.Random.Range(0, dragsTemp1.Count - 1);

                if (statePos != posnew)
                {
                    indexTemp = dragsTemp1[j].transform.GetSiblingIndex();
                    rowTemp = dragsTemp1[j]._row;

                    targetTemp = dragsTemp1[j].transform.localPosition;
                    placeTemp = j;

                    randomTemp = dragsTemp1[j].gameObject;

                    dragsTemp1[j].transform.SetSiblingIndex(dragsTemp1[posnew].transform.GetSiblingIndex());
                    dragsTemp1[j]._row = dragsTemp1[posnew]._row;


                    dragsTemp1[j].transform.localPosition = dragsTemp1[posnew].transform.localPosition;


                    dragsTemp1[posnew].transform.SetSiblingIndex(indexTemp);
                    dragsTemp1[posnew]._row = rowTemp;

                    dragsTemp1[posnew].transform.localPosition = targetTemp;
                    dragsTemp1[j] = dragsTemp1[posnew];
                    dragsTemp1[posnew] = randomTemp.GetComponent<M8A39_drag>();
                }
            }

            foreach (var drag in dragsTemp1)
            {
                drag.UpdateDefaultPosition();
                drag.UpdateCurrentPosition();
            }
            for (int i = 0; i < dragsTemp2.Count; i++)
            {
                dragsTemp2[i].GetComponent<RectTransform>().anchoredPosition = dragsTemp1[i].GetComponent<RectTransform>().anchoredPosition;
            }
            for (int i = 0; i < dragsTemp3.Count; i++)
            {
                dragsTemp3[i].GetComponent<RectTransform>().anchoredPosition = dragsTemp1[i].GetComponent<RectTransform>().anchoredPosition;
            }
            for (int i = 0; i < dragsTemp4.Count; i++)
            {
                dragsTemp4[i].GetComponent<RectTransform>().anchoredPosition = dragsTemp1[i].GetComponent<RectTransform>().anchoredPosition;
            }
            for (int i = 0; i < dragsTemp2.Count; i++)
            {
                dragsTemp2[i].UpdateDefaultPosition();
                dragsTemp2[i].UpdateCurrentPosition();
            }
            for (int i = 0; i < dragsTemp3.Count; i++)
            {
                dragsTemp3[i].UpdateDefaultPosition();
                dragsTemp3[i].UpdateCurrentPosition();
            }
            for (int i = 0; i < dragsTemp4.Count; i++)
            {
                dragsTemp4[i].UpdateDefaultPosition();
                dragsTemp4[i].UpdateCurrentPosition();
            }
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
                answers.Add(drag.gameObject, (drag._DropRight.Contains(drag._drop.GetComponent<M8A39_drop>())));


        foreach (var drop in _drops)
            if (drop._drag)
                answers.Add(drop.gameObject, (drop._drag.GetComponent<M8A39_drag>()._DropRight.Contains(drop)));

        foreach (var answer in answers)
        {
            var BhDrop = answer.Key.GetComponent<M8A39_drop>();

            if (BhDrop)
            {
                if (BhDrop._group)
                {
                    if (answer.Value)
                        BhDrop._group.GetComponent<M8A39_group>()._currentRight++;
                    else
                        BhDrop._group.GetComponent<M8A39_group>()._currentWrong++;
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
    public void ImmediatelyValidation(M8A39_drag drag, M8A39_drop drop)
    {

        answers.Add(drag.gameObject, drag._DropRight.Contains(drop));
        answers.Add(drop.gameObject, drop._drag.GetComponent<M8A39_drag>()._DropRight.Contains(drop));
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
            if (answer.Key.GetComponent<M8A39_drag>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationDrop()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M8A39_drop>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
    }

    public void TypeCalificationSymbol()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<M8A39_drag>())
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
            M8A39_drag temp = answer.Key.GetComponent<M8A39_drag>();

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
            

        }
        else
        {
            if (_customValidar == 0)

                if (rights == _drags.Count)
                {
                    _controlAudio.PlayAudio(1);
                    manager.AddCantidad(10);
                }
                else
                {
                    manager.RestLife();
                    _controlAudio.PlayAudio(2);
                }
            else
                
                if (rights == _customValidar)
                {
                    _controlAudio.PlayAudio(1);
                    manager.AddCantidad(10);
                }
                else
                {
                    manager.RestLife();
                    _controlAudio.PlayAudio(2);
                }

            _controlPuntaje.IncreaseScore(rights);
            PlayerPrefs.SetFloat("actividad1",rights);
            StartCoroutine(x());
            
            
            
        }

    }

    IEnumerator x() {
        yield return new WaitForSeconds(1);
        manager.ShowNextClues(gameObject);
        player.canMove = true;
    }

    public void SetPuntajeGroups()
    {
        rights = 0;

        foreach (var group in _groups)
        {
            if (group._needRight == group._currentRight && group._currentWrong == 0)
                rights++;
        }
        print(rights);

        if (rights == _groups.Count)
        {
            _controlAudio.PlayAudio(1);
            _controlPuntaje.IncreaseScore(rights);
            manager.AddCantidad(10);
        }
        else {
            _controlAudio.PlayAudio(2);
            manager.RestLife();
        }
        //_controlAudio.PlayAudio((rights == _groups.Count) ? 1 : 2);
       // _controlPuntaje.IncreaseScore(rights);
        StartCoroutine(x());


    }

    public void ResetDragDrop()
    {
        rights = 0;

        foreach (var drag in _drags)
        {
            SetDefaultSprite(drag.GetComponent<Image>());
            drag.enabled = true;
            drag._drop = null;
            drag.GetComponent<RectTransform>().anchoredPosition = drag._defaultPos;
            drag.UpdateCurrentPosition();
            drag.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            if (_TypeCalification == TypeCalification.Symbol || _TypeCalification == TypeCalification.Multiple)
                SetDefaultSymbol(drag.transform.GetChild(0).GetComponent<Image>());

        }

        foreach (var drop in _drops)
        {
            SetDefaultSprite(drop.GetComponent<Image>());
            drop._drag = null;
        }

        foreach (var group in _groups)
        {
            SetDefaultSprite(group.GetComponent<Image>());

            group._currentRight = 0;
            group._currentWrong = 0;

        }


        answers.Clear();

        StartCoroutine(RandomDrag());

    }
}
