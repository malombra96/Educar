using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1A109_ActivarInteractivo : MonoBehaviour
{
    public GameObject interactuable;
    private void OnEnable()
    {
        if(gameObject.activeSelf && interactuable.GetComponent<BehaviourInteraccion>()._state)
        {
            interactuable.GetComponent<BehaviourInteraccion>()._controlInteractividad.SetInteraction(true, interactuable);
        }
    }
}
