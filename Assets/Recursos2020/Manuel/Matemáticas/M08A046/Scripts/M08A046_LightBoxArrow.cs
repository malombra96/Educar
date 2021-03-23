using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08A046_LightBoxArrow : MonoBehaviour
{
    ControlAudio _controlAudio;
    public List<Sprite> _instructionsMenu;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    int n;

    void OnEnable()
    {
        n = 0;
        GetComponent<Image>().sprite = _instructionsMenu[0];
        SetStateArrows();
    }

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        GetComponent<Image>().sprite = _instructionsMenu[0];

        _left.onClick.AddListener(delegate{ChangeInstruction(-1);});
        _right.onClick.AddListener(delegate{ChangeInstruction(1);});
    }

    void ChangeInstruction(int dir)
    {
        if (n + dir <= 2 && n + dir >= 0)
        {
            n += dir;
            GetComponent<Image>().sprite = _instructionsMenu[n];
            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=1);

    }
}
