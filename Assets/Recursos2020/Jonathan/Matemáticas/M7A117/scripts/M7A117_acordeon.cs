using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M7A117_acordeon : MonoBehaviour
{
    ControlAudio _controlAudio;
    [Tooltip("Arrastre los toggles, recuerde asociar el toggle group")] [Header("Toggles")] public Toggle[] _toggles;

    [Header("Images")] public Image[] _images;
    [Header("Widht-X")] public float[] _widhts;
    [Header("Widht-Y")] public float[] _height;

    [Header("Lista de posiciones default")] public Vector2[] positions;

    public enum DisplacementAxis
    {
        X,
        Y
    }
    [Header("Eje de desplazamiento")] public DisplacementAxis _Asix;

    [Tooltip("Distancia entre cada imagen y el siguiente boton")] [Header("Delta")] public float _delta;
    [Tooltip("Seleccione si necesita que inicie activa la primer imagen")] [Header("Start Active")] public bool startActive;

    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

         positions = new Vector2[_toggles.Length];
        _images = new Image[_toggles.Length];
        _widhts = new float[_toggles.Length];
        _height = new float[_toggles.Length];

        for (int i = 0; i < _toggles.Length; i++)
        {
            positions[i] = _toggles[i].GetComponent<RectTransform>().anchoredPosition;
            _images[i] = _toggles[i].transform.GetChild(0).GetComponent<Image>();
            _widhts[i] = _toggles[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            _height[i] = _toggles[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        }

        if (startActive)
        {
            _toggles[0].isOn = true;
            SetToggleActive(0);
            SetSpriteState();
        }



        foreach (var toggle in _toggles)
            toggle.onValueChanged.AddListener(delegate { SetImageActive(toggle); });
    }

    void SetImageActive(Toggle t)
    {
        if (t.isOn)
        {
            _controlAudio.PlayAudio(0);

            for (int i = 0; i < _toggles.Length; i++)
                if (_toggles[i].isOn)
                    SetToggleActive(i);

            SetSpriteState();
        }

    }

    void SetToggleActive(int n)
    {
        if (_Asix == DisplacementAxis.X)
        {

            if (n == 0) {
                _toggles[0].GetComponent<RectTransform>().anchoredPosition = new Vector2((positions[0].x - _widhts[0] + _delta), positions[0].y);
                _toggles[1].GetComponent<RectTransform>().anchoredPosition = positions[1];
                _toggles[2].GetComponent<RectTransform>().anchoredPosition = positions[2];
            }

            if (n == 1)
            {
                _toggles[0].GetComponent<RectTransform>().anchoredPosition = new Vector2((positions[0].x - _widhts[0] + _delta), positions[0].y);
                _toggles[1].GetComponent<RectTransform>().anchoredPosition = new Vector2((positions[1].x - _widhts[1] + _delta), positions[1].y);
                _toggles[2].GetComponent<RectTransform>().anchoredPosition = positions[2];
            }

            if (n == 2) {
                _toggles[0].GetComponent<RectTransform>().anchoredPosition = new Vector2((positions[0].x - _widhts[0] + _delta), positions[0].y);
                _toggles[1].GetComponent<RectTransform>().anchoredPosition = new Vector2((positions[1].x - _widhts[1] + _delta), positions[1].y);
                _toggles[2].GetComponent<RectTransform>().anchoredPosition = new Vector2((positions[2].x - _widhts[2] + _delta), positions[2].y);
            }


            //for (int i = 0; i < _toggles.Length; i++)
            //    if (i <= n)
            //        _toggles[i].GetComponent<RectTransform>().anchoredPosition = new Vector2((positions[n + i].x + _widhts[n] + _delta), positions[n + i].y);


            //for (int i = 1; i < _toggles.Length; i++)
            //{
            //    if ((n + i) < _toggles.Length)
            //        _toggles[n + i].GetComponent<RectTransform>().anchoredPosition = positions[i];
            //}
        }
        else
        {
            for (int i = 0; i < _toggles.Length; i++)
                if (i <= n)
                    _toggles[i].GetComponent<RectTransform>().anchoredPosition = positions[i];

            for (int i = 1; i < _toggles.Length; i++)
            {
                if ((n + i) < _toggles.Length)
                    _toggles[n + i].GetComponent<RectTransform>().anchoredPosition = new Vector2(positions[n + i].x, (positions[n + i].y - _height[n] - _delta));

            }
        }


        for (int i = 0; i < _images.Length; i++)
            _images[i].gameObject.SetActive(i == n);


    }

    /// <summary>
    /// Change sprite select toggle
    /// </summary>
    void SetSpriteState()
    {
        foreach (var t in _toggles)
        {
            t.GetComponent<Image>().sprite = t.isOn
                ? t.GetComponent<BehaviourSprite>()._selection
                : t.GetComponent<BehaviourSprite>()._default;
        }
    }
}

