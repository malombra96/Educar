using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M07L120_MoveLightbox : MonoBehaviour
{
    [Header("Arrows")] 
    public Button _left;
    public Button _right;
    RectTransform _rect;


    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _left.onClick.AddListener(delegate{SetPosition(-1366);});
        _right.onClick.AddListener(delegate{SetPosition(0);});

    }

    void SetPosition(float mov) => _rect.anchoredPosition = new Vector2(mov,0);
    

}
