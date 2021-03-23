﻿using UnityEngine;
using UnityEngine.UI;

public class L05A264_Toggle : MonoBehaviour
{
    ControlAudio _controlAudio;
    Toggle _toggle;
    [Header("is Right?")] public bool isRight;
    public L05A264_GroupToggle _behaviourToggleGroup;
    public L05A264_ManagerToggle _managerToggle;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _toggle = GetComponent<Toggle>();
        /* _behaviourToggleGroup = transform.parent.GetComponent<L05A264_GroupToggle>();
        _managerToggle = transform.parent.parent.GetComponent<L05A264_ManagerToggle>(); */
        _toggle.onValueChanged.AddListener(delegate{SetStateToggle();}); 
        
    }
    
    void SetStateToggle()
    {
        //print(_toggle+"////"+_toggle.isOn);

        _managerToggle._controlAudio.PlayAudio(0);

        switch (_managerToggle._TypeSelect)
        {
            case L05A264_ManagerToggle.TypeSelection.onexgroup:

                if (_toggle.isOn)
                {
                    SetSpriteToggle(GetComponent<Image>(),_toggle.isOn);

                    if (_behaviourToggleGroup._dictionarySelection._toggle.Count > 0)
                    {
                        _behaviourToggleGroup._dictionarySelection._toggle[0] = _toggle;
                        _behaviourToggleGroup._dictionarySelection._state[0] = isRight;
                    }
                    else
                    {
                        _behaviourToggleGroup._dictionarySelection._toggle.Add(_toggle);
                        _behaviourToggleGroup._dictionarySelection._state.Add(isRight);
                    }

                    switch (_managerToggle._typeVal)
                    {

                        case L05A264_ManagerToggle.TypeValidation.inmediata:

                            _managerToggle.EvaluateGroupsToggleImmediately(_behaviourToggleGroup);

                            break;
                    }

                    
                }
                else
                {
                    SetSpriteToggle(GetComponent<Image>(),_toggle.isOn);
                    if(_behaviourToggleGroup._dictionarySelection._toggle.Contains(_toggle))
                    {
                        _behaviourToggleGroup._dictionarySelection._toggle.RemoveAt(0);
                        _behaviourToggleGroup._dictionarySelection._state.RemoveAt(0);
                    }  
                }

                break;

            case L05A264_ManagerToggle.TypeSelection.variousxgroup:

                if(_toggle.isOn)
                {
                    if(_behaviourToggleGroup._dictionarySelection._toggle.Count > _managerToggle._options-1)
                    {
                        //print("Change");
                        //print(_behaviourToggleGroup._dictionarySelection._toggle[_managerToggle._options-1]);

                        Toggle tg = _behaviourToggleGroup._dictionarySelection._toggle[_managerToggle._options-1];

                        SetSpriteToggle(tg.GetComponent<Image>(),tg.isOn);
                        tg.isOn = false;

                    }
                    
                    if(_behaviourToggleGroup._dictionarySelection._toggle.Count < _managerToggle._options)
                    {
                        //print("Add"+_toggle+"//"+_toggle.isOn);
                        _managerToggle._controlAudio.PlayAudio(0);
                        SetSpriteToggle(GetComponent<Image>(),_toggle.isOn);

                        _behaviourToggleGroup._dictionarySelection._toggle.Add(_toggle);
                        _behaviourToggleGroup._dictionarySelection._state.Add(isRight);

                        //print(_managerToggle._options+"/"+_behaviourToggleGroup._dictionarySelection._toggle.Count);
                    }
                }
                else
                {
                    if(_behaviourToggleGroup._dictionarySelection._toggle.Contains(_toggle))
                    {
                        SetSpriteToggle(GetComponent<Image>(),_toggle.isOn);

                        int index = _behaviourToggleGroup._dictionarySelection._toggle.IndexOf(_toggle);

                        _behaviourToggleGroup._dictionarySelection._toggle.RemoveAt(index);
                        _behaviourToggleGroup._dictionarySelection._state.RemoveAt(index);
                    }  
                }

                break;
        }

        if(!_managerToggle.reset)
            StartCoroutine(_managerToggle.StateBtnValidar());
    }

    /// <summary>
    /// Asigna sprite default o seleccion de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    void SetSpriteToggle(Image image, bool state)
    {
        image.sprite = state? 
            image.GetComponent<BehaviourSprite>()._selection : 
            image.GetComponent<BehaviourSprite>()._default;
    }
}
