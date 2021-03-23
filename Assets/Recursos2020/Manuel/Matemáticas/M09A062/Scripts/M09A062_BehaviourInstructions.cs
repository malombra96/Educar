using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A062_BehaviourInstructions : MonoBehaviour
{
    ControlAudio _controlAudio;

    [Header("Desktop")] 
    public GameObject _desktop;
    public List<Sprite> _instructionsDesktop;
    [Header("Mobile")] 
    public GameObject _mobile;
    public List<Sprite> _instructionsMobile;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    public GameObject _startButton;

    int n;

    void OnEnable()
    {
        _desktop.GetComponent<Image>().sprite = _instructionsDesktop[0];
        _mobile.GetComponent<Image>().sprite = _instructionsMobile[0];

        _desktop.SetActive(!Application.isMobilePlatform);
        _mobile.SetActive(Application.isMobilePlatform);

        n = 0;

        _left.onClick.AddListener(delegate{ChangeInstruction(-1);});
        _right.onClick.AddListener(delegate{ChangeInstruction(1);});

        SetStateArrows();

    }

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
    }

    void ChangeInstruction(int dir)
    {
        if (n + dir <= 4 && n + dir >= 0)
        {
            n += dir;
            _desktop.GetComponent<Image>().sprite = _instructionsDesktop[n];
            _mobile.GetComponent<Image>().sprite = _instructionsMobile[n];
            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=4);
        _startButton.SetActive(n==4);

    }
}
