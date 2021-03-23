using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class L4A255_aplico : MonoBehaviour
{
    public int a, d, p;
    public GameObject inicio, desemepeño, elementos;
    public Text porcenjae;
    public L4A255_player player;
    void Start()
    {

        a = PlayerPrefs.GetInt("aplico1");
        d = PlayerPrefs.GetInt("desmepeño1");
        p = PlayerPrefs.GetInt("p");
        porcenjae.text = p.ToString() + "%";
        print(porcenjae.text);
        print(a + "-" + d);
        if (a == 1)
        {
            player.mover = false;
            inicio.SetActive(true);
            elementos.SetActive(false);
            desemepeño.SetActive(false);
        }
        if (d == 1)
        {
            player.mover = false;
            inicio.SetActive(false);
            elementos.SetActive(false);
            desemepeño.SetActive(true);
        }

        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void conozco() {
        PlayerPrefs.SetInt("conozco", 1);
    }
}
