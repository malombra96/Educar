using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A259_aplico : MonoBehaviour
{
    // Start is called before the first frame update
    public int a, d,p;
    public GameObject aplico, desemepeño,aplico2d, barco,canvas,inicio;
    public Text porcenjae;
    void Start()
    {
        
        a = PlayerPrefs.GetInt("aplico1");
        d = PlayerPrefs.GetInt("desmepeño1");
        p = PlayerPrefs.GetInt("p");
        porcenjae.text = p.ToString()+"%";
        print(porcenjae.text);
        print(a + "-" + d);
        if (a == 1) {
            aplico.SetActive(true);
            barco.SetActive(true);
            barco.GetComponent<L5A259_barco>().mover = false;
            aplico2d.SetActive(true);
            desemepeño.SetActive(false);
            inicio.SetActive(true);
        }
        if (d == 1) {
            inicio.SetActive(false);
            barco.SetActive(false);
            aplico2d.SetActive(false);
            aplico.SetActive(false);
            desemepeño.SetActive(true);
        }
        
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void borrar() {
        PlayerPrefs.DeleteAll();
    }
}

