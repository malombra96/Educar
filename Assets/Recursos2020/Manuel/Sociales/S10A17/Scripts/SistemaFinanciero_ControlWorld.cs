using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SistemaFinanciero_ControlWorld : MonoBehaviour
{   
    [Header("Player")] public SistemaFinanciero_Player _BehaviourPlayer;
    ControlNavegacion _controlNavegacion;
    [Header("Layouts Aplico")] public List<BehaviourLayout> _layouts;

    [Header("Coins Objects")] public List<GameObject> _coins;
    [HideInInspector] public BehaviourLayout _current;

    [Header("Static Menu")] public GameObject _menu;

    [Header("Coins Answer")] public List<Image> _coinsAnswer;

    [Header("Header Coin Collision")] public GameObject _coinCurrent;


    [Header("Lifes Panel")] public Image _barLife;

    [Header("Count Right Answers")] public int _countRight;

    [Header("Mobile Controller")] public GameObject _mobileController;


    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        ResetWorld();

        _mobileController.SetActive(Application.isMobilePlatform);
    }

    void LateUpdate()
    {
        _current = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>();

        if(_current)
        {
            _menu.SetActive(_layouts.Contains(_current));
            transform.GetChild(0).gameObject.SetActive(_layouts.Contains(_current));
        }
            
    }

    public void QuitLifes()
    {
        _barLife.fillAmount -= 0.2f;
    }

    public void DisableColliderCoin()
    {
        _coinCurrent.GetComponent<CircleCollider2D>().enabled = false;
        _coinCurrent.GetComponent<Image>().color = new Color32(255,255,255,100);
    }

    public void SetCoinRight()
    {
        foreach (Image coin in _coinsAnswer)
        {
            if(coin.name == _coinCurrent.name)
                coin.color = new Color32(255,255,255,255);
        }
    }

    public void ResetWorld()
    {
        _countRight = 0;
        _barLife.fillAmount = 1f; 

        foreach (GameObject coin in _coins)
        {
            coin.GetComponent<CircleCollider2D>().enabled = true;
            coin.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
            
        _BehaviourPlayer.ResetPlayer();

        foreach (BehaviourLayout layout in _layouts)
        {
            if(layout.GetComponent<SistemaFinanciero_ManagerToggle>())
                layout.GetComponent<SistemaFinanciero_ManagerToggle>().ResetSeleccionarToggle();
        }

        foreach (Image coin in _coinsAnswer)
        {
            coin.color = new Color32(140,140,140,100);
        }
    }
}
