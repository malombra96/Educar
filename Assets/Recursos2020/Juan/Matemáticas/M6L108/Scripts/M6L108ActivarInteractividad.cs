using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M6L108ActivarInteractividad : MonoBehaviour
{
    public GameObject Interacion;
    private void OnEnable()
    {
        if (gameObject.activeSelf && Interacion.GetComponent<BehaviourInteraccion>()._state)
        {
            Interacion.GetComponent<BehaviourInteraccion>()._controlInteractividad.SetInteraction(true, Interacion);
        }
    }
}
