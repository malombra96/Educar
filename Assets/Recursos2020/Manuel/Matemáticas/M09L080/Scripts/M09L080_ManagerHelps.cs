using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09L080_ManagerHelps : MonoBehaviour
{
    ControlAudio _controlAudio;
    ControlNavegacion _controlNavegacion;

    [Header("Questions Manager")] public List<M09L080_ManagerSeleccionarToggle> _questions;
    [Header("Panel Questions")] public List<Image> _imageQuestions;

    [Header("Image Block Help")] public GameObject _blockPanel;

    [HideInInspector] public List<Toggle> _toggleOptions;

    [Header("Parameters Current Questions")] 

    BehaviourLayout currentLayout;

    public M09L080_ManagerSeleccionarToggle _currentQuestion;
    public M09L080_BehaviourToggle _rightAnswer;
    public string _rightOptionLetter;

    public string _prizeValue;

    [Header("50/50")] 
    [Header("Setup Toggle Help")]
    public Toggle _5050;
    [Header("Delete")] public Toggle _delete;

    [Header("Setup Calling Help")]
    public Toggle _call;
    public GameObject _callingFriend;
    public string _friendName;
    public Text _textName;
    public Text _textOptionLetter;

    [Header("Setup Help View Table")] 
    public Toggle _view;
    public GameObject _tableAngles;
    public int viewTable;


    [Header("Setup Help Public")] 
    public Toggle _helpPublic;
    public GameObject _opinionPubic;
    public Image _barOpinions;
    public List<Sprite> _spriteOpinions;


    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        _call.onValueChanged.AddListener( delegate{SetCallingFriend();});
        _helpPublic.onValueChanged.AddListener( delegate{SetPublicOpinion();});
        _view.onValueChanged.AddListener( delegate{SetViewTable();});

        _5050.onValueChanged.AddListener(delegate{ DeleteWrongOption(_5050,2); });
        _delete.onValueChanged.AddListener(delegate{ DeleteWrongOption(_delete,1); });

        _toggleOptions.Add(_5050);
        _toggleOptions.Add(_delete);
        _toggleOptions.Add(_call);
        _toggleOptions.Add(_view);
        _toggleOptions.Add(_helpPublic);
        
        foreach (var toggle in _toggleOptions)
            toggle.onValueChanged.AddListener( delegate{SetSpriteState();});

        viewTable = 2;
        _prizeValue = "0";
        BlockHelps(false);
        
    }



    /// <summary>
    /// Change sprite select toggle
    /// </summary>
    void SetSpriteState()
    {
        foreach (var t in _toggleOptions)
        {
            t.GetComponent<Image>().sprite = t.isOn
                ? t.GetComponent<BehaviourSprite>()._selection
                : t.GetComponent<BehaviourSprite>()._default;
        }
    }

    public void ClosePopUp()
    {
        _controlAudio.PlayAudio(0);

        if(_view.isOn)
        {
            _tableAngles.SetActive(false);
            _view.isOn = false;
            
            if(viewTable == 0)
            {
                _view.transform.GetChild(0).gameObject.SetActive(false);
                _view.interactable = false;
            }
                
        }

        if(_helpPublic.isOn)
        {
            _opinionPubic.SetActive(false);
            _helpPublic.isOn = false;
            _helpPublic.interactable = false;
        }

        if(_call.isOn)
        {
            _callingFriend.SetActive(false);
            _call.isOn = false;
            _call.interactable = false;
        }
    }

    void LateUpdate()
    {
        if(_controlNavegacion.GetLayoutActual())
            currentLayout = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>(); // Get active element

        if (currentLayout != null)
        {   
            transform.GetChild(0).gameObject.SetActive(currentLayout.GetComponent<M09L080_ManagerSeleccionarToggle>());
            _currentQuestion = currentLayout.GetComponent<M09L080_ManagerSeleccionarToggle>() ? currentLayout.GetComponent<M09L080_ManagerSeleccionarToggle>() : null;
        }

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        if(_currentQuestion)
        {
            _rightAnswer = _currentQuestion._toggleRight;
            _rightOptionLetter = _currentQuestion._toggleRight.name.Split('_')[1];
        }
        else
        {
            _rightAnswer = null;
            _rightOptionLetter = "";
        }
    }

    public void GetNameFriend(string name) => _friendName = name;

    public void SetQuestionActive()
    {
        BlockHelps(false);

        foreach (M09L080_ManagerSeleccionarToggle question in _questions)
        {
            if (question == _currentQuestion && !question._isFinished)
            {
                foreach (Image i in _imageQuestions)
                    if (i.name == _currentQuestion.name)
                        i.sprite = i.GetComponent<BehaviourSprite>()._selection;
            }
        }
    }

    public void SetQuestionFinished()
    {
        BlockHelps(true);

        foreach (M09L080_ManagerSeleccionarToggle question in _questions)
        {
            if (question == _currentQuestion)
            {
                foreach (Image i in _imageQuestions)
                {
                    if(i.name == _currentQuestion.name)
                    {
                        i.sprite = _currentQuestion._wasCorrect?
                            i.GetComponent<BehaviourSprite>()._right : 
                            i.GetComponent<BehaviourSprite>()._wrong;
                    }
                }

                if(!_currentQuestion._wasCorrect)
                    BlockHelps(true);
                else
                    _prizeValue = _currentQuestion._prize;
                    
            }
        }
    }

    #region Functions Help

    void SetCallingFriend() 
    {
        if (_call.isOn)
        {
            _controlAudio.PlayAudio(0);

            _textName.text = _friendName;
            _textOptionLetter.text = _rightOptionLetter;

            _callingFriend.SetActive(true);
        }
    }

    void SetPublicOpinion()
    {
        if (_helpPublic.isOn)
        {
            _controlAudio.PlayAudio(0);

            foreach (Sprite s in _spriteOpinions)
            {
                if (s.name.Split('_')[2] == _rightOptionLetter)
                {
                    _barOpinions.sprite = s;
                    break;
                }

            }

            _opinionPubic.SetActive(true);
        }
    }

    void SetViewTable()
    {
        if (_view.isOn)
        {
            _controlAudio.PlayAudio(0);
            viewTable--;

            if (viewTable >= 0)
            {
                _view.transform.GetChild(0).GetComponent<Image>().sprite = _view.transform.GetChild(0).GetComponent<BehaviourSprite>()._selection;
                _tableAngles.SetActive(true);
            }
            else
                _view.isOn = false;
                
        }
    }

    void DeleteWrongOption(Toggle help, int n)
    {
        if(help.isOn && _currentQuestion)
        {
            int x = n;
            M09L080_BehaviourToggleGroup _group = _currentQuestion._groupToggle[0];

            for (int i = 0; i < _group.transform.childCount; i++)
            {
                M09L080_BehaviourToggle option = _group.transform.GetChild(i).GetComponent<M09L080_BehaviourToggle>();

                if(!option.isRight && option.gameObject.activeSelf)
                {
                    if(x > 0)
                    {
                        option.gameObject.SetActive(false);
                        x--;
                    }
                    else
                        break;
                    
                }

            } 
        }

        if(help == _5050)
        {
            _5050.isOn = false;
            _5050.interactable = false;
        }
        else if(help == _delete)
        {
            _delete.isOn = false;
            _delete.interactable = false;
        }
    }

    public void BlockHelps(bool state) => _blockPanel.SetActive(state);

    #endregion

    public void ResetHelps()
    {
        foreach (Image i in _imageQuestions)
            i.sprite = i.GetComponent<BehaviourSprite>()._default;

        foreach (var t in _toggleOptions)
        {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
            t.GetComponent<Toggle>().isOn = false;
            t.interactable = true;
        }

        _tableAngles.SetActive(false);
        _view.transform.GetChild(0).GetComponent<Image>().sprite = _view.transform.GetChild(0).GetComponent<BehaviourSprite>()._default;
        _view.transform.GetChild(0).gameObject.SetActive(true);

        _opinionPubic.SetActive(false);
        _callingFriend.SetActive(false);

        viewTable = 2;
        _prizeValue = "0";
        BlockHelps(false);
    }

}
