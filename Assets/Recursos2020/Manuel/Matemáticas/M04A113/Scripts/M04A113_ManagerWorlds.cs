using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M04A113_ManagerWorlds : MonoBehaviour
{
    //[Header("Player")] public M04A113_BehaviourPlayer _BehaviourPlayer;
    [Header("Worlds")] public List<M04A113_BehaviourWorld> _worlds;
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public BehaviourLayout _current;
    
    [Header("Prefabs Player")] public List<GameObject> _prefabsPlayers;

    [Header("Selection Player")] public GameObject _playerSelection;


    // Start is called before the first frame update
    void Start()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    }

    void LateUpdate()
    {
        _current = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>();

        if(_current)
        {
            foreach (M04A113_BehaviourWorld world in _worlds)
            {
                world.gameObject.SetActive(world._aplicos.Contains(_current));
            }

            //transform.GetChild(0).gameObject.SetActive(_layouts.Contains(_current));
        }
            
    }
}
