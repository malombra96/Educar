using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_StepC : MonoBehaviour
{
    M08L037T_ManagerStep _managerStep;
    M08L037T_BehaviourGrid _grid;
    ControlAudio _controlAudio;
    ToggleGroup _group;
    [Header("Options Toggle")] public Toggle[] _toggle;

    [Header("Transform Mask")] public Transform _mask;

    [Header("DottedLine Prefab")] public GameObject _dotted;
    [Header("CpntinuousLine Prefab")] public GameObject _continuous;

    [Header("Limites-Points Line")] 
    public Vector2 min;
    public Vector2 max;
    public float ang;

    GameObject x;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _managerStep = FindObjectOfType<M08L037T_ManagerStep>();
        _grid = FindObjectOfType<M08L037T_BehaviourGrid>();
        _group = GetComponent<ToggleGroup>();

        foreach (Toggle t in _toggle)
            t.onValueChanged.AddListener(delegate{SetStateToggle(t);});

        _managerStep._validar.onClick.AddListener(delegate{SetStateToggle(false);});
        
    }

    void SetStateToggle(Toggle t)
    {
        if (!_managerStep._inReset)
        {
            SetSpriteToggle(t.GetComponent<Image>(), t.isOn);

            if (t.isOn)
            {
                _controlAudio.PlayAudio(0);
                DrawLine(t);
            }

            if (!_group.AnyTogglesOn())
                ClearLine();
            else
                _managerStep.SetNextStep(3);
        }            
    }

    void DrawLine(Toggle t)
    {
        min = GetLimits(8).Item1;
        max = GetLimits(8).Item2;

        print($"A={min}-B={max}");

        string a = "[" + min.x + "," + min.y + "]";
        string b = "[" + max.x + "," + max.y + "]";

        Vector3 posMin = _grid.GetPositionPoint(a);
        posMin.z-=.1f;
        Vector3 posMax = _grid.GetPositionPoint(b);
        posMax.z-=.1f;

        //print($"{posMin}-{posMax}");

        ClearLine();

        if(t.name == "DottedLine")
        {
            x = Instantiate(_dotted,_mask); //_dotted,transform.parent
            x.GetComponent<LineRenderer>().SetPosition(0, posMin);
            x.GetComponent<LineRenderer>().SetPosition(1, posMax);
        }
        else if(t.name == "ContinuousLine")
        {
            x = Instantiate(_continuous,_mask); //_continuous,transform.parent
            x.GetComponent<LineRenderer>().SetPosition(0, posMin);
            x.GetComponent<LineRenderer>().SetPosition(1, posMax);
        }

        _managerStep._lineSelect = t;
        _managerStep._line = x;
    }

    (Vector2,Vector2) GetLimits(int limit)
    {

        float y0 = _managerStep._pointA.y;
        float y1 = _managerStep._pointB.y;
        float x0 = _managerStep._pointA.x;
        float x1 = _managerStep._pointB.x;
        
        //print (x0 + " / " + x1 + " / " + y0 + " / " + y1);

        float dx = (x1) - (x0);
        float dy = (y1) - (y0);

        Vector2 A = Vector2.zero;
        Vector2 B = Vector2.zero;

        if (dx < 0 || dx > 0)
        {
            float m = dy / dx;

            float b = ((-1) * (m * x0)) + y0;

            ang = Mathf.Rad2Deg*Mathf.Atan(m);

            print($"y={m}x+{b}     Theta={ang}");

            float y_0 = m * (limit) + b;
            float y_1 = m * (-limit) + b;

            float x_0 = (limit - b) / m;
            float x_1 = (-limit - b) / m;

            //print ("y: " + y_0 + " y_1: " + y_1 + " x: " + x_0 + " x_1: " + x_1);

            if (m == 0)
            {
                A = new Vector2(limit,y_0);
                B = new Vector2(-limit,y_1);
                //print($"A=[7,{y_0}] - B=[-7,{y_1}]");
            }  
            else if (m < 0 || m > 0)
            {

                if (y_0 > limit)
                    A = new Vector2(x_0,limit);//print($"A=[{x_0},{7}]");
                else if (y_0 < -limit)
                    A = new Vector2(x_1,-limit);//print($"A=[{x_1},{-7}]");
                else
                    A = new Vector2(limit,y_0);//print($"A=[7,{y_0}]");


                if (y_1 > limit)
                     B = new Vector2(x_0,limit);//print($"B=[{x_0},{7}]");
                else if (y_1 < -8)
                     B = new Vector2(x_1,-limit);//print($"B=[{x_1},{-7}]");
                else
                     B = new Vector2(-limit,y_1);//print($"B=[-7,{y_1}]");

            }
        }
        else
        {
            A = new Vector2(x0,limit);
            B = new Vector2(x1,-limit);
            //print($"A=[{x0},{7}] - B=[{x1},{-7}]");
        }
              
        return(A,B);

    }


    void ClearLine()
    {
        if(x)
            Destroy(x);

        _managerStep.SetNextStep(2);
    }


    /// <summary>
    /// Asigna sprite default o seleccion de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    void SetSpriteToggle(Image image, bool state)
    {
        image.sprite = state? 
            image.GetComponent<BehaviourSprite>()._selection : 
            image.GetComponent<BehaviourSprite>()._default;
    }

    public void SetStateToggle(bool state)
    {
        foreach (Toggle t in _toggle)
            t.interactable = state;
    }

    public void ResetC()
    {
        Vector2 min = Vector2.zero;
        Vector2 max = Vector2.zero;
        float ang = 0;

        foreach (Toggle t in _toggle)
        {
            t.isOn = false;
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
            t.interactable = true;
        }  

        if(x)
        {
            Destroy(x); 
            x=null;
        }
            
    }
}
