using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class M8A51_seleccionar : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;
    ControlPuntaje _controlPuntaje;

    [HideInInspector] public List<M8A51_toggleGroup> _groupToggle;

    int rights;
    int evaluated;

    public GameObject _button;

    public int respuesta = 0;

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

    public GameObject player;

    #endregion

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (transform.parent.GetComponent<BehaviourLayout>()._isEvaluated)
        {
            if (respuesta == 1) {
                player.GetComponent<Animator>().SetInteger("x", 3);
            }

            if (respuesta == 2) {
                player.GetComponent<Animator>().SetInteger("x", 2);
            }

        }
    }

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            _button.SetActive(true);
            print("x");
        }
        else
        {
            _button.SetActive(false);
        }
        evaluated = 0;
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

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
                bool state = _groupToggle[j].transform.GetChild(i).GetComponent<BehaviourToggle>().isRight;

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

    public void EvaluateGroupsToggleImmediately(M8A51_toggleGroup completed)
    {
        bool answer = false;
        int indexGroup = _groupToggle.IndexOf(completed);

        for (int i = 0; i < _groupToggle[indexGroup].transform.childCount; i++)
        {
            _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().interactable = false;

            Image img = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Image>();
            bool state = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<M8A51_toggle>().isRight;
            bool isON = _groupToggle[indexGroup].transform.GetChild(i).GetComponent<Toggle>().isOn;

            if (_TypeQualify == TypeQualify.all)
                SetSpriteAnswer(img, state);
            else
                    if (isON)
                SetSpriteAnswer(img, state);

        }

        Toggle t = _groupToggle[indexGroup].GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
        answer = (t.isOn && t.GetComponent<M8A51_toggle>().isRight);

        string z = ("Grupo" + _groupToggle[indexGroup].name + "select" + t.name + "is" + answer);
        //print(z);

        SetPuntaje(answer);
    }

    //// Instancia el puntaje y audio, al validar por boton
    void SetPuntaje()
    {
        if (_TypeCalification == TypeCalification.grupo)
            _controlAudio.PlayAudio(rights == _groupToggle.Count ? 1 : 2);
        else
            _controlAudio.PlayAudio(rights == _groupToggle[0]._sizeCorrect ? 1 : 2);

        _controlPuntaje.IncreaseScore(rights);
        _controlNavegacion.Forward(2);
    }

    /// Instancia el puntaje y audio, al seleccionar validacion inmediata
    void SetPuntaje(bool answer)
    {
        evaluated++;

        if (answer)
        {
            respuesta = 1;
            _controlAudio.PlayAudio(1);
            _controlPuntaje.IncreaseScore();
            player.GetComponent<Animator>().SetInteger("x", 3);
        }
        else
        {
            respuesta = 2;
            _controlAudio.PlayAudio(2);
            player.GetComponent<Animator>().SetInteger("x", 2);
        }

        if (evaluated == _groupToggle.Count)
            _controlNavegacion.Forward(4);

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
        respuesta = 0;
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<M8A51_playe>().pausa.SetActive(true);
        // player.GetComponent<RectTransform>().anchoredPosition= player.GetComponent<M8A51_playe>().inicial;
        player.GetComponent<M8A51_playe>().seleccionar.SetActive(false);
        player.GetComponent<M8A51_playe>().x = false;
        //player.GetComponent<Image>().sprite = player.GetComponent<M8A51_playe>().orignal;
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
        player.GetComponent<Animator>().SetInteger("x", 0);
        player.transform.rotation = new Quaternion(0,0,0,0);
        
        evaluated = 0;
       
    }



}
