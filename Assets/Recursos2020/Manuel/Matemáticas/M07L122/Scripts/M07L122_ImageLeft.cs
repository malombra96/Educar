using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M07L122_ImageLeft : MonoBehaviour
{
    [Header("Sprite")] public Sprite[] _sprites;
    Image _image;
    M07L122_SwipeV _SwipeV;

    // Start is called before the first frame update
    void Start()
    {
        _SwipeV = FindObjectOfType<M07L122_SwipeV>();
        _image = GetComponent<Image>();
    }
    void Update()
    {
        _image.sprite = _sprites[_SwipeV.indexActive];
    }
}
