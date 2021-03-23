using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M06A089_Conozco : MonoBehaviour
{
    ControlAudio _controlAudio;

    public List<GameObject> _polygons;    

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    int n;

    void Start()
    {
        n = 0;
        _controlAudio = FindObjectOfType<ControlAudio>();
        _left.onClick.AddListener(delegate{ChangeInstruction(-1);});
        _right.onClick.AddListener(delegate{ChangeInstruction(1);});

        SetStateArrows();
    }

    void ChangeInstruction(int dir)
    {
        if (n + dir <= 2 && n + dir >= 0)
        {
            _controlAudio.PlayAudio(0);
            n+=dir;
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        for (int i = 0; i < _polygons.Count; i++)
                _polygons[i].SetActive(i==n);

        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=2);
    }
}
