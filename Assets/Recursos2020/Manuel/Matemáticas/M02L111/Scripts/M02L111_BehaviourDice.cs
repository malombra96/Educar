using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M02L111_BehaviourDice : MonoBehaviour
{
    public List<Vector3> _statesAngles;
    int n;

    void Awake() => n=0;

    void OnEnable()
    {
        transform.eulerAngles = _statesAngles[n];
        n++;

        if(n==6)
            n=0;
    }

    
}
