using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_BehaviourScrollView : MonoBehaviour
{
    ScrollRect _scrollRect;

    void Start() => _scrollRect = GetComponent<ScrollRect>();
    void Update()
    {
        for (int i = 0; i < transform.GetChild(0).transform.childCount; i++)
            if(transform.GetChild(0).transform.GetChild(i).gameObject.activeSelf)
                _scrollRect.content = transform.GetChild(0).transform.GetChild(i).GetComponent<RectTransform>();
    }
}
