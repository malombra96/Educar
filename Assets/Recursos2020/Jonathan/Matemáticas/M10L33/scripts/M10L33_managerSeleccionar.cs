using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class M10L33_managerSeleccionar : MonoBehaviour
{

    public int level;
    public GameObject Element;

    M10L33_player _player;

    public GameObject rightImage, wrongImag, background;

    [HideInInspector] public ControlAudio _controlAudio;
    ControlPuntaje _controlPuntaje;

    M10L33_manager _general;
    public List<M10L33_groupToogle> _groupToggle;

    int rights;
    int evaluated;

    #region Configure Parametres SelectionToggle

    public enum TypeValidation
    {
        inmediata,
        button
    }

    [Tooltip("Seleccione el tipo de validacion")] [Header("Tipo de Validacion")] public TypeValidation _typeVal;
    public Button _validarBTN;

    public enum TypeSelection
    {
        onexgroup,
        variousxgroup
    }

    [Tooltip("Seleccione el tipo de Selection Toggle")] [Header("Tipo de Selection")] public TypeSelection _TypeSelect;
    [Tooltip("Determine la cantidad de clicks/selecciones")] [Range(1, 10)] public int _options;

    public enum TypeCalification
    {
        individual,
        grupo
    }

    [Tooltip("Seleccione el tipo de calificaccion")] [Header("Tipo de Calificación")] public TypeCalification _TypeCalification;

    public enum TypeQualify
    {
        onlySelection,
        all
    }
    [Tooltip("Seleccione el tipo de validacion en las respuestas")] [Header("¿Como calificar?")] public TypeQualify _TypeQualify;

    #endregion

    private void Awake()
    {
        _general = GameObject.FindObjectOfType<M10L33_manager>();
    }
    private void Start()
    {
        evaluated = 0;
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _player = GameObject.FindObjectOfType<M10L33_player>();
        

        SetValidar();

    }


    /// Determina el estado del boton validar
    void SetValidar()
    {
        switch (_typeVal)
        {
            case TypeValidation.button:

                _validarBTN.gameObject.SetActive(true);
                _validarBTN.onClick.AddListener(EvaluateGroupsToggle);

                break;

            case TypeValidation.inmediata:

                _validarBTN.gameObject.SetActive(false);

                break;
        }
    }

    void EvaluateGroupsToggle()
    {
        _validarBTN.interactable = false;

        rights = 0;

        for (int j = 0; j < _groupToggle.Count; j++)
        {
            int a = 0;
            int b = _groupToggle[j]._sizeCorrect;

            for (int i = 0; i < _groupToggle[j].transform.childCount; i++)
            {
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

                Image img = _groupToggle[j].transform.GetChild(i).GetComponent<Image>();
                bool isON = _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().isOn;
                bool state = _groupToggle[j].transform.GetChild(i).GetComponent<M10L33_seleccionar>().isRight;

                if (isON && state)
                    a++;

                if (_TypeQualify == TypeQualify.all)
                    SetSpriteAnswer(img, state);
                else
                    if (isON)
                    SetSpriteAnswer(img, state);

            }

            if (_TypeCalification == TypeCalification.grupo)
            {
                if (a == b)
                    rights++;
            }
            else
            {
                rights = a;
            }


            string z = ("Grupo" + _groupToggle[j].name + "Correctas" + a + "Necesarias" + b);
            //print(z);
        }

        SetPuntaje();
    }

    public void EvaluateGroupsToggleImmediately(M10L33_groupToogle completed)
    {
        bool answer = false;
        int indexGroup = _groupToggle.IndexOf(completed);

        for (int i = 0; i < _groupToggle[indexGroup].transform.childCount; i++)
        {
            _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

            Image img = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Image>();
            bool state = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<M10L33_seleccionar>().isRight;
            SetSpriteAnswer(img, state);

        }

        Toggle t = _groupToggle[indexGroup].GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
        answer = (t.isOn && t.GetComponent<M10L33_seleccionar>().isRight);

        string z = ("Grupo" + _groupToggle[indexGroup].name + "select" + t.name + "is" + answer);
        print(z);

        SetPuntaje(answer);
    }

    //// Instancia el puntaje y audio, al validar por boton
    void SetPuntaje()
    {
        Element.GetComponent<M10L33_element>().GetQuestion();
        if (_TypeCalification == TypeCalification.grupo)
        {
            if (rights == _groupToggle.Count)
            {
                background.SetActive(false);
                rightImage.SetActive(true);
                _controlAudio.PlayAudio(1);
                _validarBTN.gameObject.SetActive(false);
                _general.AddGifts(level);
                _controlPuntaje.IncreaseScore();
            }
            else
            {
                background.SetActive(false);
                wrongImag.SetActive(true);
                _controlAudio.PlayAudio(2);
                _validarBTN.gameObject.SetActive(false);
                _general.RestLifes();
            }

        }

        //_controlAudio.PlayAudio(rights == _groupToggle.Count ? 1 : 2);
        else {
            if (rights == 1)
            {
                background.SetActive(false);
                rightImage.SetActive(true);
                _controlAudio.PlayAudio(1);
                _validarBTN.gameObject.SetActive(false);
                _general.AddGifts(level);
                _controlPuntaje.IncreaseScore();
            }
            else
            {
               
                _controlAudio.PlayAudio(2);
                _validarBTN.gameObject.SetActive(false);
                _general.RestLifes();
            }
        }

        //_controlAudio.PlayAudio(rights == _groupToggle[0]._sizeCorrect ? 1 : 2);

        //_controlPuntaje.IncreaseScore(rights);

//        StartCoroutine(a());
        
        
    }


    IEnumerator a(bool valueImage)
    {
        yield return new WaitForSeconds(1);
        if (valueImage)
        {
            background.SetActive(false);
            rightImage.SetActive(true);
        }
        else {
            background.SetActive(false);
            wrongImag.SetActive(true);
            _general.RestLifes();
        }
        
        yield return new WaitForSeconds(2);
        Element.GetComponent<M10L33_element>().GetQuestion();
        _player.canMove = true;
        _player.x = 0;
        rightImage.SetActive(false);
        wrongImag.SetActive(false);
        background.transform.parent.gameObject.SetActive(false);
    }

    /// Instancia el puntaje y audio, al seleccionar validacion inmediata
    void SetPuntaje(bool answer)
    {
        evaluated++;

        if (answer)
        {
            _controlAudio.PlayAudio(1);
            background.SetActive(false);
            rightImage.SetActive(true);
            _validarBTN.gameObject.SetActive(false);
            _general.AddGifts(level);
             _controlPuntaje.IncreaseScore();
            StartCoroutine(a(true));
        }
        else
        {
         _controlAudio.PlayAudio(2);
            background.SetActive(false);
            wrongImag.SetActive(true);
            _controlAudio.PlayAudio(2);
            _validarBTN.gameObject.SetActive(false);
            
            StartCoroutine(a(false));
        }

        if (evaluated == _groupToggle.Count) { 

        }
       

    }

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

    public void ResetSeleccionarToggle()
    {
        _validarBTN.gameObject.SetActive(true);
        background.SetActive(true);
        rightImage.SetActive(false);
        wrongImag.SetActive(false);
        _validarBTN.interactable = true;

        for (int j = 0; j < _groupToggle.Count; j++)
        {
            for (int i = 0; i < _groupToggle[j].transform.childCount; i++)
            {
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().interactable = true;
                _groupToggle[j].transform.GetChild(i).GetComponent<Toggle>().isOn = false;

                Image img = _groupToggle[j].transform.GetChild(i).GetComponent<Image>();
                SetDefaultSprite(img);

            }

        }
    }
}
