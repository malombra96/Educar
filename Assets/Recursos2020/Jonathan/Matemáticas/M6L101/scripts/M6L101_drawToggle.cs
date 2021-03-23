using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L101_drawToggle : MonoBehaviour
{
    public M6L101_frog frog;
    M6L101_managerAplico2 _managerDraw;
    Toggle _toggle;
    Image _image;
     [Header("Coordenada")] public Vector3 _point;

    [Header("Correct Match")] public List<Toggle> _match;
    public bool x;
    void Awake()
    {
        _managerDraw = FindObjectOfType<M6L101_managerAplico2>();
        _managerDraw._ListToggles.Add(GetComponent<Toggle>());
        _image = GetComponent<Image>();

        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(delegate { SetToggle(); });

        if (x) {
            _point = GetComponent<RectTransform>().position;
            _point.z -= .1f;
        }
        //
    }
    void SetToggle()
    {

        if (_toggle.isOn)
        {
            _managerDraw._controlAudio.PlayAudio(0);
            _image.sprite = GetComponent<BehaviourSprite>()._selection;
            _managerDraw.SetLine(_toggle);
        }
        else
        {
            _image.sprite = GetComponent<BehaviourSprite>()._default;

            if (_managerDraw.x)
            {
                Destroy(_managerDraw.x);
                _managerDraw.ClearTemp();
            }


        }

    }
}
