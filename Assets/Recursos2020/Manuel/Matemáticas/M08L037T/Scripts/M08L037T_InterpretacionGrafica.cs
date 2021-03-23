using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_InterpretacionGrafica : MonoBehaviour
{
    ControlAudio _controlAudio;

    public Button _A,_B;
    public GameObject _TeoryA,_TeoryB;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _A.onClick.AddListener(delegate{ActiveTeory(_TeoryA);});
        _B.onClick.AddListener(delegate{ActiveTeory(_TeoryB);});

        _TeoryA.SetActive(false);
        _TeoryB.SetActive(false);
    }


    void ActiveTeory(GameObject g)
    {
        _controlAudio.PlayAudio(0);
        g.SetActive(true);
    }


}
