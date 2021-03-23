using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L56DarPuntos : MonoBehaviour
{
    public GameObject puntos;

    private void OnEnable()
    {
        for (int x = 0; x < transform.childCount; x++)
            transform.GetChild(x).GetComponent<Text>().text = puntos.transform.GetChild(x).GetComponent<Text>().text;
    }
}
