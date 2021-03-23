using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class L11A269ManagerDragDrop : MonoBehaviour
{
    ControlPuntaje _controlPuntaje;
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public Canvas canvas;

    [Header("Item List")]
    /* [HideInInspector] */
    public List<L11A269BehaviourDrag> _drags;
    [HideInInspector] public List<L11A269BehaviourDrop> _drops;    

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
        Default        
    }

    [Header("Tipo de funcionamiento")] public OperatingMethod _OperatingMethod;

    public enum TypeValidation
    {       
        Button
    }

    [Header("Tipo de validacion")] public TypeValidation _TypeValidation;

    [Flags]
    public enum TypeCalification
    {
        Drag
    }

    [Header("Tipo de calificacion")] public TypeCalification _TypeCalification;   

    #endregion

    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        canvas = FindObjectOfType<Canvas>();
        _validar.gameObject.SetActive(_TypeValidation == TypeValidation.Button);
        _validar.onClick.AddListener(delegate { ValidarDragDrop(); });
        //StartCoroutine(StateBtnValidar());

    }

    void OnEnable()
    {
        if (needRandom && !GetComponent<BehaviourLayout>()._isEvaluated)
            StartCoroutine(RandomDrag());

        StartCoroutine(StateBtnValidar());
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
                _drags[posnew] = randomTemp.GetComponent<L11A269BehaviourDrag>();
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
        {
            drag.GetComponent<L11A269BehaviourDrag>().enabled = false;

            if (drag._drop)
                answers.Add(drag.gameObject, (drag._DropRight.Contains(drag._drop.GetComponent<L11A269BehaviourDrop>())));
        }



        //foreach (var drop in _drops)
        //    if (drop._drag[drop.GetComponent<L11A269BehaviourDrop>().i - 1])
        //        answers.Add(drop.gameObject, (drop._drag[drop.GetComponent<L11A269BehaviourDrop>().i - 1].GetComponent<L11A269BehaviourDrag>()._DropRight.Contains(drop)));        


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
        }

    }

    #region MethodsCalification

    public void TypeCalificationDrag()
    {
        foreach (var answer in answers)
            if (answer.Key.GetComponent<L11A269BehaviourDrag>())
                SetSpriteAnswer(answer.Key.GetComponent<Image>(), answer.Value);
    }

    #endregion


    /// <summary>
    /// Recibe la imagen y respuesta, para asignar el correspondiente sprite respuesta
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    public void SetSpriteAnswer(Image i, bool state)
    {
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


    /// <summary>
    /// Obtiene las respuestas correstas e instancia el audio y puntaje correspondiente
    /// </summary>
    public void SetPuntaje()
    {
        rights = 0;

        foreach (var answer in answers)
        {
            if (answer.Key.GetComponent<L11A269BehaviourDrag>() && answer.Value)
                rights++;
        }

        if (_customValidar == 0)
            _controlAudio.PlayAudio((rights == _drags.Count) ? 1 : 2);
        else
            _controlAudio.PlayAudio((rights == _customValidar) ? 1 : 2);

        _controlPuntaje.IncreaseScore(rights);
        _controlNavegacion.Forward(2);

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
        }        
        foreach (var drop in _drops)
        {
            SetDefaultSprite(drop.GetComponent<Image>());
            drop._drag.Clear();
            for (int x = 0; x < 3; x++)
                drop._drag.Add(null);
        }
        
       
        answers.Clear();
    }
}
