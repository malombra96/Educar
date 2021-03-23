using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A74_conozco2 : MonoBehaviour
{
    
    public int count = 0;
    public List<Sprite> questions,x;
    public Image imagen, imagen2;
    

    
    
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;


    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);

        imagen.sprite = questions[0];
        imagen2.sprite = x[0];


    }
    void Update()
    {
      
        if (count > 0)
        {
            previousArrow.SetActive(true);
        }
        if (count == 0)
        {
            previousArrow.SetActive(false);
        }
        if (count == questions.Count-1)
        {
            nextArrow.SetActive(false);
        }
        if (count == questions.Count - 2)
        {
            nextArrow.SetActive(true);
        }



    }

    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
       
        if (count <= questions.Count)
        {
            imagen.sprite = questions[count];
            imagen2.sprite = x[count];
            imagen2.SetNativeSize();
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        if (count > 0)
        {
            count--;
            imagen.sprite = questions[count];
            imagen2.sprite = x[count];
            imagen2.SetNativeSize();
        }

    }
}
