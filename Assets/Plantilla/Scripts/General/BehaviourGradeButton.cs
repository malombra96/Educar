using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourGradeButton : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    SpriteState _spriteState;
    BehaviourSprite _behaviourSprite;

    Image _image;

    [Header("Sprites Primary")]

    public Sprite _defaultPrimary;
    public Sprite _selectPrimary;
    public Sprite _disabledPrimary;

    [Header("Sprites Secundary")]

    public Sprite _defaultSecundary;
    public Sprite _selectSecundary;
    public Sprite _disabledSecundary;


    void Start()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _image = GetComponent<Image>();
        _spriteState = GetComponent<Button>().spriteState;

        SetSpriteState();

        if (GetComponent<BehaviourSprite>())
        {
            _behaviourSprite = GetComponent<BehaviourSprite>();
            SetBehaviourSprite();
        }


        GetComponent<Button>().spriteState = _spriteState;

    }

    void SetSpriteState()
    {
        switch (_controlNavegacion._grade)
        {
            case ControlNavegacion.Grade.Primary:

                _image.sprite = _defaultPrimary;

                _spriteState.highlightedSprite = _selectPrimary;
                _spriteState.pressedSprite = _selectPrimary;
                _spriteState.disabledSprite = _disabledPrimary;

                break;

            case ControlNavegacion.Grade.Secundary:

                _image.sprite = _defaultSecundary;

                _spriteState.highlightedSprite = _selectSecundary;
                _spriteState.pressedSprite = _selectSecundary;
                _spriteState.disabledSprite = _disabledSecundary;

                break;
        }
    }

    void SetBehaviourSprite()
    {
        switch (_controlNavegacion._grade)
        {
            case ControlNavegacion.Grade.Primary:

                _behaviourSprite._default = _defaultPrimary;
                _behaviourSprite._selection = _selectPrimary;
                _behaviourSprite._disabled = _disabledPrimary;

                break;

            case ControlNavegacion.Grade.Secundary:

                _behaviourSprite._default = _defaultSecundary;
                _behaviourSprite._selection = _selectSecundary;
                _behaviourSprite._disabled = _disabledSecundary;

                break;
        }
    }

}
