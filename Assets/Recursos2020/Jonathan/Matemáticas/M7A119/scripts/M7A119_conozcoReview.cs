using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M7A119_conozcoReview : MonoBehaviour
{
    public bool review;
    public int count = 0;
    public List<GameObject> informacion;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;


    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        // review = false;
        foreach (var item in informacion)
        {
            item.gameObject.SetActive(false);
        }
        informacion[0].SetActive(true);


    }
    void Update()
    {
        if (review)
        {
            if (count == 0)
            {
                previousArrow.SetActive(false);
            }
            if (count == informacion.Count-1) {
                nextArrow.SetActive(false);
            }
            if (count < informacion.Count-1 && count > 0) {
                nextArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
        }
    }

    public void ActivateReview()
    {
        review = true;
        count = 0;
        foreach (var item in informacion)
        {
            item.SetActive(false);
        }

        informacion[0].SetActive(true);
    }
    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == informacion.Count)
        {

        }
        else if (count <= informacion.Count)
        {
            foreach (var item in informacion)
            {
                item.SetActive(false);
            }
            informacion[count].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        if (count > 0)
        {
            count--;
            foreach (var item in informacion)
            {
                item.SetActive(false);
            }
            informacion[count].SetActive(true);
        }
    }
}
