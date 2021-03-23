using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A075_BehaviourInstructions : MonoBehaviour
{
    ControlAudio _controlAudio;

    [Header("Desktop")] 
    public GameObject _menu;
    public List<Sprite> _instructionsMenu;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    public GameObject _startButton;

    int n;

    void OnEnable()
    {
        n = 0;
        _menu.GetComponent<Image>().sprite = _instructionsMenu[0];
        SetStateArrows();
    }

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _menu.GetComponent<Image>().sprite = _instructionsMenu[0];

        _left.onClick.AddListener(delegate{ChangeInstruction(-1);});
        _right.onClick.AddListener(delegate{ChangeInstruction(1);});
    }

    void ChangeInstruction(int dir)
    {
        if (n + dir <= 2 && n + dir >= 0)
        {
            n += dir;
            _menu.GetComponent<Image>().sprite = _instructionsMenu[n];
            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=2);
        _startButton.SetActive(n==2);

    }
}
