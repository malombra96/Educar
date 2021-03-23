using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L6A277_swipe : MonoBehaviour
{
    ControlAudio _controlAudio;
    ScrollRect _scrollRect;
    RectTransform _content;
     public List<Image> _images;

    public List<Vector2> _posImages;
    [Header("Buttons")] public List<Button> _buttons;
    public float maxContent;
    public float delta;
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
            button.onClick.AddListener(delegate { ToIndex(/*_buttons.IndexOf(button)*/button.name); });

        maxContent = _content.sizeDelta.y - _content.anchoredPosition.y;
        delta = _content.GetComponent<GridLayoutGroup>().spacing.y + _images[0].GetComponent<RectTransform>().sizeDelta.y;
        indexActive = _images.Count - 1;

        SetImageActive(indexActive);

    }

    void GetIndexActive(Vector2 actual)
    {
        float posContent = actual.y * maxContent; //_scrollRect.content.GetComponent<RectTransform>().sizeDelta.x
        //print(posContent);
        //print(posContent / delta);
       // print(indexActive);
        indexActive = Mathf.RoundToInt(posContent / delta);
        //if (indexActive == 4)
        //{
        //    indexActive = 2;
        //}
        //else if (indexActive == 3)
        //{
        //    indexActive = 2;
        //}
        SetImageActive(indexActive);
    }

    void SetImageActive(int i)
    {
        if (i == 4 || i == 3 || i == 2) {
            _buttons[2].GetComponent<Image>().sprite = _buttons[2].GetComponent<BehaviourSprite>()._default;
            _buttons[1].GetComponent<Image>().sprite = _buttons[1].GetComponent<BehaviourSprite>()._selection;
            _buttons[0].GetComponent<Image>().sprite = _buttons[0].GetComponent<BehaviourSprite>()._selection;
        }else
        if (i== 1)
        {
            _buttons[1].GetComponent<Image>().sprite = _buttons[1].GetComponent<BehaviourSprite>()._default;
            _buttons[0].GetComponent<Image>().sprite = _buttons[0].GetComponent<BehaviourSprite>()._selection;
            _buttons[2].GetComponent<Image>().sprite = _buttons[2].GetComponent<BehaviourSprite>()._selection;
        }else
        if (i== 0)
        {
            _buttons[0].GetComponent<Image>().sprite = _buttons[0].GetComponent<BehaviourSprite>()._default;
            _buttons[1].GetComponent<Image>().sprite = _buttons[1].GetComponent<BehaviourSprite>()._selection;
            _buttons[2].GetComponent<Image>().sprite = _buttons[2].GetComponent<BehaviourSprite>()._selection;
        }
    }

    void ToIndex(string nombre)
    {
        //print(nombre);
        _controlAudio.PlayAudio(0);
        if (nombre == " (3)")
        {
            _scrollRect.content.GetComponent<RectTransform>().anchoredPosition = new Vector2(_scrollRect.content.GetComponent<RectTransform>().anchoredPosition.x, -563.4f);
            indexActive = 2;
            SetImageActive(indexActive);
        }
        else if (nombre == " (1)")
        {
            _scrollRect.content.GetComponent<RectTransform>().anchoredPosition = new Vector2(_scrollRect.content.GetComponent<RectTransform>().anchoredPosition.x, 563.4f);
            indexActive = 0;
            SetImageActive(indexActive);
        }
        else if (nombre == " (2)") {
            _scrollRect.content.GetComponent<RectTransform>().anchoredPosition = new Vector2(_scrollRect.content.GetComponent<RectTransform>().anchoredPosition.x, 112f);
            indexActive = 1;
            SetImageActive(indexActive);
        }
    }
}

