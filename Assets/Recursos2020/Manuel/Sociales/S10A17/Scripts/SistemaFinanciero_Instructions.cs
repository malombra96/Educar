using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaFinanciero_Instructions : MonoBehaviour
{
    
    [Header("Image Info")] public Image _info;
    [Header("Sprites Platform")] public  List<Sprite> _states;

    [Header("PopUp Instructions")] public Image _popUp;

    [Header("Sprites Platform")] public  List<Sprite> _spritesPopup;

    void Start()
    {
        _info.sprite = Application.isMobilePlatform? _states[1] : _states[0];
        _popUp.sprite = Application.isMobilePlatform? _spritesPopup[1] : _spritesPopup[0];
    }
}
