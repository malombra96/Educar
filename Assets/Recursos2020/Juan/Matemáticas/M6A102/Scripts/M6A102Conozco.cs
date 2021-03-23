using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6A102Conozco : MonoBehaviour
{
    public Button positivo;
    public Button negativo;

    public List<GameObject> Positivos;
  
    public List<GameObject> Negativos;
    public List<GameObject> textos;
    ControlAudio controlAudio;
    int x;   
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();

        positivo.onClick.AddListener(numerosPositivos);
        negativo.onClick.AddListener(numerosNegativos);
        textos[0].SetActive(true);
        negativo.interactable = false;
    }

    void numerosPositivos()
    {           
        controlAudio.PlayAudio(0);
        if (x < Positivos.Count)
        {
            Positivos[x].SetActive(true);
            if (x == 3)
            {
                textos[2].SetActive(true);
                textos[1].SetActive(false);
            }
            else if (x == 0)
            {
                textos[1].SetActive(true);
                textos[0].SetActive(false);
                negativo.interactable = true;
            }
            else if (x == Positivos.Count - 1)
                positivo.interactable = false;
            x++;
        }
    }
    void numerosNegativos()
    {       
        controlAudio.PlayAudio(0);
        if (x > 0)
        {
            x--;
            Positivos[x].SetActive(false);
            if (x == 3)
            {
                textos[2].SetActive(false);
                textos[1].SetActive(true);
            }
            else if (x == 0)
            {
                textos[1].SetActive(false);
                textos[0].SetActive(true);
                negativo.interactable = false;
            }
            else if (x == Positivos.Count - 1)
                positivo.interactable = true;
        }
    }
    public void close()
    {
        textos[0].SetActive(true);
        textos[1].SetActive(false);       
        textos[2].SetActive(false);
        negativo.interactable = false;
        positivo.interactable = true;
        x = 0;
        for(int i = 0; i < Positivos.Count; i++)
            Positivos[i].SetActive(false);        
    }
}
