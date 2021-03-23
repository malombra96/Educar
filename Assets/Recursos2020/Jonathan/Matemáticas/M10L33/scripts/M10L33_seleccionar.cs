using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_seleccionar : MonoBehaviour
{

    ControlAudio _controlAudio;
    Toggle _toggle;
    [Header("is Right?")] public bool isRight;
    public M10L33_groupToogle _behaviourToggleGroup;
    public M10L33_managerSeleccionar _managerToggle;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(delegate { SetStateToggle(); });

    }


    void SetStateToggle()
    {
        //print(_toggle+"////"+_toggle.isOn);

        switch (_managerToggle._TypeSelect)
        {
            case M10L33_managerSeleccionar.TypeSelection.onexgroup:

                if (_toggle.isOn)
                {
                    _managerToggle._controlAudio.PlayAudio(0);
                    SetStateToggle(GetComponent<Image>(), _toggle.isOn);

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

                        case M10L33_managerSeleccionar.TypeValidation.inmediata:

                            _managerToggle.EvaluateGroupsToggleImmediately(_behaviourToggleGroup);

                            break;
                    }


                }
                else
                {
                    SetStateToggle(GetComponent<Image>(), _toggle.isOn);
                    if (_behaviourToggleGroup._dictionarySelection._toggle.Contains(_toggle))
                    {
                        _behaviourToggleGroup._dictionarySelection._toggle.RemoveAt(0);
                        _behaviourToggleGroup._dictionarySelection._state.RemoveAt(0);
                    }
                }

                break;

            case M10L33_managerSeleccionar.TypeSelection.variousxgroup:

                if (_toggle.isOn)
                {
                    if (_behaviourToggleGroup._dictionarySelection._toggle.Count > _managerToggle._options - 1)
                    {
                        //print("Change");
                        //print(_behaviourToggleGroup._dictionarySelection._toggle[_managerToggle._options-1]);

                        Toggle tg = _behaviourToggleGroup._dictionarySelection._toggle[_managerToggle._options - 1];

                        SetStateToggle(tg.GetComponent<Image>(), tg.isOn);
                        tg.isOn = false;

                    }

                    if (_behaviourToggleGroup._dictionarySelection._toggle.Count < _managerToggle._options)
                    {
                        //print("Add"+_toggle+"//"+_toggle.isOn);
                        _managerToggle._controlAudio.PlayAudio(0);
                        SetStateToggle(GetComponent<Image>(), _toggle.isOn);

                        _behaviourToggleGroup._dictionarySelection._toggle.Add(_toggle);
                        _behaviourToggleGroup._dictionarySelection._state.Add(isRight);

                        //print(_managerToggle._options+"/"+_behaviourToggleGroup._dictionarySelection._toggle.Count);
                    }
                }
                else
                {
                    if (_behaviourToggleGroup._dictionarySelection._toggle.Contains(_toggle))
                    {
                        SetStateToggle(GetComponent<Image>(), _toggle.isOn);

                        int index = _behaviourToggleGroup._dictionarySelection._toggle.IndexOf(_toggle);

                        _behaviourToggleGroup._dictionarySelection._toggle.RemoveAt(index);
                        _behaviourToggleGroup._dictionarySelection._state.RemoveAt(index);
                    }
                }

                break;
        }
    }

    /// <summary>
    /// Asigna sprite default o seleccion de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    void SetStateToggle(Image image, bool state)
    {
        image.sprite = state ?
            image.GetComponent<BehaviourSprite>()._selection :
            image.GetComponent<BehaviourSprite>()._default;
    }

}
