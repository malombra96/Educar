using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M5A115_toggle : MonoBehaviour
{
    ControlAudio _controlAudio;
    Toggle _toggle;
    [Header("is Right?")] public bool isRight;
    M5A115_grupoSeleccionar _M5A115_grupoSeleccionar;
    M5A115_managerSeleccionar _managerToggle;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _toggle = GetComponent<Toggle>();
        _M5A115_grupoSeleccionar = transform.parent.GetComponent<M5A115_grupoSeleccionar>();
        _managerToggle = transform.parent.parent.GetComponent<M5A115_managerSeleccionar>();
        _toggle.onValueChanged.AddListener(delegate { SetStateToggle(); });

        if (_managerToggle._NeedSymbol == M5A115_managerSeleccionar.NeedSymbol.withSymbol)
            transform.GetChild(0).gameObject.SetActive(false);

    }

    void SetStateToggle()
    {
        //print(_toggle+"////"+_toggle.isOn);

        switch (_managerToggle._TypeSelect)
        {
            case M5A115_managerSeleccionar.TypeSelection.onexgroup:

                if (_toggle.isOn)
                {
                    _managerToggle._controlAudio.PlayAudio(0);
                    SetSpriteToggle(GetComponent<Image>(), _toggle.isOn);

                    if (_M5A115_grupoSeleccionar._dictionarySelection._toggle.Count > 0)
                    {
                        _M5A115_grupoSeleccionar._dictionarySelection._toggle[0] = _toggle;
                        _M5A115_grupoSeleccionar._dictionarySelection._state[0] = isRight;
                    }
                    else
                    {
                        _M5A115_grupoSeleccionar._dictionarySelection._toggle.Add(_toggle);
                        _M5A115_grupoSeleccionar._dictionarySelection._state.Add(isRight);
                    }

                    switch (_managerToggle._typeVal)
                    {

                        case M5A115_managerSeleccionar.TypeValidation.inmediata:

                            _managerToggle.EvaluateGroupsToggleImmediately(_M5A115_grupoSeleccionar);

                            break;
                    }


                }
                else
                {
                    SetSpriteToggle(GetComponent<Image>(), _toggle.isOn);
                    if (_M5A115_grupoSeleccionar._dictionarySelection._toggle.Contains(_toggle))
                    {
                        _M5A115_grupoSeleccionar._dictionarySelection._toggle.RemoveAt(0);
                        _M5A115_grupoSeleccionar._dictionarySelection._state.RemoveAt(0);
                    }
                }

                break;

            case M5A115_managerSeleccionar.TypeSelection.variousxgroup:

                if (_toggle.isOn)
                {
                    if (_M5A115_grupoSeleccionar._dictionarySelection._toggle.Count > _managerToggle._options - 1)
                    {
                        //print("Change");
                        //print(_M5A115_grupoSeleccionar._dictionarySelection._toggle[_managerToggle._options-1]);

                        Toggle tg = _M5A115_grupoSeleccionar._dictionarySelection._toggle[_managerToggle._options - 1];

                        SetSpriteToggle(tg.GetComponent<Image>(), tg.isOn);
                        tg.isOn = false;

                    }

                    if (_M5A115_grupoSeleccionar._dictionarySelection._toggle.Count < _managerToggle._options)
                    {
                        //print("Add"+_toggle+"//"+_toggle.isOn);
                        _managerToggle._controlAudio.PlayAudio(0);
                        SetSpriteToggle(GetComponent<Image>(), _toggle.isOn);

                        _M5A115_grupoSeleccionar._dictionarySelection._toggle.Add(_toggle);
                        _M5A115_grupoSeleccionar._dictionarySelection._state.Add(isRight);

                        //print(_managerToggle._options+"/"+_M5A115_grupoSeleccionar._dictionarySelection._toggle.Count);
                    }
                }
                else
                {
                    if (_M5A115_grupoSeleccionar._dictionarySelection._toggle.Contains(_toggle))
                    {
                        SetSpriteToggle(GetComponent<Image>(), _toggle.isOn);

                        int index = _M5A115_grupoSeleccionar._dictionarySelection._toggle.IndexOf(_toggle);

                        _M5A115_grupoSeleccionar._dictionarySelection._toggle.RemoveAt(index);
                        _M5A115_grupoSeleccionar._dictionarySelection._state.RemoveAt(index);
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
