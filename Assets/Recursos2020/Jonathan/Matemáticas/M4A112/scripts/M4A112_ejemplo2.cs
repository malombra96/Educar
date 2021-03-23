using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M4A112_ejemplo2 : MonoBehaviour
{
    public bool review;
    public int count = 0;
    public List<GameObject> questions,imagenes;
    
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;
    [SerializeField] ControlNavegacion _navegacion;


    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(true);
        // review = false;
        foreach (var item in questions)
        {
            item.gameObject.SetActive(false);
        }
        questions[0].SetActive(true);

        foreach (var item in imagenes)
        {
            item.gameObject.SetActive(false);
        }
        imagenes[0].SetActive(true);


    }
    void Update()
    {
        if (review)
        {
            if (count > 0)
            {
                previousArrow.GetComponent<Button>().interactable = true;
            }
            if (count == 0)
            {
                previousArrow.GetComponent<Button>().interactable = false;
            }

            if (count == questions.Count-1) {
                nextArrow.GetComponent<Button>().interactable = false;
            }
            if (count < questions.Count-1) {
                nextArrow.GetComponent<Button>().interactable = true;
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
        foreach (var item in imagenes)
        {
            item.gameObject.SetActive(false);
        }
        imagenes[0].SetActive(true);
    }
    public void NextQuestion()
    {
        

        _Audio.PlayAudio(0);
        if (count == questions.Count)
        {
            //count = questions.Count - 1;
            //_navegacion.Forward();
        }
        else if (count < questions.Count-1)
        {
            //foreach (var item in questions)
            //{
            //    item.SetActive(false);
            //}
            count++;
            questions[count].SetActive(true);
            foreach (var item in imagenes)
            {
                item.gameObject.SetActive(false);
            }
            imagenes[count].SetActive(true);

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
                //questions[0].SetActive(true);
                //count = 0;
                //_navegacion.Backward();

            }
            else
            {
                //foreach (var item in questions)
                //{
                //    item.SetActive(false);
                //}
                questions[count+1].SetActive(false);
                foreach (var item in imagenes)
                {
                    item.gameObject.SetActive(false);
                }
                imagenes[count].SetActive(true);
            }


        }

    }
}
