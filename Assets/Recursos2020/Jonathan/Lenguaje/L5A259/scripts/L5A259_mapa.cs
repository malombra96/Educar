using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A259_mapa : MonoBehaviour
{
    public GameObject barco;
    public bool mover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mover) {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, gameObject.GetComponent<RectTransform>().anchoredPosition.y*3);

        }
    }
}
