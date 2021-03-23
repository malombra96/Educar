using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M8A57_Input_1 : MonoBehaviour, IPointerClickHandler
{
    [Header("Manager :")] public M8A57_Manager_Aplico_1 manager;
    private bool _isEnabled;
    void Start()
    {
        _isEnabled = true;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {

    }
    
}
