using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_aplico : MonoBehaviour
{
    public GameObject pc, mobile, instruccion;
    public Button cerrar;


    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            pc.SetActive(false);
            mobile.SetActive(true);
        }
        else {
            pc.SetActive(true);
            mobile.SetActive(false);
        }

        cerrar.onClick.AddListener(Cerrar);
    }
    public void EnableImages()
    {
        if (Application.isMobilePlatform)
        {
            pc.SetActive(false);
            mobile.SetActive(true);
        }
        else
        {
            pc.SetActive(true);
            mobile.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cerrar() {
        instruccion.SetActive(true);
        if (Application.isMobilePlatform)
        {
            pc.SetActive(false);
            mobile.SetActive(false);
        }
        else
        {
            pc.SetActive(false);
            mobile.SetActive(false);
        }
        cerrar.gameObject.SetActive(false);
    }

    public void restart() {
        cerrar.gameObject.SetActive(true);
        if (Application.isMobilePlatform)
        {
            pc.SetActive(false);
            mobile.SetActive(true);
        }
        else
        {
            pc.SetActive(true);
            mobile.SetActive(false);
        }
        instruccion.SetActive(false);
    }
}
    