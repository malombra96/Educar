using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L001_BehaviourDropDown : MonoBehaviour
{
    M10L001_ManagerDropDown _managerDropDown;
    
    [Header("Index Option Correct")] public int _indexRight;
    [Header("Index Current")] public int _indexCurrent;
    
    [HideInInspector] [Header("State Executed")] public bool state;

    [HideInInspector] [Header("State Reset")] public bool reset;
    
    public enum Type
    {
        text,
        image
    }

    [Header("DropDown Type")] public Type _type;

    void Start()
    {
        GetComponent<Dropdown>().value = 0;
        GetComponent<Dropdown>().onValueChanged.AddListener(delegate { GetValueCurrent(GetComponent<Dropdown>().value); });

        _managerDropDown = FindObjectOfType<M10L001_ManagerDropDown>();
        _managerDropDown._dropdowns.Add(this);

        state = false;
        reset = false;
    }

    void GetValueCurrent(int n)
    {
        if (!reset)
        {
            _indexCurrent = n;
            state = _indexCurrent != 0;
            _managerDropDown._controlAudio.PlayAudio(0);
            _managerDropDown.StateBtnValidar();    
        }
    }

}
