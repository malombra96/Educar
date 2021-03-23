using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class L6A277_general : MonoBehaviour
{
    public List<GameObject> niveles,planetas;
    public float errores = -40;
    public GameObject nave,fondo;
    public int contador, vidas;
    public Vector2 inicial;
    public bool review;
    public int count = 0;
    public GameObject nextArrow, previousArrow,mal,bien,normal;
    public ControlNavegacion control;
    [SerializeField] ControlAudio _Audio;
    // Start is called before the first frame update
    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        planetas[0].SetActive(true);
    }

    void Update()
    {
        if (review)
        {
            if (count == 0)
            {
                previousArrow.SetActive(false);
            }
            if ( count > 0)
            {
                nextArrow.SetActive(true); previousArrow.SetActive(true);
            }
        }
    }

    public void ResetAll() {
        foreach (var item in niveles)
        {
            item.SetActive(false);
            item.GetComponent<L6A277_nivel>().reset = false;
            item.GetComponent<L6A277_nivel>().seleccionar.ResetSeleccionarToggle();
        }
        contador = 0;
        nave.GetComponent<RectTransform>().anchoredPosition = inicial;
        vidas = 11;
        bien.SetActive(false);
        mal.SetActive(false);
        normal.SetActive(false);
        foreach (var item in planetas)
        {
            item.SetActive(false);
        }
        planetas[0].SetActive(true);
        fondo.SetActive(false);
    }

    public void ActivateReview()
    {
        review = true;
        count = 0;
        foreach (var item in niveles)
        {
            item.SetActive(false);
            item.GetComponent<L6A277_nivel>().review = true;
        }

        niveles[0].SetActive(true);
        fondo.SetActive(false);
    }
    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == niveles.Count)
        {
            control.Forward();
        }
        else if (count <= niveles.Count)
        {
            foreach (var item in niveles)
            {
                item.SetActive(false);
            }
            niveles[count].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        if (count > 0)
        {
            count--;
            foreach (var item in niveles)
            {
                item.SetActive(false);
            }
            niveles[count].SetActive(true);
        }
    }

    public void Calificar(bool value) {
        StartCoroutine(y(value));
    }


    IEnumerator y(bool value)
    {
        yield return new WaitForSeconds(2f);
        niveles[contador].SetActive(false);
        planetas[contador].SetActive(false);
        if (!value)
        {
            nave.GetComponent<RectTransform>().anchoredPosition = new Vector2(nave.GetComponent<RectTransform>().anchoredPosition.x, nave.GetComponent<RectTransform>().anchoredPosition.y + errores);
            vidas--;
        }
        contador++;
        if (contador < niveles.Count)
        {
            planetas[contador].SetActive(true);
        }
            StartCoroutine(Siguiente());
    }

    IEnumerator Siguiente() {
        yield return new WaitForSeconds(1.5f);
        if (contador <niveles.Count) {
            
            niveles[contador].SetActive(true);
        }
        if (contador == niveles.Count) {
            if (vidas == niveles.Count) {
                bien.SetActive(true);
            }
            if (vidas < niveles.Count -1 && vidas > niveles.Count /2) {
                normal.SetActive(true);
            }
            if (vidas <= niveles.Count /2) {
                mal.SetActive(true);
            }
            control.Forward(4f);
        }
    }

    public void Iniciar() {

        fondo.SetActive(true);
        StartCoroutine(x());
    }

    IEnumerator x() {
        yield return new WaitForSeconds(1f);
        niveles[contador].SetActive(true);
        planetas[contador].SetActive(true);
    }
}
