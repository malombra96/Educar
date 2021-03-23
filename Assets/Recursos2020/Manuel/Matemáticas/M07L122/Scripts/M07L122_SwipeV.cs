using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M07L122_SwipeV : MonoBehaviour
{
    ControlAudio _controlAudio;
    ScrollRect _scrollRect;
    RectTransform _content;
    [HideInInspector] public List<Image> _images;

    [HideInInspector] public List<Vector2> _posImages;
    [Header("Buttons")] public List<Button> _buttons;
    [HideInInspector] public float maxContent;
    [HideInInspector] public float delta;
    [Header("Index Image Active")] public int indexActive;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _scrollRect = transform.GetChild(0).GetComponent<ScrollRect>();
        _scrollRect.onValueChanged.AddListener(GetIndexActive);

        _content = _scrollRect.content.GetComponent<RectTransform>();

        //for (int i = 0; i < _scrollRect.content.childCount; i++)
        //    _images.Add(_scrollRect.content.GetChild(i).GetComponent<Image>());

        for (int i = 0; i < _scrollRect.content.childCount; i++)
            _images.Add(_scrollRect.content.GetChild(i).GetComponent<Image>());

        //_images.Reverse();

        foreach (var image in _images)
            _posImages.Add(image.GetComponent<RectTransform>().anchoredPosition);

        _posImages.Reverse();

        foreach (var button in _buttons)
            button.onClick.AddListener(delegate{ ToIndex(_buttons.IndexOf(button));});

        maxContent =  _content.sizeDelta.y - _content.anchoredPosition.y;
        delta = _content.GetComponent<GridLayoutGroup>().spacing.y + _images[0].GetComponent<RectTransform>().sizeDelta.y;
        indexActive = _images.Count-1;

        SetImageActive();

    }

    void GetIndexActive(Vector2 actual)
    {
        float posContent = actual.y*maxContent; //_scrollRect.content.GetComponent<RectTransform>().sizeDelta.x
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
        _scrollRect.content.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x,-(_posImages[n].y));

    }
}
