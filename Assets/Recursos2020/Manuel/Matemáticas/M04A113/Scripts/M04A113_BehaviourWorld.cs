using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M04A113_BehaviourWorld : MonoBehaviour
{
    [Header("Manager Worlds")] public M04A113_ManagerWorlds _managerWorlds;
    [Header("BehaviourLayout")] public List<BehaviourLayout> _aplicos;

    [Header("Colliders Question")] public List<Collider2D> _colliders;

    [Header("Diamonds")] public List<GameObject> _diamonds;

    [Header("Player Instantiate")] public GameObject _player;

    [Header("Current Fruit Collision")] public GameObject _colliderCurrent;

    [Header("Mobile Controllers")] public GameObject _mobileControllers;


    // Start is called before the first frame update
    void OnEnable()
    {
        if(_managerWorlds._current)
        {
            if(!_managerWorlds._current.GetComponent<BehaviourLayout>()._isEvaluated)
                InstantiateNewPlayer();
        }
        
    }

    void Start()
    {
        foreach (GameObject diamond in _diamonds)
            diamond.SetActive(false);

        _mobileControllers.SetActive(Application.isMobilePlatform);
    }

    void InstantiateNewPlayer()
    {
        if(_managerWorlds._playerSelection)
        {
            _player = Instantiate(_managerWorlds._playerSelection,transform);
            _player.GetComponent<RectTransform>().anchoredPosition = new Vector2(-630,24);
            _player.name = "Player";

        }
    }

    public void DisableCollider() => _colliderCurrent.GetComponent<BoxCollider2D>().enabled = false;

    public void SetDiamondRight()
    {
        int n = _colliders.IndexOf(_colliderCurrent.GetComponent<BoxCollider2D>());
        _diamonds[n].SetActive(true);    
    }

    public void ResetWorld()
    {
        foreach (Collider2D collider in _colliders)
            collider.enabled = true;
            

        foreach (GameObject diamond in _diamonds)
            diamond.SetActive(false);

        foreach (BehaviourLayout layout in _aplicos)
        {
            if(layout.GetComponent<M04A113_ManagerInput>())
                layout.GetComponent<M04A113_ManagerInput>().resetAll();
            else if(layout.GetComponent<M04A113_ManagerToggle>())
                layout.GetComponent<M04A113_ManagerToggle>().ResetSeleccionarToggle();
            else if(layout.GetComponent<M04A113_ManagerBar>())
                layout.GetComponent<M04A113_ManagerBar>().ResetBAR();
            
        }

        if(_player)
            Destroy(_player,1);
    }   

    ///////////// Collision Player ////////////////////////////////////

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
            Destroy(col.gameObject,1);

        InstantiateNewPlayer();
    }

}
