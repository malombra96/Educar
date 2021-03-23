using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M10A38ManagerDragDrop : MonoBehaviour
{
    ControlPuntaje _controlPuntaje;
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public Canvas canvas;

    [Header("Item List")]
    [HideInInspector] public List<M10A38BehaviourDrag> _drags;
    [HideInInspector] public List<M10A38BehaviourDrop> _drops;

    [HideInInspector] public List<M10A38BehaviourDropGroup> _groups;

    //Dictionary<GameObject, bool> answers = new Dictionary<GameObject, bool>();
    int rights;
    int evaluated;

    [Header("Requiere Random")] public bool needRandom;

    [Header("Button Submit")]
    public Button _validar;    

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
        Drag       
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
        _validar.onClick.AddListener(delegate { SetTypeCalification(); });
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

        _validar.interactable = (i == _drags.Count);
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
                _drags[posnew] = randomTemp.GetComponent<M10A38BehaviourDrag>();
            }
        }

        foreach (var drag in _drags)
        {
            drag.UpdateDefaultPosition();
            drag.UpdateCurrentPosition();
        }
    }

   
    void SetTypeCalification()
    {
        switch (_TypeCalification)
        {
            case TypeCalification.Drag:

                TypeCalificationDrag();
                

                break;
               
        }

    }

    #region MethodsCalification

    public void TypeCalificationDrag()
    {
        _validar.interactable = false;
        int puntos = 0;
        //bool correcto = false;
            for (int x = 0; x < _drags.Count; x++)
            {
                _drags[x].mover = false;
            }
        for(int i = 0; i < _groups.Count; i++)
        {
            _groups[i].validar();
            if (_groups[i].correcto)
            {
                puntos++;
            }
        }       
        
        //if (puntos == _groups.Count)
        //{
        //    for (int x = 0; x < _drags.Count; x++)
        //    {
        //        SetSpriteAnswer(_drags[x].GetComponent<Image>(), true);                
        //    }            
        //    //correcto = true;
        //}
        //else
        //{
        //    for (int x = 0; x < _drags.Count; x++)
        //    {
        //        SetSpriteAnswer(_drags[x].GetComponent<Image>(), false);
        //    }
        //}
        _controlPuntaje.IncreaseScore(puntos == _groups.Count ? 1 : 0);
        _controlAudio.PlayAudio( puntos ==_groups.Count ? 1 : 2);
        _controlNavegacion.Forward(2);
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

    void SetDefaultSymbol(Image i)
    {
        i.gameObject.SetActive(false);
        i.sprite = i.GetComponent<BehaviourSprite>()._default;
    }   

    public void ResetDragDrop()
    {
        rights = 0;
        foreach (var drag in _drags)
        {
            SetDefaultSprite(drag.GetComponent<Image>());
            drag.mover = true;
            drag.enabled = true;
            drag.correcto = false;
            drag._drop = null;
            drag.GetComponent<RectTransform>().anchoredPosition = drag._defaultPos;
            drag.UpdateCurrentPosition();           

        }

        foreach (var drop in _drops)
        {
            SetDefaultSprite(drop.GetComponent<Image>());
            drop._drag = null;
        }

        foreach (var drop in _groups)
        {
            drop.x = 0;
        }
        //StartCoroutine(RandomDrag());

    }
}
