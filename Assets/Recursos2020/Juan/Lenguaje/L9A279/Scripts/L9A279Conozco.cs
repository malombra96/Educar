using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L9A279Conozco : MonoBehaviour
{
    ControlAudio controlAudio;
    public GameObject info_1;
    public GameObject info_2;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        GetComponent<Button>().onClick.AddListener(cambio);
    }

    // Update is called once per frame
    void cambio()
    {
        controlAudio.PlayAudio(0);
        if (info_1.activeSelf)
        {
            info_1.SetActive(false);
            info_2.SetActive(true);
        }
        else
        {
            info_1.SetActive(true);
            info_2.SetActive(false);
        }
    }
}
