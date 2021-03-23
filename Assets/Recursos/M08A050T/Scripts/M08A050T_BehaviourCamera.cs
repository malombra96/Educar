using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M08A050T_BehaviourCamera : MonoBehaviour
{
    [Header("Limits Camera")]
    public float XMin;
    public float XMax;

    public float YMin;
    public float YMax;

    Transform _target;

    void Start() => _target = FindObjectOfType<M08A050T_BehaviourPlayer>().transform;

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(_target.position.x,XMin,XMax),Mathf.Clamp(_target.position.y,YMin,YMax),transform.position.z);
    }



}
