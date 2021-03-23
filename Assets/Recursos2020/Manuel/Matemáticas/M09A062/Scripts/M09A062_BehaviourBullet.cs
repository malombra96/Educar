using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09A062_BehaviourBullet : MonoBehaviour
{
    [Header("Velovidad del proyectil")] public float vel;
    [Header("Tiempo proyectil")] public int time;
    
    void Start () => Destroy (gameObject, time);
	
    void Update () => transform.Translate(0,vel*Time.deltaTime,0);
}
