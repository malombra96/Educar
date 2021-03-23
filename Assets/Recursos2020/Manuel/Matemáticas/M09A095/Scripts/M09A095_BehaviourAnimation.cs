using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A095_BehaviourAnimation : MonoBehaviour
{
    ControlAudio _controlAudio;
    Animator _animator;
    public Button _playAnimation;
    
    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _animator = GetComponent<Animator>();
        _playAnimation.onClick.AddListener(SetAnimator);
    }

    void SetAnimator()
    {
        _animator.Play("StartAnimation");
    }
}
