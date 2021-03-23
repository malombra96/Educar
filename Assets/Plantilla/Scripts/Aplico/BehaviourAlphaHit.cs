using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourAlphaHit : MonoBehaviour
{
    [Header("Ingrese el valor minimo de transparencia")] [Range(0,1)] public float _alpha; 
    void Start()
    {
        if(_alpha == 0)
            _alpha = 0.5f;
        else
            GetComponent<Image>().alphaHitTestMinimumThreshold = _alpha;
    }
}
