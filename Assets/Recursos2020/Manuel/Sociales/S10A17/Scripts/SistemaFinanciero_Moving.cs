using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaFinanciero_Moving : MonoBehaviour
{
    [Header("WayPoints")] 
    public RectTransform _A;
    public RectTransform _B;
    public RectTransform _C;

    [Header("Moving Speed")] [Range(100,500)] public int _speed;


    RectTransform _rectTransform;
    Vector3 _next;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _next = _A.anchoredPosition;
    }

    void Update()
    {
        if (Vector2.Distance(_rectTransform.anchoredPosition,_A.anchoredPosition) < 1)
            _next = _B.anchoredPosition;

        if(Vector2.Distance(_rectTransform.anchoredPosition,_B.anchoredPosition) < 1)//(_rectTransform.anchoredPosition == _B.anchoredPosition)
        {
            if(_C)
                _next = _C.anchoredPosition;
            else
                _next = _A.anchoredPosition;

        }
            
        if(_C)
        {
            if(Vector2.Distance(_rectTransform.anchoredPosition,_C.anchoredPosition) < 1)//(_rectTransform.anchoredPosition == _C.anchoredPosition)
            _next = _A.anchoredPosition;
        }

        
        _rectTransform.anchoredPosition = Vector3.MoveTowards(_rectTransform.anchoredPosition,_next,_speed*Time.deltaTime);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);

        if (collision.gameObject.name == "Player")
            collision.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.gameObject.name == "Player")
        {
            other.transform.parent = GameObject.Find("ContentWorld").transform;
            other.transform.SetAsLastSibling();
        }
            
    }
}
