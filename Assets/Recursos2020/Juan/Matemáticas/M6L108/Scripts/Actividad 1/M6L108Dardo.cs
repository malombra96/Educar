using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M6L108Dardo : MonoBehaviour
{
    public float speed;
    public RectTransform hubicacion;
    public Vector3 posicionInicial;
    // Start is called before the first frame update
    void Awake()
    {
        posicionInicial = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()        
    {
        if (hubicacion)
        {
           Vector3 t;
            t = new Vector2(333, hubicacion.anchoredPosition.y);
            GetComponent<RectTransform>().anchoredPosition =
                Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, t, speed * Time.deltaTime);
        }
    }
}
