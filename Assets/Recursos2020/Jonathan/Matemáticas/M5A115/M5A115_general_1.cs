using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M5A115_general_1 : MonoBehaviour
{
    public GameObject vida;
    public List<GameObject> fichasOut, fichasIn;
    public int vidas,fichas;
    public ControlNavegacion ControlNavegacion;
    public ControlPuntaje ControlPuntaje;
    public GameObject imagenCorrecta, imagenIncorrecta;

    public bool review;
    public int count = 0;
    public List<GameObject> informacion;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;

    private void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
       

    }

    void Update()
    {
        if (review)
        {
            if (count == 0)
            {
                previousArrow.SetActive(false);
            }
            if (count == informacion.Count - 1)
            {
                nextArrow.SetActive(true);
            }
            if (count> 0) {
                previousArrow.SetActive(true);
            }
        }
    }

    public void ActivateReview()
    {
        previousArrow.SetActive(false);
        review = true;
        count = 0;
        foreach (var item in informacion)
        {
            item.SetActive(false);
        }

        informacion[0].SetActive(true);
        imagenCorrecta.SetActive(false);
        imagenIncorrecta.SetActive(false);
    }

    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == informacion.Count)
        {
            
            ControlNavegacion.Forward();
            count = informacion.Count-1;
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

    public void RestarVidas() {
        vida.transform.GetChild(vidas).gameObject.SetActive(false);
        vidas++;
        if (vidas == vida.transform.childCount) {
            foreach (var f in fichasIn) {
                f.SetActive(false);
            }
            imagenIncorrecta.SetActive(true);
            ControlNavegacion.Forward(2f);
        }
    }

    public void intentos() {
        fichas++;
        if (fichas == fichasOut.Count) {
            foreach (var f in fichasOut)
            {
                f.SetActive(false);
            }
            imagenCorrecta.SetActive(true);
            ControlNavegacion.Forward(2f);
        }
    }

    public void ResetAll() {
        vida.transform.GetChild(0).gameObject.SetActive(true);
        vida.transform.GetChild(1).gameObject.SetActive(true);
        vida.transform.GetChild(2).gameObject.SetActive(true);
        vidas = 0;
        fichas = 0;
        imagenCorrecta.SetActive(false);
        imagenIncorrecta.SetActive(false);
        foreach (var f in fichasIn)
        {
            f.SetActive(false);
        }

        foreach (var fi in informacion)
        {
            fi.SetActive(false);
        }

        foreach (var f in fichasOut)
        {
            f.SetActive(true);
            f.GetComponent<Button>().interactable = true;
            f.GetComponent<M5A115_ficha>().pregunta1 = false;
            f.GetComponent<M5A115_ficha>().pregunta2 = false;
            f.GetComponent<M5A115_ficha>().calificado = false;
            f.GetComponent<M5A115_ficha>().seleccionar.GetComponent<M5A115_managerSeleccionar>().ResetSeleccionarToggle();
            f.GetComponent<M5A115_ficha>().input.GetComponent<M5A115_managerInput>().resetAll();
        }
    }
}

