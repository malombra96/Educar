using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_conozco_2 : MonoBehaviour
{

    public ControlPuntaje puntaje;
    public List<Text> textos;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("C1") == 0) {
            textos[0].text = PlayerPrefs.GetInt("C1").ToString() + "%";
            print("0");
        }
        else{
            textos[0].text = PlayerPrefs.GetInt("C1").ToString() + "00%";
        }
        if (PlayerPrefs.GetInt("C2") == 0)
        {
            textos[1].text = PlayerPrefs.GetInt("C2").ToString() + "%";
        }
        else {
            textos[1].text = PlayerPrefs.GetInt("C2").ToString() + "00%";
        }
        if (PlayerPrefs.GetInt("C3") == 0)
        {
            textos[2].text = PlayerPrefs.GetInt("C3").ToString() + "%";
        }
        else{
            textos[2].text = PlayerPrefs.GetInt("C3").ToString() + "00%";
        }
        if (PlayerPrefs.GetInt("C4") == 0)
        {
            textos[3].text = PlayerPrefs.GetInt("C4").ToString() + "%";
        }
        else {
            textos[3].text = PlayerPrefs.GetInt("C4").ToString() + "00%";
        }
        
       
        
        



    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.A)) {
            PlayerPrefs.DeleteAll();
            print("Deleted");
        }
    }

    public void DeleteValues() {
        PlayerPrefs.DeleteAll();
    }
}
