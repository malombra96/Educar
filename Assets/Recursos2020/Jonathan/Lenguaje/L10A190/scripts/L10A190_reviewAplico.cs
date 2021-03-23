using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10A190_reviewAplico : MonoBehaviour
{
    public GameObject aplico1, review, desemepeño, NoCavnas, nombre;
    public int x;
    public InputField nom;
    public Text nom1;
    // Start is called before the first frame update
    void Start()
    {
        x = PlayerPrefs.GetInt("review");
        if (x == 1)
        {
            aplico1.SetActive(true);
            review.SetActive(true);
            desemepeño.SetActive(false);
            NoCavnas.SetActive(true);
            nombre.SetActive(false);
            nom.text = PlayerPrefs.GetString("nombre");
            nom1.text = PlayerPrefs.GetString("nombre");
            GetComponent<AudioSource>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Escape)))
        {
            print("x");
            PlayerPrefs.DeleteAll();
        }
    }
}
