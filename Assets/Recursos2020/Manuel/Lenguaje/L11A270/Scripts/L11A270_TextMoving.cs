using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L11A270_TextMoving : MonoBehaviour
{
    [Header("Layout DragDrop")] public BehaviourLayout _layout;
    [Header("BehaviourPlayer")] public L11A270_BehaviourPlayer _BehaviourPlayer;
    [Header("Sigth Camera")] public GameObject _sigthCamera;
    ControlAudio _controlAudio;

    [Header("Arrow")] public Toggle _arrow;
    RectTransform _rect;

    [Header("Instruction Image")] public Image _imageInstruction;

    [Header("Sprites Instructions")] public Sprite[] _instructions = new Sprite[3];
    

    void OnEnable()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _arrow.onValueChanged.AddListener(delegate{SetState(_arrow.isOn);});
        _arrow.isOn = false;

        _rect = GetComponent<RectTransform>();
        _rect.anchoredPosition = new Vector2(-1355,-91);
        _arrow.GetComponent<RectTransform>().localScale = new Vector3(-1,1,1);

        if(_layout._isEvaluated)
            _arrow.isOn = true;

            _imageInstruction.sprite =  Application.isMobilePlatform? _instructions[1] : _instructions[0]; 
       
    }

    void SetState(bool state)
    {
        _controlAudio.PlayAudio(0);
        _rect.anchoredPosition = state? new Vector2(-60,-91) : new Vector2(-1355,-91);
        _arrow.GetComponent<RectTransform>().localScale = state? Vector3.one : new Vector3(-1,1,1);
        _BehaviourPlayer.enabled = !state;
        _sigthCamera.SetActive(!state); 

        if(Application.isMobilePlatform)
             _imageInstruction.sprite =  state? _instructions[2] : _instructions[1]; 
        else
             _imageInstruction.sprite =  state? _instructions[2] : _instructions[0]; 

        
        
    }
}
