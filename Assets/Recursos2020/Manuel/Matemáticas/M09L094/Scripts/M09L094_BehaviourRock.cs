using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09L094_BehaviourRock : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "Rock" || col.collider.name == "EndWorld")
            Destroy(gameObject);
        else if(col.collider.name == "Mago")
        {
            col.collider.GetComponent<M09L094_BehaviourMago>().KnockRock();
            Destroy(gameObject);
        }
    }
}
