using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10A38NextConozco : MonoBehaviour
{
    public GameObject[] info;
    ControlAudio controlAudio;
    public Button siguiente, anterior;
    int x = 0;
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();

        for(int i = 0; i < info.Length; i++)
        {
            info[i].SetActive(false);
        }
        anterior.interactable = false;
        info[0].SetActive(true);

        siguiente.onClick.AddListener(next);
        anterior.onClick.AddListener(back);
    }

    void next()
    {
        controlAudio.PlayAudio(0);
        if (x < info.Length-1)
        {
            info[x].SetActive(false);
            x++;
            info[x].SetActive(true);
           
            if (x == info.Length - 1)
            {
                siguiente.interactable = false;
            }
        }
        if (x > 0)
        {
            anterior.interactable = true;
        }
    }

    void back()
    {
        controlAudio.PlayAudio(0);

        info[x].SetActive(false);
        x--;
        info[x].SetActive(true);

        if (x == 0)
        {
            anterior.interactable = false;
        }

        if (x < info.Length - 1)
        {
            siguiente.interactable = true;
        }
    }
}
