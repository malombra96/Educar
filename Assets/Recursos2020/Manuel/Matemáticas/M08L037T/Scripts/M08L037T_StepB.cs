using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_StepB : MonoBehaviour
{
    M08L037T_ManagerStep _managerStep;
    ControlAudio _controlAudio;

    public InputField[] _pointA = new InputField[2];
    public InputField[] _pointB = new InputField[2];
    public GameObject _advertencia_B,_advertencia_C;

    public int correct = 0; 

    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _managerStep = FindObjectOfType<M08L037T_ManagerStep>();
    }

    void Start()
    {

        if (!Application.isMobilePlatform)
        {
            foreach (var item in _pointA.Concat(_pointB))
                item.onEndEdit.AddListener(delegate { CheckStep_2(item); });

            foreach (var item in _pointA.Concat(_pointB))
            {
                if (_pointA.Contains(item))
                    item.onValueChanged.AddListener(delegate { _managerStep.SetWarning(_advertencia_B, false); });
                else
                    item.onValueChanged.AddListener(delegate { _managerStep.SetWarning(_advertencia_C, false); });
            }
        }
        else
        {
            foreach (var item in _pointA.Concat(_pointB))
                item.onValueChanged.AddListener(delegate { CheckStep_2(item); });

            foreach (var item in _pointA.Concat(_pointB))
            {
                if (_pointA.Contains(item))
                    item.onEndEdit.AddListener(delegate { _managerStep.SetWarning(_advertencia_B, false); });
                else
                    item.onEndEdit.AddListener(delegate { _managerStep.SetWarning(_advertencia_C, false); });
            }

        } 

        
            

    }

    void CheckStep_2(InputField point)
    {
        if (!_managerStep._inReset)
        {
            int[] t = new int[2];

            foreach (var input in _pointA.Concat(_pointB))
            {
                if (!string.IsNullOrEmpty(input.text) && !string.IsNullOrWhiteSpace(input.text) && int.TryParse(input.text, out int z))
                {
                    if ((int.Parse(input.text) <= 7) && (int.Parse(input.text) >= -7))
                    {
                        if (_pointA.Contains(input))
                            t[0]++;
                        else if (_pointB.Contains(input))
                            t[1]++;
                    }
                }

            }

            string Ax, Ay, Bx, By;

            Ax = _pointA[0].text;
            Ay = _pointA[1].text;

            Bx = _pointB[0].text;
            By = _pointB[1].text;

            //print($"Ax=[{Ax}],Ay=[{Ay}]");
            //print($"Bx=[{Bx}],By=[{By}]");

            if (_pointA.Contains(point))
            {
                if ((t[0] == _pointA.Length) && t[0]!= 0)
                {
                    if (Ax != Bx || Ay != By)
                    {
                        _managerStep._pointA = new Vector2(int.Parse(Ax), int.Parse(Ay));
                        EvaluatePoint(_pointA, _managerStep._pointA);
                    }
                }
                    

            }
            else
            {
                if ((t[1] == _pointB.Length) && t[1]!= 0)
                {
                    if (Bx != Ax || By != Ay)
                    {
                        _managerStep._pointB = new Vector2(int.Parse(Bx), int.Parse(By));
                        EvaluatePoint(_pointB, _managerStep._pointB);
                    }
                }
            }
        }
    }

    void EvaluatePoint(InputField[] input, Vector2 coordenada)
    {
        if (SeparateEquation(coordenada[0].ToString(), coordenada[1].ToString()))
        {
            StartCoroutine(DisableInput(input));
            _controlAudio.PlayAudio(1);
            _managerStep.SetAnswerSprite(input[0].transform.parent.GetComponent<Image>(), true);
            correct++;

            _managerStep.SetWarning(_advertencia_B, false);
            _managerStep.SetWarning(_advertencia_C, false);
            ActivePoint(coordenada[0].ToString(),coordenada[1].ToString());
            
        }
        else
        {
            _controlAudio.PlayAudio(2);
            _managerStep.SetAnswerSprite(input[0].transform.parent.GetComponent<Image>(), false);

            _advertencia_B.SetActive(input == _pointA);
            _advertencia_C.SetActive(input == _pointB);
                
        }

        if(correct==2)
        {
            _controlAudio.PlayAudio(1);
            _managerStep.SetNextStep(2);
        }
            
    }

    IEnumerator DisableInput(InputField[] group)
    {
        yield return new WaitForSeconds(.1f);

        foreach (var item in group)
        {
            item.interactable = false;
            item.GetComponent<M08L037T_BehaviourInputField>()._isEnabled = false;
        }
            
    }

    public bool SeparateEquation(string a,string b)
    {
        string[] ecu = _managerStep._equation.ToCharArray().Select(c => c.ToString()).ToArray();
        string values = "";
        
        string left = "";
        string right = "";
        string operathor = "";
        string result ="";

        for (int i = 0; i < ecu.Length; i++)
        {   
            if(ecu[i] == "=")
            {
                values = _managerStep._equation.Split('=')[0];
                result = _managerStep._equation.Split('=')[1];
                break;
            }
        }
            
        for (int j = 1; j < values.Length; j++) // Comienza en 1 para evitar '-' si lo hay
        {
            if(values[j] == '+')
            {
                left = values.Split('+')[0];
                right = values.Split('+')[1];
                operathor = "+";
                break;
            }
            else if(values[j] == '-')
            {
                left = (values[0] == '-')? string.Concat("-",values.Split('-')[1]) : values.Split('-')[0];
                right = (values[0] == '-')? values.Split('-')[2] : values.Split('-')[1];
                
                operathor = "-";
                break;
            }
            
        }

        print($" Ecuacion Separada {left}/{operathor}/{right}={result}");

        float vx = GetValue(left,a);
        float vy = GetValue(right,b);
        float vr = float.Parse(result);
 
        print($" Values {vx}{operathor}{vy}={vr}");

        return MathOperation(vx,vy,operathor,vr);
            
        
    }

    float GetValue(string s,string n)
    {
        float valor = 0;

        if(s.Length > 1)
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

    bool MathOperation(float x, float y , string operathor,float r)
    {
        float z = 0;

        switch (operathor)
        {
            case "+": z = x+y; break;
            case "-": z = x-y; break;
        }

        return (z==r);
        
    }

    void ActivePoint(string x,string y)
    {
        M08L037T_BehaviourGrid grid = FindObjectOfType<M08L037T_BehaviourGrid>();
        grid.SetPoint(x,y,true);
    }

    public void ResetB()
    {
        correct = 0;

        _pointA[0].transform.parent.GetComponent<Image>().sprite = _pointA[0].transform.parent.GetComponent<BehaviourSprite>()._default;
        _pointB[0].transform.parent.GetComponent<Image>().sprite = _pointB[0].transform.parent.GetComponent<BehaviourSprite>()._default;
        
        foreach (InputField i in _pointA.Concat(_pointB))
        {
            i.text = "";
            i.interactable = true;
            i.GetComponent<M08L037T_BehaviourInputField>()._isEnabled = true;
        }
        
    }

}
