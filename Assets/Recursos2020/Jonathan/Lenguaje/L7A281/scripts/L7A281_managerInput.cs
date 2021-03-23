using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class L7A281_managerInput : MonoBehaviour
{
    public L7A281_managerGeneral general;
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

    public List<L7A281_groupInput> _groupInputField;

    [Header("Button Validar")] public Button botonValidar;

    [HideInInspector] public ControlAudio _controlAudio;

    ControlPuntaje _controlPuntaje;

    ControlNavegacion _controlNavegacion;

    [HideInInspector] public bool haCalificado;

  


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
                if (inputField.GetComponent<L7A281_input>()._isEmpty)
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

                inputField.GetComponent<L7A281_input>()._isRight = false;
                inputField.GetComponent<L7A281_input>()._isEnabled = true;
                inputField.GetComponent<Image>().color = colorFondoInicial;

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

            if (_groupInputField[i]._TipoRespuestas == L7A281_groupInput.TipoRespuestas.individual)
            {
                for (int j = 0; j < _groupInputField[i]._inputFields.Count; j++)
                {
                    InputField f = _groupInputField[i]._inputFields[j];

                    f.interactable = false;
                    f.GetComponent<L7A281_input>()._isEnabled = false;

                    bool answer;
                    answer = (f.GetComponent<InputField>().text.Length >= f.GetComponent<L7A281_input>().caracteres);
                    _listAnswers[i].inputFields[j] = f;
                    _listAnswers[i].answers[j] = answer;


                    if (answer)
                    {
                        f.GetComponent<InputField>().textComponent.color = colorTextoCorrecto;
                        contadorTemp[i]++;
                    }
                    else {
                        f.GetComponent<InputField>().textComponent.color = colorTextoIncorrecto;
                    }
                    
                }
            }
        }

        switch (calificacion)
        {
            case TipoCalificacion.texto: TipoCalificationTexto(); break;
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

        
        _controlAudio.PlayAudio(groupTemp == _groupInputField.Count ? 1 : 2);
        //_controlNavegacion.Forward(2);
        general.NextExcersise();

    }

    public void TipoCalificationTexto()
    {
        print("x");
        foreach (var group in _listAnswers)
            for (int i = 0; i < group.inputFields.Length; i++)
                SetTextAnswer(group.inputFields[i].transform.GetChild(1).GetComponent<Text>(), group.answers[i]);
    }

    void SetTextAnswer(Text text, bool state) {
        print("s");
        text.color = state ? colorTextoCorrecto : colorTextoIncorrecto;
    } 


    public void resetAll()
    {
        InitializeState();
        foreach (var group in _listAnswers) {
            for (int i = 0; i < group.inputFields.Length; i++)
            {
                group.inputFields[i].transform.GetChild(1).GetComponent<Text>().text = "";
                group.inputFields[i].transform.GetChild(1).GetComponent<Text>().color = Color.black;
            }
        }
       
               
        
    }

}
