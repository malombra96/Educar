using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10A190_review : MonoBehaviour
{
    public bool review;
    public int count = 0;
    public List<GameObject> questions;
    public GameObject aplic, desempeno;

    public L10A190_player _Player;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;


    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        // review = false;
        foreach (var item in questions)
        {
            item.transform.GetChild(0).gameObject.SetActive(true);
        }
        questions[0].SetActive(true);


    }
    void Update()
    {
        if (review)
        {

            _Player.canMove = false;
            if (count > 0)
            {
                previousArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
            if (count == 0)
            {
                previousArrow.SetActive(false);
            }
            if (count > questions.Count)
            {
                aplic.SetActive(false);
                desempeno.SetActive(true);
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
            aplic.SetActive(false);
            desempeno.SetActive(true);
        }
        else if (count <= questions.Count)
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

        if (count > 0)
        {
            count--;
            foreach (var item in questions)
            {
                item.SetActive(false);
            }
            questions[count].SetActive(true);

        }

    }
}
