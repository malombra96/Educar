using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class L4A234_palabra : MonoBehaviour
{
    public bool presente, imperfecto, perfecto, futuro;
    public Sprite original,correcto,incorrecto;
    // Start is called before the first frame update
    void Awake()
    {
        //original = gameObject.GetComponent<Image>().sprite;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cambiar1() {
        print("s");
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = correcto;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void cambiar2()
    {
        print("a");
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = incorrecto;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
        gameObject.GetComponent<Image>().enabled = false;
    }
}
