using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M06A088_Despegues : MonoBehaviour
{
    
    public M06A088_ManagerDD _ManagerDD;
    BehaviourLayout _Layout;
    Animator _animator;

    ControlNavegacion _controlNavegacion;

    void OnEnable()
    {
        _Layout = GetComponent<BehaviourLayout>();
        _animator = GetComponent<Animator>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _animator.SetInteger("state",(_ManagerDD.rights == _ManagerDD._drags.Count)? 2:1); 
        _controlNavegacion.Forward(6);

    }

}
