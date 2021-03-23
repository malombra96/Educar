using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M06A088_Aterrizaje : MonoBehaviour
{
    public ManagerInputField _ManagerInput;
    BehaviourLayout _Layout;
    Animator _animator;

    ControlNavegacion _controlNavegacion;

    void OnEnable()
    {
        _Layout = GetComponent<BehaviourLayout>();
        _animator = GetComponent<Animator>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        _animator.SetInteger("state",GetStateAnswers()? 2:1); 


        _controlNavegacion.Forward(6);

    }

    bool GetStateAnswers()
    {
        if(_ManagerInput._listAnswers.Length > 0)
            return _ManagerInput._listAnswers[0].answers[0];
        else 
            return false;
    }
}
