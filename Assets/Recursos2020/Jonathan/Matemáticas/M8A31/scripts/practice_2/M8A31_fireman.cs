using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8A31_fireman : MonoBehaviour
{
    public bool isRight;

    public List<Sprite> _sprites;

    M8A31_ManagerHelicopter _manager;
    private void Start()
    {
        _manager = GameObject.FindObjectOfType<M8A31_ManagerHelicopter>();
        _manager._firemanObjects.Add(gameObject);
    }
}
