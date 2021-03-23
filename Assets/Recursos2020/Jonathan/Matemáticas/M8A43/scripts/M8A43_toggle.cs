using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class M8A43_toggle : MonoBehaviour
{
    public bool istrue;
    public M8A43_manger_2 manager;
    Toggle _toggle;
    Image _image;
    [Header("Coordenada")] public Vector3 _point;

    [Header("Correct Match")] public List<Toggle> _match;
    
    void Awake()
    {
        _image = GetComponent<Image>();

        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(delegate { SetToggle(); });

        _point = GetComponent<RectTransform>().position;
        _point.z -= .1f;
    }

    private void Start()
    {
        manager._ListToggles.Add(_toggle);
    }
    void SetToggle()
    {

        if (_toggle.isOn)
        {
            manager._controlAudio.PlayAudio(0);
            _image.sprite = GetComponent<BehaviourSprite>()._selection;
            _image.color = new Color32(255,255,255,255);
            //_managerDraw.SetLine(_toggle);
            manager.AddToggle(_toggle.gameObject);

        }
        else
        {
            _image.sprite = GetComponent<BehaviourSprite>()._default;
            _image.color = new Color32(255, 255, 255, 255);
            if (manager.firstToggle == gameObject) {
                manager.firstToggle = null;
                int index = manager.checkPoints.IndexOf(gameObject.GetComponent<RectTransform>());
                manager.checkPoints.RemoveAt(index);
            }
            if (manager.secondToggle== gameObject)
            {
                manager.secondToggle = null;
                int index = manager.checkPoints.IndexOf(gameObject.GetComponent<RectTransform>());
                manager.checkPoints.RemoveAt(index);
            }
            if (manager.thirdToggle == gameObject)
            {
                manager.thirdToggle = null;
                int index = manager.checkPoints.IndexOf(gameObject.GetComponent<RectTransform>());
                manager.checkPoints.RemoveAt(index);
            }
        }

    }
}
