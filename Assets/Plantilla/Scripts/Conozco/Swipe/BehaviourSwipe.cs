using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourSwipe : MonoBehaviour
{   
    ControlAudio _controlAudio;
    ScrollRect _scrollRect;
    RectTransform _content;
    [HideInInspector] public List<Image> _images;

    [HideInInspector] public List<Vector2> _posImages;
    [Header("Buttons")] public List<Button> _buttons;
    [HideInInspector] float maxContent;
    [HideInInspector] public float delta;
    [Header("Index Image Active")] int indexActive;

    

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _scrollRect = transform.GetChild(0).GetComponent<ScrollRect>();
        _scrollRect.onValueChanged.AddListener(GetIndexActive);

        _content = _scrollRect.content.GetComponent<RectTransform>();

        for (int i = 0; i < _scrollRect.content.childCount; i++)
            _images.Add(_scrollRect.content.GetChild(i).GetComponent<Image>());

        foreach (var image in _images)
            _posImages.Add(image.GetComponent<RectTransform>().anchoredPosition);

        foreach (var button in _buttons)
            button.onClick.AddListener(delegate{ ToIndex(_buttons.IndexOf(button));});

        maxContent =  _content.sizeDelta.x + _content.anchoredPosition.x;
        delta = _content.GetComponent<GridLayoutGroup>().spacing.x + _images[0].GetComponent<RectTransform>().sizeDelta.x;
        indexActive = 0;

        SetImageActive();

    }

    void GetIndexActive(Vector2 actual)
    {
        float posContent = actual.x*maxContent; //_scrollRect.content.GetComponent<RectTransform>().sizeDelta.x
        indexActive = Mathf.RoundToInt(posContent/delta);
        SetImageActive();
    }

    void SetImageActive()
    {
        for (int i = 0; i < _buttons.Count; i++)
            if(i == indexActive)
                _buttons[i].GetComponent<Image>().sprite = _buttons[i].GetComponent<BehaviourSprite>()._default;
            else
                _buttons[i].GetComponent<Image>().sprite = _buttons[i].GetComponent<BehaviourSprite>()._selection;

    }

    void ToIndex(int n)
    {
        _controlAudio.PlayAudio(0);
        Vector2 pos = _scrollRect.content.GetComponent<RectTransform>().anchoredPosition;
        _scrollRect.content.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(_posImages[n].x), pos.y);

    }

   
}
