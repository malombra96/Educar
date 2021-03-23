using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class M6L107_managerInput : MonoBehaviour
{
    public int v, goT,goF;
    public GameObject vidas,cofre,player;
    public enum TipoCalificacion
    {
        texto,
        fondo,
        simbolo,

        multiple
    }


    [Header("Tipo de Calficacion")] public TipoCalificacion calificacion;
    [Header("Tipo de calificacion Multiple")] public UnityEvent _FunctionMultiple;

    public enum TipoPuntuacion
    {
        individual,
        grupo
    }

    [Header("Tipo de puntuacion")] public TipoPuntuacion _TipoPuntuacion;

    public List<M6L107_groupInput> _groupInputField;

    [Header("Button Validar")] public Button botonValidar;

    [HideInInspector] public ControlAudio _controlAudio;

    ControlPuntaje _controlPuntaje;

    ControlNavegacion _controlNavegacion;

    [HideInInspector] public bool haCalificado;

    [Header("LightBox NumPad")] public GameObject _lightBox;


    [Header("Colors Setup")]

    public Color32 colorTextoInicial;
    public Color32 colorTextoCorrecto;
    public Color32 colorTextoIncorrecto;

    public Color32 colorFondoInicial;
    public Color32 colorFondoCorrecto;
    public Color32 colorFondoIncorrecto;

    [Header("Lista Respuestas por grupo")] public ListAnswers[] _listAnswers;

    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    }

    void Start()
    {
        InitializeState();
        botonValidar.onClick.AddListener(CalificarInputs);
    }

    public void SetStateValidarBTN()
    {
        bool s = true;

        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                if (inputField.GetComponent<M6L107_input>()._isEmpty)
                {
                    s = false;
                    break;
                }
            }
        }



        botonValidar.interactable = s;
    }

    public void InitializeState()
    {
        foreach (var group in _groupInputField)
        {
            foreach (var inputField in group._inputFields)
            {
                inputField.GetComponent<InputField>().text = "";
                inputField.interactable = true;


                for (int i = 0; i < inputField.transform.childCount; i++)
                    if (inputField.transform.GetChild(i).GetComponent<BehaviourSprite>())
                        inputField.transform.GetChild(i).gameObject.SetActive(false);


                if (inputField.transform.GetChild(0).GetComponent<Text>())
                    inputField.transform.GetChild(0).GetComponent<Text>().color = colorTextoInicial;
                else
                    inputField.transform.GetChild(1).GetComponent<Text>().color = colorTextoInicial;

                inputField.GetComponent<M6L107_input>()._isRight = false;
                inputField.GetComponent<M6L107_input>()._isEnabled = true;
                inputField.GetComponent<Image>().color = colorFondoInicial;
                inputField.GetComponent<Image>().sprite = inputField.GetComponent<BehaviourSprite>()._default;

            }
        }

        _listAnswers = new ListAnswers[0];

        SetStateValidarBTN();
    }

    public void CalificarInputs()
    {
        botonValidar.interactable = false;
        _controlAudio.PlayAudio(0);

        int[] contadorTemp = new int[_groupInputField.Count];
        _listAnswers = new ListAnswers[_groupInputField.Count];

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            _listAnswers[i].inputFields = new InputField[_groupInputField[i]._inputFields.Count];
            _listAnswers[i].answers = new bool[_groupInputField[i]._inputFields.Count];

            if (_groupInputField[i]._TipoRespuestas == M6L107_groupInput.TipoRespuestas.individual)
            {
                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<M6L107_input>()._isEnabled = false;

                    bool answer;
                    answer = (f.GetComponent<M6L107_input>().respuestaCorrecta.Contains(f.GetComponent<InputField>().text));
                    _listAnswers[i].inputFields[j] = f;
                    _listAnswers[i].answers[j] = answer;


                    if (answer)
                        contadorTemp[i]++;
                }
            }
            else
            {
                _groupInputField[i].EvaluateGroup();

                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<M6L107_input>()._isEnabled = false;

                    bool answer;
                    answer = f.GetComponent<M6L107_input>()._isRight;

                    _listAnswers[i].inputFields[j] = f;
                    _listAnswers[i].answers[j] = answer;


                    if (answer)
                        contadorTemp[i]++;
                }
            }
        }

        switch (calificacion)
        {
            case TipoCalificacion.texto: TipoCalificationTexto(); break;

            case TipoCalificacion.fondo: TipoCalificationFondo(); break;

            case TipoCalificacion.simbolo: TipoCalificationSimbolo(); break;

            case TipoCalificacion.multiple: _FunctionMultiple.Invoke(); break;
        }

        int groupTemp = 0;

        for (int i = 0; i < _groupInputField.Count; i++)
        {
            if (contadorTemp[i] == _groupInputField[i]._inputFields.Count)
                groupTemp++;
        }

        if (_TipoPuntuacion == TipoPuntuacion.grupo)
            _controlPuntaje.IncreaseScore(groupTemp);
        else
        {
            int total = 0;

            foreach (var grupoAnswers in contadorTemp)
                total += grupoAnswers;

            //print(total);

            _controlPuntaje.IncreaseScore(total);
        }


        //_controlAudio.PlayAudio(groupTemp == _groupInputField.Count ? 1 : 2);
        if (groupTemp == _groupInputField.Count) {
            _controlAudio.PlayAudio(1);
            StartCoroutine(y(goT, 3));
        }
        else {
            _controlAudio.PlayAudio(2);
            //v--;
            //if (v < vidas.transform.childCount && v != 0)
            //{
            //    vidas.transform.GetChild(v).gameObject.SetActive(false);
            //    StartCoroutine(x());
            //}
            //if (v == 0)
            //{
                StartCoroutine(y(goF,2));
            //}
        }
        
        

    }
    IEnumerator y(int value,int seg)
    {
        yield return new WaitForSeconds(seg);
        _controlNavegacion.GoToLayout(value);
    }

    IEnumerator x() {
        yield return new WaitForSeconds(1);
        resetAll_1();
    }
    public void TipoCalificationTexto()
    {
        foreach (var group in _listAnswers)
            for (int i = 0; i < group.inputFields.Length; i++)
                SetTextAnswer(group.inputFields[i].GetComponent<Image>(), group.answers[i]);

    }

    public void TipoCalificationFondo()
    {
        foreach (var group in _listAnswers)
            for (int i = 0; i < group.inputFields.Length; i++)
                SetFondoAnswer(group.inputFields[i].GetComponent<Image>(), group.answers[i]);


    }

    public void TipoCalificationSimbolo()
    {
        foreach (var group in _listAnswers)
            for (int i = 0; i < group.inputFields.Length; i++)
            {
                SetSymbolAnswer(group.inputFields[i].transform.GetChild(2).gameObject, group.answers[i]);
                group.inputFields[i].transform.GetChild(2).gameObject.SetActive(true);
            }

    }

    void SetFondoAnswer(Image image, bool state) => image.color = state ? colorFondoCorrecto : colorFondoIncorrecto;

    void SetSymbolAnswer(GameObject symbol, bool state)
    {
        Image i = symbol.GetComponent<Image>();
        BehaviourSprite bh = symbol.GetComponent<BehaviourSprite>();
        i.sprite = state ? bh._right : bh._wrong;
    }

    void SetTextAnswer(Image image, bool state) {
        if (state)
        {
            image.sprite = image.gameObject.GetComponent<BehaviourSprite>()._right;
            cofre.GetComponent<Image>().sprite = cofre.GetComponent<BehaviourSprite>()._selection;
            player.GetComponent<Animator>().enabled = true;
            player.GetComponent<M6L107_Castel>().e = true;
        }
        else {
            image.sprite = image.gameObject.GetComponent<BehaviourSprite>()._wrong;
        }
    }


    public void resetAll()
    {
        InitializeState();
        cofre.GetComponent<Image>().sprite = cofre.GetComponent<BehaviourSprite>()._default;
        v = 3;
        for (int i = 0; i < vidas.transform.childCount; i++)
        {
            vidas.transform.GetChild(i).gameObject.SetActive(true);
        }
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<M6L107_Castel>().e = false;
        player.GetComponent<RectTransform>().anchoredPosition = player.GetComponent<M6L107_Castel>().inicio;
    }
    public void resetAll_1()
    {
        InitializeState();
        _controlPuntaje.resetScore();
        
    }

}
