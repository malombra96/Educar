using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M08A050T_BehaviourPlatform : MonoBehaviour
{
    CapsuleCollider2D _playerCollider;

    public BoxCollider2D platformCollider;
    public BoxCollider2D platformTrigger;


    // Start is called before the first frame update
    void Start()
    {
        _playerCollider = FindObjectOfType<M08A050T_BehaviourPlayer>().GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreCollision(platformCollider,platformTrigger,true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Character")
        {
            Physics2D.IgnoreCollision(platformCollider,_playerCollider,true);
        }
    }


}
