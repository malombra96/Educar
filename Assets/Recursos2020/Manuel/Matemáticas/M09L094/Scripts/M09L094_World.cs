using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09L094_World : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;

    BehaviourLayout _currentLayout;

    [Header("Layouts Associated")] public List<BehaviourLayout> _layouts;

    [Header("Colliders Associated")] public List<BoxCollider2D> _colliders;

    [Header("Player-Mago")] public M09L094_BehaviourMago _mago;
    [Header("Enemy-Golem")] public M09L094_BehaviourGolem _golem;

    [Header("Lifes Mago")] 
    public int _lifes; 
    public Transform _groupLifes;

    [Header("EndGame Go To")] public int _nextWorld;

    [Header("Mobile Controllers")] public GameObject _mobileControllers;

    [Header("UI Pause")] 
    
    public Button _pause;
    public Button _play;

    public GameObject _panelPause;

    void OnEnable() => _mobileControllers.SetActive(Application.isMobilePlatform);

    void Start()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _lifes = 3;

        _pause.onClick.AddListener(delegate{PauseGameUI(false);});
        _play.onClick.AddListener(delegate{PauseGameUI(true);});
        _panelPause.SetActive(false);

        foreach (BehaviourLayout layout in _layouts)
        {
            for (int i = 0; i < layout.transform.childCount; i++)
                layout.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    public void SetLife()
    {
        _lifes--;

        for (int i = 0; i < _groupLifes.childCount; i++)
            _groupLifes.GetChild(i).gameObject.SetActive((i+1) <= _lifes);

        if(_lifes == 0)
        {
            PauseGame(false);
            _controlNavegacion.GoToLayout(_nextWorld,2);
        }
            
    }

    public void SetQuestionPanel(int index)
    {
        print(index);

        PauseGame(false);

        for (int i = 0; i < _layouts.Count; i++)
        {
            for (int j = 0; j < _layouts[i].transform.childCount; j++)
            {
                _layouts[i].transform.GetChild(j).gameObject.SetActive((i == index));
            }
        }

        if(_layouts[index].GetComponent<M09L094_ManagerToggle>())
            StartCoroutine(_layouts[index].GetComponent<M09L094_ManagerToggle>().StateBtnValidar());
        else 
            StartCoroutine(_layouts[index].GetComponent<M09L094_ManagerDD>().StateBtnValidar());

    }

    public void PauseGame(bool state)
    {
        _mago.SetStateBehaviour(state);
        _golem.SetStateBehaviour(state);

        if (!state)
        {
            M09L094_BehaviourRock[] rocks = FindObjectsOfType<M09L094_BehaviourRock>();

            foreach (var rock in rocks)
                Object.Destroy(rock.gameObject);
        }
        else
        {
            _golem.ShootingRock();
        }

    }

    public void PauseGameUI(bool state)
    {
        _pause.gameObject.SetActive(state);
        _play.gameObject.SetActive(!state);

        _panelPause.SetActive(!state);

        PauseGame(state);
    }

    public void ResetWorld()
    {
        _mago.ResetStates();
        _golem.ResetStates();
        _lifes = 3;

        for (int i = 0; i < _groupLifes.childCount; i++)
            _groupLifes.GetChild(i).gameObject.SetActive(true);

        foreach (BoxCollider2D collider in _colliders)
        {
            collider.gameObject.SetActive(true);
        }

        foreach (BehaviourLayout aplico in _layouts)
        {
            if(aplico.GetComponent<M09L094_ManagerToggle>())
                aplico.GetComponent<M09L094_ManagerToggle>().ResetSeleccionarToggle();
            else if(aplico.GetComponent<M09L094_ManagerDD>())
                aplico.GetComponent<M09L094_ManagerDD>().ResetDragDrop();
        }
    }
}
