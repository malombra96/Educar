using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L03A229_CortuineCountry : MonoBehaviour
{   
    void OnEnable() => FindObjectOfType<ControlNavegacion>().Forward(2);
}
