using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1041ZoomImagen : MonoBehaviour
{
    [Header("Image Content")] public RectTransform _image;
    [Header("Slider")] public Slider _slider;


    void Start() => _slider.onValueChanged.AddListener(delegate { ZoomImage(_slider.value); });

    void ZoomImage(float value) => _image.localScale = new Vector3(value, value, 1);
}
