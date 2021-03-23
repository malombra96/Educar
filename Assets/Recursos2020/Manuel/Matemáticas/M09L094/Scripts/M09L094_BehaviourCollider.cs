using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09L094_BehaviourCollider : MonoBehaviour
{
    [Header("World")] public M09L094_World _world;
    [Header("Index Question")] public int _index;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Mago")
        {
            _world.SetQuestionPanel(_index);
            gameObject.SetActive(false);
        }
            
            
    }

}
