using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M08L035_BehaviourInstructions : MonoBehaviour
{
    void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(!Application.isMobilePlatform);
        transform.GetChild(1).gameObject.SetActive(Application.isMobilePlatform);
    }
}
