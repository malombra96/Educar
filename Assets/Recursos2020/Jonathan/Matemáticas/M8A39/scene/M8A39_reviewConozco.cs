using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A39_reviewConozco : MonoBehaviour
{
    public GameObject aplico1, aplico2, review, review_1,desemepeño,NoCavnas, image, nombre;
    public int x;
    public InputField nom;
    public Text nom1;
    // Start is called before the first frame update
    void Start()
    {
        x = PlayerPrefs.GetInt("review");
        if (x == 1) {
            aplico1.SetActive(true);
            review.SetActive(true);
            review_1.SetActive(false);
            desemepeño.SetActive(false);
            NoCavnas.SetActive(true);
            image.SetActive(false);
            nombre.SetActive(false);
            nom.text = PlayerPrefs.GetString("nombre");
            nom1.text = PlayerPrefs.GetString("nombre");
            GetComponent<AudioSource>().enabled = false;
            aplico2.GetComponent<AudioSource>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if ((Input.GetKey(KeyCode.Escape)))
        //{
        //    print("x");
        //    PlayerPrefs.DeleteAll();
        //}
    }
}
