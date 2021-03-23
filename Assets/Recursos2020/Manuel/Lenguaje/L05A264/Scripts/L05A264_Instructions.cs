using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L05A264_Instructions : MonoBehaviour
{
    ControlAudio _controlAudio;
    
    [Header("Image Instruction")] public Image _image;
    public List<Sprite> _spritesDesktop;
    public List<Sprite> _spritesMobile;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    int n;

    void OnEnable()
    {
        for (int i = 0; i < _spritesDesktop.Count; i++)
            _image.sprite = Application.isMobilePlatform? _spritesMobile[0] : _spritesDesktop[0];

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
        if (n + dir <= (_spritesDesktop.Count-1) && n + dir >= 0)
        {
            n += dir;
            
            _image.sprite =  Application.isMobilePlatform? _spritesMobile[n] : _spritesDesktop[n];

            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=(_spritesDesktop.Count-1));
    }
}
