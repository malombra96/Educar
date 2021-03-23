using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A235_managerGeneral : MonoBehaviour
{
    public Button inicio;
    public Toggle luna, sol;
    public List<GameObject> ejercicios;
    public int contador,contadorEstrellas,contadorReview;
    public GameObject estrellas,personaje;
    public ControlNavegacion controlNavegacion;
    public ControlAudio controlAudio;
    public bool review;
    public GameObject nextArrow, previousArrow;

    void Start()
    {
        inicio.onClick.AddListener(Iniciar);
        luna.onValueChanged.AddListener(delegate { Seleccionar(luna); });
        sol.onValueChanged.AddListener(delegate { Seleccionar(sol); });
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        Init();
    }

    public void ActivateReview()
    {
        review = true;
        contadorReview = 0;
        foreach (var item in ejercicios)
        {
            item.SetActive(false);
        }

        ejercicios[0].SetActive(true);
        gameObject.GetComponent<Image>().enabled = false;
        

    }

    private void Update()
    {
        if (!luna.isOn && !sol.isOn) {
            inicio.interactable = false;
        }

        if (review)
        {
            if (contadorReview == 0)
            {
                previousArrow.SetActive(false);
            }
            if (contadorReview == ejercicios.Count)
            {
                nextArrow.SetActive(false);
            }
            if (contadorReview < ejercicios.Count && contadorReview > 0)
            {
                nextArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
        }

    }

    public void NextQuestion()
    {
        controlAudio.PlayAudio(0);
        contadorReview++;
        if (contadorReview == ejercicios.Count)
        {
            controlNavegacion.Forward();
        }
        else if (contadorReview <= ejercicios.Count)
        {
            foreach (var item in ejercicios)
            {
                item.SetActive(false);
            }
            ejercicios[contadorReview].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        controlAudio.PlayAudio(0);

        if (contadorReview > 0)
        {
            contadorReview--;
            foreach (var item in ejercicios)
            {
                item.SetActive(false);
            }
            ejercicios[contadorReview].SetActive(true);
        }
    }
    public void Init() {
        foreach (var e in ejercicios)
        {
            e.SetActive(false);
        }
        contador = 0;
        inicio.interactable = false;
        for (int i = 0; i < estrellas.transform.childCount; i++)
        {
            estrellas.transform.GetChild(i).GetComponent<Image>().sprite = estrellas.transform.GetChild(i).GetComponent<BehaviourSprite>()._default;
        }
        contadorEstrellas = estrellas.transform.childCount;
        gameObject.GetComponent<Image>().enabled = true;
        //personaje.SetActive(false);
        estrellas.SetActive(false);
        luna.interactable = true;
        sol.interactable = true;
    }

    public void Iniciar() {
        
        inicio.interactable = false;
        luna.interactable = false;
        sol.interactable = false;
        ejercicios[0].SetActive(true);
        gameObject.GetComponent<Image>().enabled = false;
        personaje.SetActive(true);
        estrellas.SetActive(true);
    }

    public void Seleccionar(Toggle t) {
        controlAudio.PlayAudio(0);
        if (t.isOn)
        {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._selection;
            inicio.interactable = true;
        }
        else {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
            inicio.interactable = false;
        }
    }

    public void CalificarEjercicio(bool value) {
        if (value) {
            if (contadorEstrellas > 0) {
                
                estrellas.transform.GetChild(contadorEstrellas-1).gameObject.GetComponent<Image>().sprite = estrellas.transform.GetChild(contadorEstrellas-1).gameObject.GetComponent<BehaviourSprite>()._selection;
                contadorEstrellas--;
            }
        }
        StartCoroutine(x());
    }

    IEnumerator x() {
        yield return new WaitForSeconds(2f);
        ejercicios[contador].SetActive(false);
        contador++;
        if (contador < ejercicios.Count) {
            ejercicios[contador].SetActive(true);
        }
        if (contador == ejercicios.Count)
        {
            //gameObject.GetComponent<Image>().enabled = true;
            ejercicios[contador-1].SetActive(true);
            controlNavegacion.Forward();
        }
    }

    public void ResetAll() {
        Init();
        for (int i = 0; i<ejercicios.Count;i++) {
            if (ejercicios[i].GetComponent<L4A235_managerSeleccionar>()) {
                ejercicios[i].GetComponent<L4A235_managerSeleccionar>().ResetSeleccionarToggle();
            }
            if (ejercicios[i].GetComponent<L4A235_managerDrag>())
            {
                ejercicios[i].GetComponent<L4A235_managerDrag>().ResetDragDrop();
            }
        }
        luna.isOn = false;
        sol.isOn = false;
    }
}
