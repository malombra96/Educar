using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A082_DropDown : MonoBehaviour
{
    M09A082_ManagerDropDown _managerDropDown;
    
    [Header("Index Option Correct")] public int _indexRight;
    [Header("Index Current")] public int _indexCurrent;
    
    [HideInInspector] [Header("State Executed")] public bool state;

    [HideInInspector] [Header("State Reset")] public bool reset;

    [Header("List State Sprites")] 

    public List<Sprite> _default;
    public List<Sprite> _right;
    public List<Sprite> _wrong;


    void Start()
    {
        GetComponent<Dropdown>().value = 0;
        GetComponent<Dropdown>().onValueChanged.AddListener(delegate { GetValueCurrent(GetComponent<Dropdown>().value); });

        _managerDropDown = FindObjectOfType<M09A082_ManagerDropDown>();
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
