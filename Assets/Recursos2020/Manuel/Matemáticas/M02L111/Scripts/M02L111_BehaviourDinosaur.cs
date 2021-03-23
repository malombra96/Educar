using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M02L111_BehaviourDinosaur : MonoBehaviour
{
    public List<Sprite> _sizesRight;
    public List<Sprite> _sizesWrong;

    [Header("Current Size")] public int _currentIndex;

    [Header("CorrectSize")] public bool _correctSize;

    void Start() => ResetDinosaur();

    public void SetAnswerSize(bool state)
    {
        if(state)
        {
            _currentIndex++;
            GetComponent<Image>().sprite = _sizesRight[_currentIndex];

             _correctSize = (_currentIndex == (_sizesRight.Count-1));
        }
        else
        {
            GetComponent<Image>().sprite = _sizesWrong[_currentIndex];
        }
        
        print(_currentIndex);
    }

    public void ResetDinosaur()
    {
        _correctSize = false;
        _currentIndex = 0;
        GetComponent<Image>().sprite = _sizesRight[0];
    }


    
}
