using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A129_ControlWorld : MonoBehaviour
{
    [Header("Player")] public M09A129_BehaviourPlayer _BehaviourPlayer;
    ControlNavegacion _controlNavegacion;
    [Header("Layouts Aplico")] public List<BehaviourLayout> _layouts;

    [Header("Fruits Objects")] public List<GameObject> _fruits;
    [HideInInspector] public BehaviourLayout _current;

    [Header("Static Menu")] public GameObject _menu;

    [Header("Down Panel")] public List<GameObject> _downPanel;
    [Header("Header Fruit Collision")] public GameObject _fruitCurrent;

    [Header("Lifes Panel")]

    /* [HideInInspector] */ public int _lifes;
    public RectTransform _groupLifes;

    [Header("Mobile Controllers")] 
    public GameObject _mobileControllers;



    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _mobileControllers.SetActive(Application.isMobilePlatform);
        ResetWorld();
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
        _lifes--;

        for (int i = 0; i < _groupLifes.childCount; i++)
            _groupLifes.GetChild(i).gameObject.SetActive(i<=_lifes);
    }

    public void DisableColliderFruit()
    {
        _fruitCurrent.GetComponent<CircleCollider2D>().enabled = false;
        _fruitCurrent.GetComponent<Image>().color = new Color32(255,255,255,100);
    }

    public void SetFruitRight()
    {
        foreach (GameObject fruit in _downPanel)
        {
            if(!fruit.activeSelf)
                fruit.SetActive(fruit.name == _fruitCurrent.name);
        }
         
    }

    public void ResetWorld()
    {
        _lifes = 4;

        foreach (GameObject fruit in _fruits)
        {
            fruit.GetComponent<CircleCollider2D>().enabled = true;
            fruit.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
            
        for (int i = 0; i < _groupLifes.childCount; i++)
            _groupLifes.GetChild(i).gameObject.SetActive(true);

        foreach (GameObject fruit in _downPanel)
            fruit.SetActive(false);

        _BehaviourPlayer.ResetPlayer();

        foreach (BehaviourLayout layout in _layouts)
        {
            if(layout.GetComponent<M09A129_ManagerInput>())
                layout.GetComponent<M09A129_ManagerInput>().resetAll();
            else if(layout.GetComponent<M09A129_ManagerToggle>())
                layout.GetComponent<M09A129_ManagerToggle>().ResetSeleccionarToggle();
        }
    }
}
