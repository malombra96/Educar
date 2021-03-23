using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourZoomImage : MonoBehaviour
{
    [Header("Image Content")] public RectTransform _image;
    [Header("Slider")] public Slider _slider;


    void Start() => _slider.onValueChanged.AddListener(delegate {ZoomImage(_slider.value);});

    void ZoomImage(float value) => _image.localScale = new Vector2(value,value);
}
