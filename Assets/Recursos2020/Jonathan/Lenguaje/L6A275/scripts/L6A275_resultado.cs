using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6A275_resultado : MonoBehaviour
{
    public L6A275_general general;
    public GameObject Woman, Men;
    public bool w, m;
    public ControlPuntaje ControlPuntaje;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (!general.review)
        {
            general.enunciado.SetActive(false);
            general.toggleMan.gameObject.SetActive(false);
            general.toggleWoman.gameObject.SetActive(false);
            if (w)
            {
                Woman.SetActive(true);
                if (general.correctas == ControlPuntaje.questions)
                {
                    Woman.transform.GetChild(0).gameObject.SetActive(true);
                    Woman.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    Woman.transform.GetChild(0).gameObject.SetActive(false);
                    Woman.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
            if (m)
            {
                Men.SetActive(true);
                if (general.correctas == ControlPuntaje.questions)
                {
                    Men.transform.GetChild(0).gameObject.SetActive(true);
                    Men.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    Men.transform.GetChild(0).gameObject.SetActive(false);
                    Men.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }
}
