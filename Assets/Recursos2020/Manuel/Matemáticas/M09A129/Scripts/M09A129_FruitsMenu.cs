using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A129_FruitsMenu : MonoBehaviour
{
    ControlAudio _controlAudio;
    Toggle _toggle;
    RectTransform _menu;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _toggle = transform.GetChild(0).GetComponent<Toggle>();
        _menu = GetComponent<RectTransform>();

        _menu.anchoredPosition = new Vector3(0,-434,0);
        _toggle.onValueChanged.AddListener( delegate{SetMenu();});
    }

    void SetMenu()
    {
        _controlAudio.PlayAudio(0);

        _menu.anchoredPosition = _toggle.isOn? new Vector3(0,-334,0) : new Vector3(0,-434,0);

        _toggle.GetComponent<Image>().sprite = _toggle.isOn? 
            _toggle.GetComponent<BehaviourSprite>()._selection :
            _toggle.GetComponent<BehaviourSprite>()._default;
    }
}
