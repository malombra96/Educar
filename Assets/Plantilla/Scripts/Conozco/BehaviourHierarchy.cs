using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourHierarchy : MonoBehaviour
{
    void Update() => GetComponent<RectTransform>().SetSiblingIndex(GetComponent<RectTransform>().parent.childCount - 1);
}
