using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09A129_BehaviourFruit : MonoBehaviour
{
    M09A129_ControlWorld _world;

    // Start is called before the first frame update
    void Start()
    {
        _world = FindObjectOfType<M09A129_ControlWorld>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            col.GetComponent<M09A129_BehaviourPlayer>().SetBehaviourPlayer(false);
            _world._fruitCurrent = gameObject;

            if(_world._current)
            {
                if(_world._current.GetComponent<M09A129_ManagerInput>() || _world._current.GetComponent<M09A129_ManagerToggle>())
                {
                    _world._current.transform.GetChild(0).gameObject.SetActive(true);
                }
            }

        }
    }
    
}
