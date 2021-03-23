using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09L094_BehaviourPower : MonoBehaviour
{
    [Header("Velovidad del proyectil")] public float vel;
    [Header("Tiempo proyectil")] public int time;

    void Start ()
    {
        if(transform.localScale.x < 0)
          vel = vel*-1;

        Destroy (gameObject, time);
    } 
	
    void Update () => transform.Translate(vel*Time.deltaTime,0,0);

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "Rock")
        {
            Destroy(col.collider.gameObject);
            Destroy(gameObject);
        }
        else if (col.collider.name == "Golem")
        {
            col.collider.GetComponent<M09L094_BehaviourGolem>().DestroyGolem();
            ControlNavegacion x = FindObjectOfType<ControlNavegacion>();

            if (x.GetLayoutActual())
            {
                if (x.GetLayoutActual().GetComponent<M09L094_ManagerToggle>())
                {
                    if (x.GetLayoutActual().GetComponent<M09L094_ManagerToggle>()._isLast)
                    {
                        x.Forward(1);
                        Destroy(gameObject);
                    }
                }
                else if(x.GetLayoutActual().GetComponent<M09L094_ManagerDD>())
                {
                    if (x.GetLayoutActual().GetComponent<M09L094_ManagerDD>()._isLast)
                    {
                        x.Forward(1);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
