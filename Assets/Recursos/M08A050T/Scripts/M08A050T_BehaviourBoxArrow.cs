using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08A050T_BehaviourBoxArrow : MonoBehaviour
{
    ControlAudio _controlAudio;
    public RectTransform _box;
    public Button _left,_right;
    int[] _positions = new int[3]{1335,0,-1335}; 
    int n;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _left.onClick.AddListener(delegate{MoveBox(-1);});
        _right.onClick.AddListener(delegate{MoveBox(1);});

        n = 0;
        _box.anchoredPosition = new Vector2(_positions[n],_box.anchoredPosition.y);
        SetStateArrows();
    }

    void MoveBox(int dir)
    {
        if (n + dir <= 2 && n + dir >= 0)
        {
            n += dir;
            _box.anchoredPosition = new Vector2(_positions[n], _box.anchoredPosition.y);
            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=2);

    }
}
