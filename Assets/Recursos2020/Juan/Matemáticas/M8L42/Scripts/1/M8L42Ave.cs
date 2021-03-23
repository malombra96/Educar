using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;

public class M8L42Ave : MonoBehaviour
{
    public float speed;

    public RectTransform hubicacion;

    Vector3 target;
    public Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = GetComponent<RectTransform>().anchoredPosition;        
    }

    // Update is called once per frame
    void Update()
    {
        if (hubicacion)
        {
            //target = Camera.main.ScreenToWorldPoint(hubicacion.anchoredPosition);
            GetComponent<Animator>().SetBool("volar", true);

            GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition,
                hubicacion.anchoredPosition, speed * Time.deltaTime); 

            if(hubicacion.anchoredPosition.x == GetComponent<RectTransform>().anchoredPosition.x)
                GetComponent<Animator>().SetBool("aterrisa", true);
        }         
    }
}
