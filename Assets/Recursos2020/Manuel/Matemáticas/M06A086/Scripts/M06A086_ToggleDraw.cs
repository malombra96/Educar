using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M06A086_ToggleDraw : MonoBehaviour
{
    M06A086_ManagerDraw _managerDraw;
    Toggle _toggle;
    Image _image;
    [HideInInspector][Header("Coordenada")] public Vector3 _point;

    [Header("Correct Match")] public List<Toggle> _match;

    [Header("Start Line")] public bool _startLine;
    void Awake()
    {
        _managerDraw = FindObjectOfType<M06A086_ManagerDraw>();
        _managerDraw._ListToggles.Add(GetComponent<Toggle>());   
        _image = GetComponent<Image>();

        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(delegate { SetToggle(); });

        _point = GetComponent<RectTransform>().position;
        _point.z-=.1f; 
    }
    void SetToggle()
    {
        
        if (_toggle.isOn)
        {
            if(_startLine || _managerDraw._A != null)
            {   
                _managerDraw._controlAudio.PlayAudio(0);
                _image.sprite = GetComponent<BehaviourSprite>()._selection;
                
            }

            _managerDraw.SetLine(_toggle);
                
        }
        else
        {
            _image.sprite = GetComponent<BehaviourSprite>()._default;

            if(_managerDraw.x)
            {
                Destroy(_managerDraw.x);
                _managerDraw.ClearTemp();
            }
                
                
        }
        
    }
}
