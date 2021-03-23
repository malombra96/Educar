using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M04A113_Bar : MonoBehaviour
{
    [Header("RightAnswer")] public int _rightAnswer;
    [Header("CurrentAnswer")] public int _currentAnswer;

    //[Header("State")] public bool _stateValue;

    Slider _slider;
    M04A113_ManagerBar _managerBar;

    [Header("Multipliquer Slider")] public int _multipliquer;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(delegate{SaveSlider(_slider.value);});

        _managerBar = transform.parent.parent.GetComponent<M04A113_ManagerBar>();
    }

    void SaveSlider(float value)
    {
        _currentAnswer = Mathf.RoundToInt(value)*_multipliquer;
        _managerBar._controlAudio.PlayAudio(0);
        //_stateValue = _currentAnswer!=0;
        //_managerBar.StateValidarBTN();

    }

}

