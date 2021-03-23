using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M08L037T_BehaviourPuntero : MonoBehaviour
{
    [Header("Canvas")] public Canvas myCanvas;

    public bool interactuable;

    private void Start()
    {
        interactuable = true;
    }

    private void Update()
    {
        if (interactuable)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            transform.position = myCanvas.transform.TransformPoint(pos);
        }

    }
}
