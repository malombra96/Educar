using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A235_teoria : MonoBehaviour
{
    // Start is called before the first frame updatepubli
    public GameObject botonCerrarLightbox,botonesSwipe,swipe;

    private void OnEnable()
    {
        botonCerrarLightbox.SetActive(false);
        botonesSwipe.SetActive(false);
        swipe.GetComponent<ScrollRect>().enabled = false;
    }

    private void OnDisable()
    {
        botonCerrarLightbox.SetActive(true);
        botonesSwipe.SetActive(true);
        swipe.GetComponent<ScrollRect>().enabled = true;
    }
}
