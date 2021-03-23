using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M04A113_BehaviourImage : MonoBehaviour
{
    ControlAudio _controlAudio;
    [Header("Desktop")]  public List<GameObject> _instructionsMenu;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;
    int n;

    void OnEnable()
    {
        n = 0;

        for (int i = 0; i < _instructionsMenu.Count; i++)
            _instructionsMenu[i].SetActive(i==0);

        SetStateArrows();
    }

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _left.onClick.AddListener(delegate{ChangeInstruction(-1);});
        _right.onClick.AddListener(delegate{ChangeInstruction(1);});
    }

    void ChangeInstruction(int dir)
    {
        if (n + dir <= _instructionsMenu.Count-1 && n + dir >= 0)
        {
            n += dir;

            for (int i = 0; i < _instructionsMenu.Count; i++)
                _instructionsMenu[i].SetActive(i == n);

            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=_instructionsMenu.Count-1);

    }
}
