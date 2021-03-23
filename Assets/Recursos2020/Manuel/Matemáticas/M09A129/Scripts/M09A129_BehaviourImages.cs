using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class M09A129_BehaviourImages : MonoBehaviour
{
    ControlAudio _controlAudio;
    Image  _image;

    [Header("Sprite Background")] public List<Sprite> _bgSprite;

    public List<GameObject> _steps;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    [Header("Sprites Arrow")] 
    public List<Sprite> _off;
    public List<Sprite> _over;
    public List<Sprite> _on;


    public int n;

    void OnEnable()
    {
        _image = GetComponent<Image>();

        n = 0;
        SetStateArrows();
        SetStateBG();

        for (int i = 0; i < _steps.Count ; i++)
            _steps[i].SetActive(i == n);

    }

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _left.onClick.AddListener(delegate{ChangeInstruction(-1);});
        _right.onClick.AddListener(delegate{ChangeInstruction(1);});
    }

    void ChangeInstruction(int dir)
    {
        if (n + dir <= (_steps.Count-1) && n + dir >= 0)
        {
            n += dir;

            for (int i = 0; i < _steps.Count ; i++)
                _steps[i].SetActive(i == n);

            _controlAudio.PlayAudio(0);
            SetStateArrows();
            SetStateBG();
        }
    }

    void SetStateArrows()
    {
        _left.GetComponent<Image>().sprite = n>=0 && n<=3? _off[0]: n>3 && n<=9? _off[1] : _off[2];
        _right.GetComponent<Image>().sprite = n>=0 && n<=3? _off[0]: n>3 && n<=9? _off[1] : _off[2];

        SpriteState ls = _left.spriteState;
        SpriteState rs = _left.spriteState;

        ls.highlightedSprite = n>=0 && n<=3? _over[0]: n>3 && n<=9? _over[1] : _over[2];
        rs.highlightedSprite = n>=0 && n<=3? _over[0]: n>3 && n<=9? _over[1] : _over[2];

        ls.pressedSprite = n>=0 && n<=3? _on[0]: n>3 && n<=9? _on[1] : _on[2];
        rs.pressedSprite = n>=0 && n<=3? _on[0]: n>3 && n<=9? _on[1] : _on[2];

        _left.spriteState = ls;
        _right.spriteState = rs;

        _left.interactable =  (n!=0);
        _right.interactable = (n!=(_steps.Count-1));

    }
    
    void SetStateBG()
    {
        _image.sprite = n>=0 && n<=3? _bgSprite[0]: n>3 && n<=9? _bgSprite[1] : _bgSprite[2];
    }
}
