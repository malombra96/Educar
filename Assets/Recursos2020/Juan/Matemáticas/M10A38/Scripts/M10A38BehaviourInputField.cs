using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class M10A38BehaviourInputField : MonoBehaviour, IPointerClickHandler
{
    M10A38ManagerInputField _managerInputField;
    InputField _input;

    [HideInInspector] [Header("State Interaction")] public bool _isEnabled;
    [HideInInspector] [Header("State Empty")] public bool _isEmpty;
    [HideInInspector] [Header("State Answer")] public bool _isRight;

    List<string> _operatorsMath = new List<string> { "+", "-", "*", "/" };
    [Header("Simbolos Matemáticos")] public List<string> _charactersMath = new List<string> { "-", "<", ">", "%", "{", "}", "[", "]", "°" };
    [Header("Tipo de contenido matemático del input")] public bool _isMath;
    [Header("Ingrese las respuestas correctas")] public List<string> respuestaCorrecta;


    void Awake()
    {
        _isEmpty = true;
        _isEnabled = true;
        _isRight = false;

        _managerInputField = FindObjectOfType<M10A38ManagerInputField>();

        _input = GetComponent<InputField>();

        _input.onValueChanged.AddListener(delegate { SetStateEmpty(_input.text); });
        _input.onEndEdit.AddListener(delegate { SetOperationMath(_input.text); });

        if (Application.isMobilePlatform)
            _input.contentType = InputField.ContentType.Standard;

        if (_isMath)
            _input.contentType = InputField.ContentType.Standard;
    }

    public void SetStateEmpty(string s)
    {
        _isEmpty = string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);

        if (s.Length == 1)
        {
            bool x = string.IsNullOrWhiteSpace(s[0].ToString());
            bool y = int.TryParse(s[0].ToString(), out int n);
            bool z = _charactersMath.Any(c => s[0].ToString().Contains(c));

            if (x)
                _input.text = "";
            else if (!y && !z)
                _input.text = "";

        }
        else
            CheckInput(s);


        _managerInputField.SetStateValidarBTN();

    }

    void CheckInput(string s)
    {
        int value;
        string[] cs = s.ToCharArray().Select(c => c.ToString()).ToArray();

        CheckMultipleOperators(cs);

        for (int i = 1; i < cs.Length; i++)
        {
            bool x = !_operatorsMath.Where(c => c.Contains(cs[i])).Contains(cs[i]);
            bool y = !int.TryParse(cs[i], out int n);

            bool z = _operatorsMath.Where(c => c.Contains(cs[i])).Contains(cs[i]) && _operatorsMath.Where(c => c.Contains(cs[i - 1])).Contains(cs[i - 1]);

            if ((x && y) || z)
                _input.text = _input.text.Replace(cs[i], string.Empty);

        }

        //print(_operatorsMath.Any(_input.text.Contains));
        //print(_operatorsMath.Any(c => _input.text.Contains(c)));

    }

    void CheckMultipleOperators(string[] cs)
    {
        int k = 0;

        for (int i = 1; i < cs.Length; i++)
        {
            for (int j = 0; j < _operatorsMath.Count; j++)
            {
                if (cs[i] == _operatorsMath[j])
                {
                    k++;

                    if (k > 1)
                        _input.text = _input.text.Replace(cs[i], string.Empty);

                }
            }
        }
    }

    void SetOperationMath(string s)
    {
        if (_operatorsMath.Any(c => _input.text.Contains(c) && s.Length >= 3)) //(_operatorsMath.Any(s.Contains)
        {
            string num = _input.text;
            string[] ff = num.Split('+', '-', '*', '/');

            int a = int.Parse(num.Split('+', '-', '*', '/')[string.IsNullOrEmpty(ff[0]) ? 1 : 0]);
            int b = int.Parse(num.Split('+', '-', '*', '/')[string.IsNullOrEmpty(ff[0]) ? 2 : 1]);

            if (string.IsNullOrEmpty(ff[0]))
                a = -a;

            float c = 0;
            float d = 0;

            string o = GetTypeOperation(s);

            switch (o)
            {
                case "+": c = a + b; break;

                case "-": c = a - b; break;

                case "*": c = a * b; break;

                case "/": c = a / b; d = a % b; break;
            }

            print(c + "/" + d);

            if (o != "/")
                _input.text = c.ToString();
            else if (d == 0)
                _input.text = c.ToString();
        }
    }

    string GetTypeOperation(string s)
    {
        for (int i = 1; i < s.Length; i++)
            for (int j = 0; j < _operatorsMath.Count; j++)
                if (s[i].ToString() == _operatorsMath[j])
                    return _operatorsMath[j];

        return "";
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Application.isMobilePlatform && _isEnabled)
        {
            _managerInputField._controlAudio.PlayAudio(0);

            ManagerDisplay _managerDisplay = _managerInputField._lightBox.transform.GetChild(0).GetChild(0).GetComponent<ManagerDisplay>();

            _managerDisplay.currentInput = GetComponent<InputField>();
            _managerDisplay.limiteCaracteres = GetComponent<InputField>().characterLimit;

            _managerInputField._lightBox.SetActive(true);
            _managerInputField._lightBox.GetComponent<Animator>().Play("NumPad_in");
        }
    }
}
