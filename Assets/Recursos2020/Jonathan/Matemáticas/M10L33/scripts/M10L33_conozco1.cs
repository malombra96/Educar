using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_conozco1 : MonoBehaviour
{
    public Text nombre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            PlayerPrefs.DeleteAll();
        }
    }

    public void review() {
        PlayerPrefs.SetInt("review",1);
        PlayerPrefs.SetString("nombre",nombre.text);
    }
}
