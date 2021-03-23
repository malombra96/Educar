using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M07L122_Shape : MonoBehaviour
{
    [Header("Dpad Arrows")] public Button _left;public Button _right;public Button _up;public Button _down;
    [Header("Moving Shape")] public GameObject _shape;
    [Header("Default POS")] public Vector2 defaultPos;
    [Header("Delta")] public float _delta;
    [Header("Enabled Inputs")] public GameObject _enable;

    [Header("History Moving")] public Vector2 _history;

    [Header("Right Moving")] public Vector2 _rightMov;

    [Header("State Moving")] public bool _isRight;

    [Header("Limit Moving")] 

    public Vector2 _limitPositive;
    public Vector2 _limitNegative;


    // Start is called before the first frame update
    void Start()
    {
        _shape.gameObject.SetActive(false);
        _enable.SetActive(true);

        //defaultPos = _shape.GetComponent<RectTransform>().anchoredPosition;

        _down.onClick.AddListener(delegate{MoveShape("Y",-1);});
        _up.onClick.AddListener(delegate{MoveShape("Y",1);});
        _left.onClick.AddListener(delegate{MoveShape("X",-1);});
        _right.onClick.AddListener(delegate{MoveShape("X",1);});

        _isRight=false;
    }

    // Update is called once per frame
    void MoveShape(string axis,float sentido)
    {
        Vector2 temp = new Vector2();

        if(axis == "X")
        {
            _history.x+=sentido;

            if(_history.x <= _limitPositive.x && _history.x>= _limitNegative.x)
                temp.x += _delta*sentido;
            else
                _history.x = _history.x >= _limitPositive.x? _limitPositive.x : _history.x <= _limitNegative.x? _limitNegative.x : _history.x;

        }
        else if(axis == "Y")
        {
            _history.y+=sentido;

            if(_history.y <= _limitPositive.y && _history.y>= _limitNegative.y)
                temp.y += _delta*sentido;
            else 
                _history.y = _history.y >= _limitPositive.y? _limitPositive.y : _history.y <= _limitNegative.y? _limitNegative.y : _history.y;
        }            

        _shape.GetComponent<RectTransform>().anchoredPosition += temp;

        _shape.SetActive(defaultPos != _shape.GetComponent<RectTransform>().anchoredPosition);
        _enable.SetActive(defaultPos == _shape.GetComponent<RectTransform>().anchoredPosition);

        _isRight = (_history == _rightMov);

    }

    public void ResetShape()
    {
        _shape.gameObject.SetActive(false);
        _enable.SetActive(true);

        _shape.GetComponent<RectTransform>().anchoredPosition = defaultPos;
        _history = Vector2.zero;

        _isRight = false;
        
    }
}
