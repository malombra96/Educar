using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class M4A112_togglePestaa : MonoBehaviour
{
    public GameObject nextToggle,backToggle;
    public float speed = 10.0f;    
    public Vector3 initialPosition,nextPosition,backPosition;
    public bool click,noClick;
    private void Awake()
    {
        initialPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;        
    }

    void Update()
    {
        if (click) {
            float step = speed * Time.deltaTime;
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(gameObject.GetComponent<RectTransform>().anchoredPosition, nextPosition, step);
            
        }
        if (noClick)
        {
            float step = speed * Time.deltaTime;
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(gameObject.GetComponent<RectTransform>().anchoredPosition,backPosition, step);
        }
    }
}
