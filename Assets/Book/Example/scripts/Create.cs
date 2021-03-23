using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Create : MonoBehaviour, IPointerClickHandler
{
    public GameObject input;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject a = Instantiate(input, Vector3.zero, new Quaternion(0,0,0,0),transform);
        a.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
