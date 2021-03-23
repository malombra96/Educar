using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10A058_BehaviourBar : MonoBehaviour
{
    ControlAudio _controlAudio;
    ControlPuntaje _controlPuntaje;

    ControlNavegacion _controlNavegacion;

    [Header("Slider")] public List<Slider> _slider;
    [Header("Text Bar")] public List<Text> _text;
    [Header("Lines")] public List<Image> _lines;
    [Header("Static Text")] public List<Text> _static;
    int index;
    
    [Header("Answer Right")] public List<int> _answers;

    [Header("Colors State")] 
    public List<Color32> _default;
    public Color32 _right;
    public Color32 _wrong;

    [Header("Button Validar")] public Button _validar;

    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    }

    void Start()
    {
        foreach (Slider s in _slider)
        {
            s.onValueChanged.AddListener(delegate{SetActive(s);});   
            _default.Add(s.fillRect.GetComponent<Image>().color);
        }
            
            

        DisableLines();

        _validar.onClick.AddListener(Validar);

        EnabledValidar();
        
    }
    void SetActive(Slider s)
    {
        index = _slider.IndexOf(s);

        for (int i = 0; i < _text.Count; i++)
            _text[i].gameObject.SetActive(i==index);
        
        for (int i = 0; i < _lines.Count; i++)
            _lines[i].gameObject.SetActive(i==index);

        SetText(_text[index],s.value);
        EnabledValidar();

    }

    void DisableLines()
    {
        for (int i = 0; i < _text.Count; i++)
            _text[i].gameObject.SetActive(false);
        
        for (int i = 0; i < _lines.Count; i++)
            _lines[i].gameObject.SetActive(false); 
    }


    void SetText(Text T,float value)
    {
        T.text = value.ToString();
        _static[index].text = T.text;
    } 

    void EnabledValidar()
    {
        bool b = true;

        foreach (Slider s in _slider)
        {
            if(s.value == 0)
            {
                b=false;
                break;
            }   
        }

        _validar.interactable = b;
            
    }

    void Validar()
    {
        int right = 0;

        _validar.interactable = false;
        DisableLines();

        for (int i = 0; i < _slider.Count; i++)
        {
            _slider[i].interactable = false;

            if(_slider[i].value == _answers[i])
            {
                _slider[i].fillRect.GetComponent<Image>().color = _right;
                right++;
            }
            else
                _slider[i].fillRect.GetComponent<Image>().color = _wrong;
            
        }

        _controlAudio.PlayAudio(right == _slider.Count? 1 : 2);
        _controlPuntaje.IncreaseScore(right);

        _controlNavegacion.Forward(2);
    }

    public void ResetHistogramaBar()
    {
        for (int i = 0; i < _slider.Count; i++)
        {
            _slider[i].value = 0;

            if(_default.Count > 0)
                _slider[i].fillRect.GetComponent<Image>().color = _default[i];

            _slider[i].interactable = true;
            SetText(_text[i],0);
        }

        DisableLines();
    }



}
