using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L7A281_managerGeneral : MonoBehaviour
{
    
    
    public int count,countReview;
    public List<GameObject> elementos;
    public ControlNavegacion controlNavegacion;
    public GameObject nextArrow, previousArrow,iniciar;
    public bool review, first;
    public ControlPuntaje controlPuntaje;
    public ControlAudio controlAudio;
    public GameObject incorrecto, correcto;

    void Start()
    {
        first = true;
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        iniciar.GetComponent<Button>().onClick.AddListener(inicio);
        previousArrow.SetActive(false);
        foreach (var item in elementos)
        {
            item.gameObject.SetActive(false);
        }
        incorrecto.SetActive(false);
        correcto.SetActive(false);

    }

    void Update()
    {
        if (review)
        {
            if (countReview == 0)
            {
                previousArrow.SetActive(false);
            }
            if (countReview == elementos.Count)
            {
                nextArrow.SetActive(false);
                previousArrow.SetActive(true);
            }
            if (countReview < elementos.Count && countReview > 0)
            {
                nextArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
        }
    }

    public void inicio() {
        controlAudio.PlayAudio(0);
        elementos[0].SetActive(true);
    }

    public void ActivateReview()
    {
        review = true;
        countReview = 0;
        foreach (var item in elementos)
        {
            item.SetActive(false);
        }
        elementos[0].SetActive(true);
        incorrecto.SetActive(false);
        correcto.SetActive(false);
    }
    public void NextQuestion()
    {
        countReview++;
        if (countReview == elementos.Count)
        {
            controlNavegacion.Forward();
        }
        else if (countReview <= elementos.Count)
        {
            foreach (var item in elementos)
            {
                item.SetActive(false);
            }
            elementos[countReview].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        if (countReview > 0)
        {
            countReview--;
            foreach (var item in elementos)
            {
                item.SetActive(false);
            }
            elementos[countReview].SetActive(true);
        }
    }

    public void NextExcersise()
    {
        if (count < elementos.Count)
        {
            StartCoroutine(x());
        }
        if (count == elementos.Count) {
            controlNavegacion.Forward(2f);
        }
    }
    IEnumerator x()
    {
        yield return new WaitForSeconds(1f);
        elementos[count].SetActive(false);
        count++;
        if (count < elementos.Count)
        {
            elementos[count].SetActive(true);
        }
        if (count == elementos.Count)
        {

            if (controlPuntaje._rightAnswers == controlPuntaje.questions)
            {
                correcto.SetActive(true);
                controlNavegacion.Forward(2f);
            }
            else
            {
                incorrecto.SetActive(true);
                controlNavegacion.Forward(2f);
            }

        }

    }
    public void ResetAll()
    {
        if (first)
        {
            for (int i = 0; i < elementos.Count; i++) {
                if (elementos[i].GetComponent<L7A281_managerInput>()) {
                    elementos[i].GetComponent<L7A281_managerInput>().resetAll();
                }
                if (elementos[i].GetComponent<L7A281_managerSeleccionar>())
                {
                    elementos[i].GetComponent<L7A281_managerSeleccionar>().ResetSeleccionarToggle();
                }
            }
            foreach (var item in elementos)
            {
                item.SetActive(false);
            }
            count = 0;
            incorrecto.SetActive(false);
            correcto.SetActive(false);
        }
    }
}
