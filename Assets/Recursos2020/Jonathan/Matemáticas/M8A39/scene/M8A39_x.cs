using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A39_x : MonoBehaviour
{
    // Start is called before the first frame update
    public Text nombre;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Escape))) {
            print("x");
            PlayerPrefs.DeleteAll();
        }
    }
    public void SetPlayerpref()
    {
        PlayerPrefs.SetInt("review",1);
        PlayerPrefs.SetString("nombre",nombre.text);
    }
}
