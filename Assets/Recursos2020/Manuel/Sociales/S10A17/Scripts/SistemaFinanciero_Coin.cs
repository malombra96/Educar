using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaFinanciero_Coin : MonoBehaviour
{
    SistemaFinanciero_ControlWorld _world;

    // Start is called before the first frame update
    void Start()
    {
        _world = FindObjectOfType<SistemaFinanciero_ControlWorld>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            col.GetComponent<SistemaFinanciero_Player>().SetBehaviourPlayer(false);
            _world._coinCurrent = gameObject;

            if(_world._current)
            {
                if(_world._current.GetComponent<SistemaFinanciero_ManagerToggle>())
                {
                    _world._current.transform.GetChild(0).gameObject.SetActive(true);
                    StartCoroutine(_world._current.GetComponent<SistemaFinanciero_ManagerToggle>().StateBtnValidar());
                }
            }

        }
    }
}
