using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_review : MonoBehaviour
{

    public bool review;
    public int count = 0;
    public List<GameObject> questions;
    public GameObject aplic, desempeño;

    public M10L33_player _Player;
    public GameObject reviewIMage,leve1,leve2,level3;
    public GameObject nextArrow, previousArrow,joystick1,joystick2;

    [SerializeField] ControlAudio _Audio;
    

    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        // review = false;
        foreach (var item in questions)
        {
            item.SetActive(false);
            item.transform.GetChild(0).gameObject.SetActive(true);
            item.transform.GetChild(1).gameObject.SetActive(false);
            item.transform.GetChild(2).gameObject.SetActive(false);
        }
        questions[0].SetActive(true);


    }
    void Update()
    {

        if (review)
        {
            if (count >= 0 && count < 5) {
                leve1.SetActive(true);
                leve2.SetActive(false);
                level3.SetActive(false);
            }
            if (count >= 5 && count < 10) {
                leve2.SetActive(true);
                leve1.SetActive(false);
                level3.SetActive(false);
            }
            if (count >= 10 && count <questions.Count) {
                level3.SetActive(true);
                leve2.SetActive(false);
                leve1.SetActive(false);
            }
            joystick1.SetActive(false);
            joystick2.SetActive(false);
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
        }


    }

    public void ActivateReview()
    {
        count = 0;
        previousArrow.SetActive(false);
        // review = false;
        foreach (var item in questions)
        {
            item.SetActive(false);
            item.transform.GetChild(0).gameObject.SetActive(true);
            item.transform.GetChild(1).gameObject.SetActive(false);
            item.transform.GetChild(2).gameObject.SetActive(false);
        }
        questions[0].SetActive(true);
        review = true;
    


}
    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == questions.Count)
        {
            aplic.SetActive(false);
            desempeño.SetActive(true);
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

    public void ResetReview()
    {
        count = 0;
        previousArrow.SetActive(false);
        // review = false;
        foreach (var item in questions)
        {
            item.SetActive(false);
            item.transform.GetChild(0).gameObject.SetActive(true);
            item.transform.GetChild(1).gameObject.SetActive(false);
            item.transform.GetChild(2).gameObject.SetActive(false);
        }

        questions[0].SetActive(true);
        review = false;
    }
}



