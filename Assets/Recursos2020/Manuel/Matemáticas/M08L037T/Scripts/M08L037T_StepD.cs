using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_StepD : MonoBehaviour
{
    ControlAudio _controlAudio;
    M08L037T_ManagerStep _managerStep;

    M08L037T_StepC _stepC;

    [Header("Grid")] public M08L037T_BehaviourGrid _grid;

    [Header("Point Input")]
    public InputField[] _pointC = new InputField[2];

    ToggleGroup _group;
    [Header("Options Toggle")] public Toggle[] _toggle;

    string[] currentPoint = new string[2];
    int t;

    [Header("Selection Zone")]

    public Image _select;

    public bool _changeRegion;

    float ang;

    [Header("Advertence over Line")] 
    
    public Image _advertence;
    bool _overLine;

    void Start()
    {
        t=0;

        _overLine= false;

        _advertence.gameObject.SetActive(false);

        _controlAudio = FindObjectOfType<ControlAudio>();

        _managerStep = FindObjectOfType<M08L037T_ManagerStep>();

        _stepC = FindObjectOfType<M08L037T_StepC>();

        //_grid = FindObjectOfType<M08L037T_BehaviourGrid>();

        _group = GetComponent<ToggleGroup>();

        if (!Application.isMobilePlatform)
        {
            foreach (var item in _pointC)
                item.onEndEdit.AddListener(delegate { CheckStep_4(item); });
        }
        else
        {
            foreach (var item in _pointC)
                item.onValueChanged.AddListener(delegate { CheckStep_4(item); });
        }

        foreach (Toggle t in _toggle)
            t.onValueChanged.AddListener(delegate{SetStateToggle(t);});


        _managerStep._validar.onClick.AddListener(delegate{SetStateElements(false);});

        CheckStateInteraction();
        
    }
    void GetMidPoint()
    {
        ang = 0;
        _select.GetComponent<RectTransform>().SetParent(transform.parent);
        _grid = FindObjectOfType<M08L037T_BehaviourGrid>();


        Vector2 posA = _grid.GetAnchoredPosition("[" + _stepC.min.x + "," + _stepC.min.y + "]"); 
        Vector2 posB = _grid.GetAnchoredPosition("[" + _stepC.max.x + "," + _stepC.max.y + "]"); 

        Vector2 midAB =  new Vector2((((posA.x)+(posB.x))/2),(((posA.y)+(posB.y))/2));

        print($"PosA{posA},posB{posB}Mid{midAB}");

        ang = _changeRegion? (_stepC.ang-180) : _stepC.ang;

        _select.GetComponent<RectTransform>().anchoredPosition = midAB;
        _select.GetComponent<RectTransform>().localEulerAngles = new Vector3(0,0,ang);
        _select.GetComponent<RectTransform>().SetParent(transform.parent.GetChild(1));
        
    }

    void CheckStep_4(InputField point)
    {
        if (!_managerStep._inReset)
        {
            t = 0;

            foreach (var input in _pointC)
            {
                if (!string.IsNullOrEmpty(input.text))
                {
                    if ((int.Parse(input.text) <= 7) && (int.Parse(input.text) >= -7))
                    {
                        if (!string.IsNullOrEmpty(input.text))
                            t++;
                    }
                    else
                    {
                        SetAdvertence(true);
                    }

                }
            }

            if (t == _pointC.Length)
            {
                GetMidPoint();
                SetAdvertence(false);
                SetPoint(currentPoint[0], currentPoint[1], false);

                currentPoint[0] = _pointC[0].text;
                currentPoint[1] = _pointC[1].text;

                SetPoint(currentPoint[0], currentPoint[1], true);
                _managerStep._compliesWithInequality = SeparateInequality(currentPoint[0], currentPoint[1]);
                print($" point:{currentPoint[0]},{currentPoint[1]} is {_managerStep._compliesWithInequality}");
            }

            CheckStateInteraction();
        }

    }

    void SetPoint(string x,string y,bool state)
    {
        M08L037T_BehaviourGrid grid = FindObjectOfType<M08L037T_BehaviourGrid>();
        grid.SetPoint(x,y,state);
    }

    void SetStateToggle(Toggle t)
    {
        if (!_managerStep._inReset)
        {

            SetSpriteToggle(t.GetComponent<Image>(), t.isOn);

            if (t.isOn)
            {
                _controlAudio.PlayAudio(0);

                if (t.name == "Yes")
                    _select.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, ang);
                else
                    _select.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, ang - 180);


                _managerStep._region = _select;
                _managerStep._regionSelect = t;

                _select.gameObject.SetActive(true);
            }

            CheckStateInteraction();
        }
            
    }

    void SetSpriteToggle(Image image, bool state)
    {
        image.sprite = state? 
            image.GetComponent<BehaviourSprite>()._selection : 
            image.GetComponent<BehaviourSprite>()._default;
    }

    void CheckStateInteraction()
    {
        _managerStep._validar.interactable = (t == _pointC.Length && _group.AnyTogglesOn());

        if(!_group.AnyTogglesOn())
            _select.gameObject.SetActive(false);

        foreach (Toggle tt in _toggle)
            tt.interactable = ((t == _pointC.Length) && !_overLine);
    }

    #region Mathematical Process

        public bool SeparateInequality(string a, string b)
    {
        string[] ecu = _managerStep._desigualdad.ToCharArray().Select(c => c.ToString()).ToArray();
        string values = "";

        string left = "";
        string right = "";
        string logic = "";
        string operathor = "";
        string result = "";

        for (int i = 0; i < ecu.Length; i++)
        {
            if (ecu[i] == "<" || ecu[i] == ">" || ecu[i] == "≥" || ecu[i] == "≤")
            {
                logic = ecu[i];
                values = _managerStep._desigualdad.Split('<', '>', '≤', '≥')[0];
                result = _managerStep._desigualdad.Split('<', '>', '≤', '≥')[1];
                break;
            }
        }

        for (int j = 1; j < values.Length; j++) // Comienza en 1 para evitar '-' si lo hay
        {
            if (values[j] == '+')
            {
                left = values.Split('+')[0];
                right = values.Split('+')[1];
                operathor = "+";
                break;
            }
            else if (values[j] == '-')
            {
                left = (values[0] == '-') ? string.Concat("-", values.Split('-')[1]) : values.Split('-')[0];
                right = (values[0] == '-') ? values.Split('-')[2] : values.Split('-')[1];

                operathor = "-";
                break;
            }

        }

        _managerStep._logicOperathor = logic;

        print($" Desigualdad {left}/{operathor}/{right}{logic}{result}");

        float vx = GetValue(left, a);
        float vy = GetValue(right, b);
        float vr = float.Parse(result);

        print($" Values {vx}{operathor}{vy}{logic}{vr}");

        return MathOperation(vx, vy, operathor, logic, vr);


    }

    float GetValue(string s, string n)
    {
        float valor = 0;

        if (s.Length > 1)
        {
            string[] cs = s.Split('x', 'y');

            for (int i = 0; i < cs.Length; i++)
            {
                if (string.IsNullOrEmpty(cs[i]))
                    cs[i] = n;
            }

            valor = float.Parse(cs[0]) * float.Parse(cs[1]);
        }
        else
        {
            valor = float.Parse(n);
        }

        return valor;

    }

    bool MathOperation(float x, float y, string operathor, string log, float r)
    {
        float z = 0;
        bool b = false;

        switch (operathor)
        {
            case "+": z = x + y; break;
            case "-": z = x - y; break;
        }

        switch (log)
        {
            case ">":

                if (z != r)
                    b = (z > r);
                else
                    SetAdvertence(true);

            break;

            case "<":

                if (z != r)
                    b = (z < r);
                else
                    SetAdvertence(true);

            break;

            case "≥":

                if (z != r)
                    b = (z > r);
                else
                    SetAdvertence(true);

            break;

            case "≤":

                if (z != r)
                    b = (z < r);
                else
                    SetAdvertence(true);

            break;
        }

        return b;

    }

    #endregion

    void SetAdvertence(bool state)
    {
        _advertence.gameObject.SetActive(state);
        _overLine = state;

        foreach (Toggle tt in _toggle)
            tt.isOn = false;

    } 

    public void SetStateElements(bool state)
    {
        foreach (Toggle t in _toggle)
            t.interactable = state;

        foreach (InputField i in _pointC)
        {
            i.interactable = state;
            i.GetComponent<M08L037T_BehaviourInputField>()._isEnabled = state;
        }
    }

    public void ResetD()
    {
        t=0;
        ang = 0;
        _overLine= false;
        _advertence.gameObject.SetActive(false);
        currentPoint[0] = "";
        currentPoint[1] = "";
        _overLine = false;
        
        if(_grid)
            _grid.ResetGrid();

        foreach (Toggle t in _toggle)
        {
            t.isOn = false;
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
            t.interactable = true;
        }

        foreach (InputField i in _pointC)
        {
            i.transform.parent.GetComponent<Image>().sprite = i.transform.parent.GetComponent<BehaviourSprite>()._default;
            i.text = "";
            i.interactable = true;
            i.GetComponent<M08L037T_BehaviourInputField>()._isEnabled = true;
        }

        _select.GetComponent<Image>().color = _select.GetComponent<M08L037T_ColorState>()._default;
        _select.gameObject.SetActive(false);

        _select.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        _select.GetComponent<RectTransform>().localEulerAngles = Vector3.zero;
        _select.GetComponent<RectTransform>().SetParent(transform.parent);
    }
}
