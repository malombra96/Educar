using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A39_review : MonoBehaviour
{


    public bool review;
    public int count = 0;
    public List<GameObject> questions;
    public GameObject aplic, aplico2;

    public M8A39_player _Player;
    public GameObject reviewIMage;
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
            if (count > questions.Count) {
                aplic.SetActive(false);
                aplico2.SetActive(true);
                reviewIMage.SetActive(true);
                reviewIMage.GetComponent<M8A39_review1>().count = 0;
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
            aplico2.SetActive(true);
            reviewIMage.SetActive(true);
            reviewIMage.GetComponent<M8A39_review1>().count = 0;
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
