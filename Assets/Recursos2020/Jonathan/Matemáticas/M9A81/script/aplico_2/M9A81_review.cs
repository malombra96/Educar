using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A81_review : MonoBehaviour
{



    public bool review;
    public int count = 0;
    public List<GameObject> questions;
    public GameObject aplic, aplico2,seleccionar;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;
    [SerializeField] ControlNavegacion _navegacion;


    void Start()
    {
        seleccionar.SetActive(false);
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        // review = false;
        foreach (var item in questions)
        {
            item.gameObject.SetActive(false);
        }
        questions[0].SetActive(true);


    }
    void Update()
    {
        if (review)
        {
            if (count > 0)
            {
                previousArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
            if (count == 0)
            {
                previousArrow.SetActive(true);
            }
        }
    }

    public void ActivateReview()
    {
        review = true;
        count = 0;
        foreach (var item in questions)
        {
            item.SetActive(false);
        }

        questions[0].SetActive(true);
    }
    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == questions.Count)
        {
            count = questions.Count - 1;
            _navegacion.Forward();
        }
        else if (count < questions.Count)
        {
            foreach (var item in questions)
            {
                item.SetActive(false);
            }
            questions[count].SetActive(true);
            
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        if (count > -1)
        {
            count--;
            if (count == -1)
            {
                questions[0].SetActive(true);
                count = 0;
                _navegacion.Backward();
                
            }
            else {
                foreach (var item in questions)
                {
                    item.SetActive(false);
                }
                questions[count].SetActive(true);
            }
            

        }

    }
}
