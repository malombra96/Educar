using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L116BotonScroll : MonoBehaviour
{
    ControlAudio controlAudio;
    public bool arriba;
    public RectTransform rectTransform;
    [Range(0, 500)] public float rango;
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        GetComponent<Button>().onClick.AddListener(moverScroll);
    }
    void moverScroll()
    {
        controlAudio.PlayAudio(0);
        if (!arriba)
            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y + rango);
        else
            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y - rango);

    }
}
