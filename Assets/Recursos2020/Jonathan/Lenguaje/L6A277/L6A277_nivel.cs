using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6A277_nivel : MonoBehaviour
{
    public GameObject fondo;
    public L6A277_managerSeleccionar seleccionar;
    public bool reset,review;
    // Start is called before the first frame update
    void Start()
    {
        fondo = gameObject.transform.GetChild(0).gameObject;
        seleccionar= gameObject.transform.GetChild(1).gameObject.GetComponent<L6A277_managerSeleccionar>();
        fondo.SetActive(true);
        StartCoroutine(OcultarMostrar());
    }

    IEnumerator OcultarMostrar() {
        yield return new WaitForSeconds(2f);
        seleccionar.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (reset && !review) {
            StartCoroutine(OcultarMostrar());
        }
    }
}
