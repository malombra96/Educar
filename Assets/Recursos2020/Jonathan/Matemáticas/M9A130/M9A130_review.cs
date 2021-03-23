using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A130_review : MonoBehaviour
{
    public bool review;
    public int count = 0;
    public List<M9A130_managerSeleccionar> informacion;
    public GameObject nextArrow, previousArrow;
    public ControlNavegacion ControlNavegacion;
    [SerializeField] ControlAudio _Audio;


    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        //previousArrow.SetActive(false);
        // review = false;
        //foreach (var item in informacion)
        //{
        //    item.gameObject.SetActive(false);
        //}
        //informacion[0].SetActive(true);


    }
    void Update()
    {
        if (review)
        {
            //if (count == 0)
            //{
            //    previousArrow.SetActive(false);
            //}
            //if (count == informacion.Count - 1)
            //{
            //    nextArrow.SetActive(false);
            //}
            //if (count < informacion.Count - 1 && count > 0)
            //{
            //    nextArrow.SetActive(true);
            //    previousArrow.SetActive(true);
            //}
        }
    }

    public void ActivateReview()
    {
        review = true;
        count = 0;
        if (informacion.Count>0) {
            foreach (var item in informacion)
            {
                item.gameObject.SetActive(false);
            }

            informacion[0].gameObject.SetActive(true);
        }
        
    }
    public void NextQuestion()
    {
        _Audio.PlayAudio(0);
        if (informacion.Count > 0)
        {
            count++;


            if (count == informacion.Count)
            {

                ControlNavegacion.Forward();
                count = informacion.Count - 1;
            }
            else if (count < informacion.Count)
            {
                foreach (var item in informacion)
                {
                    item.gameObject.SetActive(false);
                }
                informacion[count].gameObject.SetActive(true);
            }
        }
        else {
            ControlNavegacion.Forward();
        }
       
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);
        if (informacion.Count > 0)
        {
            count--;
            if (count >= 0)
            {
                foreach (var item in informacion)
                {
                    item.gameObject.SetActive(false);
                }
                informacion[count].gameObject.SetActive(true);
            }
            if (count < 0)
            {
                count = 0;
                ControlNavegacion.Backward();
            }
        }
        else {
            ControlNavegacion.Backward();
        }
            
        

       
    }
}
