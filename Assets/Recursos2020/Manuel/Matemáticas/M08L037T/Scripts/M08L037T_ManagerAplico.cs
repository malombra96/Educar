using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_ManagerAplico : MonoBehaviour
{
    ManagerDrawLine _ManagerDrawLine;
    public Image[] _inecuaciones;
    public Toggle[] _leftToggles;

    void Start()
    {
        _ManagerDrawLine = FindObjectOfType<ManagerDrawLine>();
    }

    public void GetLinesDraw()
    {
        foreach (var line in _ManagerDrawLine._lines)
        {
            var x = line.toggle[0].GetComponent<BehaviourDrawToggle>();
            var y = line.toggle[1].GetComponent<BehaviourDrawToggle>();
    
            SetAnswerSprite(

                x.GetComponent<Toggle>(),
                y.GetComponent<Toggle>(),(
                x._match.Contains(line.toggle[1]) || y._match.Contains(line.toggle[0]))
                
                );    
        }
    }

    void SetAnswerSprite(Toggle x, Toggle y, bool answer)
    {
        for (int k = 0; k < _leftToggles.Length; k++)
            if(_leftToggles[k] == x || _leftToggles[k] == y)
                _inecuaciones[k].GetComponent<Image>().sprite = answer? _inecuaciones[k].GetComponent<BehaviourSprite>()._right : _inecuaciones[k].GetComponent<BehaviourSprite>()._wrong;
            
    }

    public void ResetAnswerSprite()
    {
        foreach (Image innecuacion in _inecuaciones)
            innecuacion.GetComponent<Image>().sprite = innecuacion.GetComponent<BehaviourSprite>()._default;
    }


}
