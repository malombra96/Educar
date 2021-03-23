using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M04A113_ManagerBar : MonoBehaviour
{
    [Header("World Associate")] public M04A113_BehaviourWorld _controlWorld;
    [HideInInspector] public ControlAudio _controlAudio;
    ControlPuntaje _controlPuntaje;
    ControlNavegacion _controlNavegacion;

    [Header("List BAR")] public List<M04A113_Bar> _bars;

    [Header("Colors State")] [Header("Default=0/Right=1/Wrong=2")] public List<Color32> _colors;

    [Header("Validar BTN")] public Button _validar;

    int countRight;

    void OnEnable()
    {   
        if(GetComponent<BehaviourLayout>()._isEvaluated)
            transform.GetChild(0).gameObject.SetActive(true);
    } 

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();

        _validar.onClick.AddListener(ValidarBAR);

        InitializeStates();

    }

    void InitializeStates()
    {
        countRight = 0;
        _validar.interactable = true;

        foreach (M04A113_Bar bar in _bars)
        {
            bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color = _colors[0];
            bar.GetComponent<Slider>().value = 0;
            bar.GetComponent<Slider>().interactable = true;
        }

        transform.GetChild(0).gameObject.SetActive(GetComponent<BehaviourLayout>()._isEvaluated);
    }

    void ValidarBAR()
    {
        _validar.interactable = false;

        foreach (M04A113_Bar bar in _bars)
        {
            bar.GetComponent<Slider>().interactable = false;
            bar.GetComponent<Slider>().fillRect.GetComponent<Image>().color =  bar._rightAnswer == bar._currentAnswer? _colors[1]:_colors[2];
            countRight += (bar._rightAnswer == bar._currentAnswer)? 1 : 0;
        }

        /* _controlAudio.PlayAudio(countRight==_bars.Count? 1 : 2);
        _controlPuntaje.IncreaseScore(countRight==_bars.Count? 1 : 0); */

        _controlWorld.DisableCollider();

        if (countRight == _bars.Count)
        {
            _controlAudio.PlayAudio(1);
            _controlWorld.SetDiamondRight();
            _controlPuntaje.IncreaseScore(1);
        }
        else
        {
            _controlAudio.PlayAudio(2);
        }

        _controlWorld._player.GetComponent<M04A113_BehaviourPlayer>().SetBehaviourPlayer(true);
        _controlNavegacion.Forward(2);
    }

/*     public void StateValidarBTN()
    {   
        _validar.interactable = true;

        foreach (M04A113_Bar bar in _bars)
        {
            if(!bar._stateValue)
            {
                _validar.interactable = false;
                break;
            }
            
        }
    } */

    public void ResetBAR()
    {
        InitializeStates();
    }

}
