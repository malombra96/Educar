using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class L9A279Mira : MonoBehaviour
{
    public bool mover;
    L9A279Manager manager;
    private void Start()
    {
        manager = transform.GetComponentInParent<L9A279Manager>();
    }

    void FixedUpdate()
    {
        if (mover)
        {
            Vector2 Pospuntero = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Transform>().position = new Vector2(Pospuntero.x, Pospuntero.y);
            GetComponent<RectTransform>().anchoredPosition = new Vector2
                (Mathf.Clamp(GetComponent<RectTransform>().anchoredPosition.x, -613, 613), Mathf.Clamp(GetComponent<RectTransform>().anchoredPosition.y, -96, 200));
        }
    }
    public void seleccion(RectTransform r)
    {
        mover = false;
        GetComponent<RectTransform>().anchoredPosition = r.anchoredPosition;
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;        
    }
    public void poderMover() => mover = !mover;
}
