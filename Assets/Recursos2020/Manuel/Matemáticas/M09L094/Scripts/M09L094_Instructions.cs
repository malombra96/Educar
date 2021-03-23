using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09L094_Instructions : MonoBehaviour
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
        _desktop.transform.GetChild(0).GetComponent<Image>().sprite = _instructionsDesktop[0];
        _mobile.transform.GetChild(0).GetComponent<Image>().sprite = _instructionsMobile[0];

        _desktop.SetActive(!Application.isMobilePlatform);
        _mobile.SetActive(Application.isMobilePlatform);

        n = 0;


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
        if (n + dir <= 7 && n + dir >= 0)
        {
            n += dir;
            print(n);
            _desktop.transform.GetChild(0).GetComponent<Image>().sprite = _instructionsDesktop[n];
            _mobile.transform.GetChild(0).GetComponent<Image>().sprite = _instructionsMobile[n];
            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=7);
        _startButton.SetActive(n==7);

    }
}
