using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M04A113_BehaviourCollider : MonoBehaviour
{
    [Header("World")] public M04A113_BehaviourWorld _world;
    [Header("Manager World")] M04A113_ManagerWorlds _ManagerWorlds;

    void Start() => _ManagerWorlds = FindObjectOfType<M04A113_ManagerWorlds>();

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.GetComponent<M04A113_BehaviourPlayer>().SetBehaviourPlayer(false);
            _world._colliderCurrent = gameObject;


            if (_ManagerWorlds._current)
            {
                if (_ManagerWorlds._current.GetComponent<M04A113_ManagerInput>() || _ManagerWorlds._current.GetComponent<M04A113_ManagerToggle>() || _ManagerWorlds._current.GetComponent<M04A113_ManagerBar>())
                {
                    _ManagerWorlds._current.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
}
