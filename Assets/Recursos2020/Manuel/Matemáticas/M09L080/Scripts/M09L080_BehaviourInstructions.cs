using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09L080_BehaviourInstructions : MonoBehaviour
{
    ControlAudio _controlAudio;

    [Header("Desktop")] 
    public GameObject _dialogo;
    public List<Sprite> _instructionsMenu;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    public GameObject _inputName;
    
    public GameObject _continueButton;

    int n;

    void OnEnable()
    {
        n = 0;
        _dialogo.GetComponent<Image>().sprite = _instructionsMenu[0];
        SetStateArrows();
    }

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _dialogo.GetComponent<Image>().sprite = _instructionsMenu[0];

        _left.onClick.AddListener(delegate{ChangeInstruction(-1);});
        _right.onClick.AddListener(delegate{ChangeInstruction(1);});
    }

    void ChangeInstruction(int dir)
    {
        if (n + dir <= 3 && n + dir >= 0)
        {
            n += dir;
            _dialogo.GetComponent<Image>().sprite = _instructionsMenu[n];
            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=3);
        _inputName.SetActive(n==2);
        _continueButton.SetActive(n==3);

        if(n==2)
            CheckInput();

    }

    public void CheckInput() => _right.gameObject.SetActive(!string.IsNullOrEmpty(_inputName.GetComponent<InputField>().text));
}
