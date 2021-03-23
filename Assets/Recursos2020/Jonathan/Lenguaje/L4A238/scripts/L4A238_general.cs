using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A238_general : MonoBehaviour
{
    public GameObject player, mapa;
    public List<GameObject> numeros;
    public int count, countReview;
    public List<GameObject> elementos,iniciarelementos;
    public ControlNavegacion controlNavegacion;
    public GameObject nextArrow, previousArrow, iniciar;
    public bool review, first, move;
    public ControlPuntaje controlPuntaje;
    public ControlAudio controlAudio;
    public GameObject incorrecto, correcto;
    public float speed;
    public Vector2 ini;

    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        iniciar.GetComponent<Button>().onClick.AddListener(inicio);
        previousArrow.SetActive(false);
        if (!review) {
            ini = player.GetComponent<RectTransform>().anchoredPosition;
            first = true;
            
            foreach (var item in elementos)
            {
                item.gameObject.SetActive(false);
            }
            incorrecto.SetActive(false);
            correcto.SetActive(false);

        }

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
        else
        {
            if (move)
            {
                if (count < elementos.Count)
                {
                    float step = speed * Time.deltaTime;
                    player.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(player.GetComponent<RectTransform>().anchoredPosition, numeros[count-1].GetComponent<RectTransform>().anchoredPosition, step);
                    if (player.GetComponent<RectTransform>().anchoredPosition == numeros[count-1].GetComponent<RectTransform>().anchoredPosition) {
                        move = false;
                    }
                }
            }
        }
    }

    public void inicio()
    {
        controlAudio.PlayAudio(0);
        mapa.SetActive(true);
        StartCoroutine(m());
    }

    IEnumerator m()
    {
        yield return new WaitForSeconds(1f);
        numeros[0].GetComponent<Image>().sprite = numeros[0].GetComponent<BehaviourSprite>()._selection;
        yield return new WaitForSeconds(0.5f);
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
        
        incorrecto.SetActive(false);
        correcto.SetActive(false);
        foreach (var item in iniciarelementos)
        {
            item.gameObject.SetActive(false);
        }
        elementos[countReview].SetActive(true);
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
        if (count == elementos.Count)
        {
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
            move = true;
            player.GetComponent<Animator>().SetBool("x", true);
            numeros[count].GetComponent<Image>().sprite = numeros[count].GetComponent<BehaviourSprite>()._selection;
            yield return new WaitForSeconds(1f);
            elementos[count].SetActive(true);
        }
        if (count == elementos.Count)
        {
            player.GetComponent<Animator>().SetBool("x", false);
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
            for (int i = 0; i < elementos.Count; i++)
            {
                if (elementos[i].GetComponent<L4A238_managerDrag>())
                {
                    elementos[i].GetComponent<L4A238_managerDrag>().ResetDragDrop();
                }
                if (elementos[i].GetComponent<L4A238_managerDraw>())
                {
                    elementos[i].GetComponent<L4A238_managerDraw>().ResetLines();
                }
            }
            foreach (var item in elementos)
            {
                item.SetActive(false);
            }
            count = 0;
            incorrecto.SetActive(false);
            correcto.SetActive(false);
            player.GetComponent<RectTransform>().anchoredPosition = ini;
            player.GetComponent<Animator>().SetBool("x", false);
            foreach (var item in numeros)
            {
                item.GetComponent<Image>().sprite = item.GetComponent<BehaviourSprite>()._default;
            }
            mapa.SetActive(false);
        }
    }
}
