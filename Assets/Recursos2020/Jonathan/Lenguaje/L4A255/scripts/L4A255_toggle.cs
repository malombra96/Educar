using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A255_toggle : MonoBehaviour
{
    ControlAudio _controlAudio;
    Toggle _toggle;
    [Header("is Right?")] public bool isRight;
    L4A255_groupToggle _L4A255_groupToggle;
    L4A255_managerToggle _managerToggle;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _toggle = GetComponent<Toggle>();
        _L4A255_groupToggle = transform.parent.GetComponent<L4A255_groupToggle>();
        _managerToggle = transform.parent.parent.GetComponent<L4A255_managerToggle>();
        _toggle.onValueChanged.AddListener(delegate { SetStateToggle(); });

        if (_managerToggle._NeedSymbol == L4A255_managerToggle.NeedSymbol.withSymbol)
            transform.GetChild(0).gameObject.SetActive(false);

    }

    void SetStateToggle()
    {
        //print(_toggle+"////"+_toggle.isOn);

        switch (_managerToggle._TypeSelect)
        {
            case L4A255_managerToggle.TypeSelection.onexgroup:

                if (_toggle.isOn)
                {
                    _managerToggle._controlAudio.PlayAudio(0);
                    SetSpriteToggle(GetComponent<Image>(), _toggle.isOn);

                    if (_L4A255_groupToggle._dictionarySelection._toggle.Count > 0)
                    {
                        _L4A255_groupToggle._dictionarySelection._toggle[0] = _toggle;
                        _L4A255_groupToggle._dictionarySelection._state[0] = isRight;
                    }
                    else
                    {
                        _L4A255_groupToggle._dictionarySelection._toggle.Add(_toggle);
                        _L4A255_groupToggle._dictionarySelection._state.Add(isRight);
                    }

                    switch (_managerToggle._typeVal)
                    {

                        case L4A255_managerToggle.TypeValidation.inmediata:

                            _managerToggle.EvaluateGroupsToggleImmediately(_L4A255_groupToggle);

                            break;
                    }


                }
                else
                {
                    SetSpriteToggle(GetComponent<Image>(), _toggle.isOn);
                    if (_L4A255_groupToggle._dictionarySelection._toggle.Contains(_toggle))
                    {
                        _L4A255_groupToggle._dictionarySelection._toggle.RemoveAt(0);
                        _L4A255_groupToggle._dictionarySelection._state.RemoveAt(0);
                    }
                }

                break;

            case L4A255_managerToggle.TypeSelection.variousxgroup:

                if (_toggle.isOn)
                {
                    if (_L4A255_groupToggle._dictionarySelection._toggle.Count > _managerToggle._options - 1)
                    {
                        //print("Change");
                        //print(_L4A255_groupToggle._dictionarySelection._toggle[_managerToggle._options-1]);

                        Toggle tg = _L4A255_groupToggle._dictionarySelection._toggle[_managerToggle._options - 1];

                        SetSpriteToggle(tg.GetComponent<Image>(), tg.isOn);
                        tg.isOn = false;

                    }

                    if (_L4A255_groupToggle._dictionarySelection._toggle.Count < _managerToggle._options)
                    {
                        //print("Add"+_toggle+"//"+_toggle.isOn);
                        _managerToggle._controlAudio.PlayAudio(0);
                        SetSpriteToggle(GetComponent<Image>(), _toggle.isOn);

                        _L4A255_groupToggle._dictionarySelection._toggle.Add(_toggle);
                        _L4A255_groupToggle._dictionarySelection._state.Add(isRight);

                        //print(_managerToggle._options+"/"+_L4A255_groupToggle._dictionarySelection._toggle.Count);
                    }
                }
                else
                {
                    if (_L4A255_groupToggle._dictionarySelection._toggle.Contains(_toggle))
                    {
                        SetSpriteToggle(GetComponent<Image>(), _toggle.isOn);

                        int index = _L4A255_groupToggle._dictionarySelection._toggle.IndexOf(_toggle);

                        _L4A255_groupToggle._dictionarySelection._toggle.RemoveAt(index);
                        _L4A255_groupToggle._dictionarySelection._state.RemoveAt(index);
                    }
                }

                break;
        }

        if (!_managerToggle.reset)
            StartCoroutine(_managerToggle.StateBtnValidar());
    }

    /// <summary>
    /// Asigna sprite default o seleccion de la imagen recibida
    /// </summary>
    /// <param name="i"></param>
    /// <param name="state"></param>
    void SetSpriteToggle(Image image, bool state)
    {
        image.sprite = state ?
            image.GetComponent<BehaviourSprite>()._selection :
            image.GetComponent<BehaviourSprite>()._default;
    }


}
