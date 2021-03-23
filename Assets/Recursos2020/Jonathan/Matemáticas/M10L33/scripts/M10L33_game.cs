using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M10L33_game : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject aplico, desempeño, review;
    public M10L33_review reviewScript;
    void Start()
    {
        if (PlayerPrefs.GetInt("review") == 1) {
            aplico.SetActive(true);
            desempeño.SetActive(false);
            review.SetActive(true);
            reviewScript.ActivateReview();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
            print("x");
        }
    }
}
