using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L6A275_general : MonoBehaviour
{
    public GameObject enunciado;
    public Toggle toggleWoman, toggleMan;
    public int correctas;
    public Button botonIinicar;
    public string personaje;
    public List<GameObject> elementos;
    public bool review,m,w;
    public L6A275_resultado resultado;

    public int count = 0;
    public GameObject nextArrow, previousArrow;
    [SerializeField] ControlAudio _Audio;
    public ControlNavegacion ControlNavegacion;


    // Start is called before the first frame update
    void Start()
    {
        toggleMan.onValueChanged.AddListener(delegate { Cambiar(toggleMan); });
        toggleWoman.onValueChanged.AddListener(delegate { Cambiar(toggleWoman); });
        botonIinicar.onClick.AddListener(Iniciar);
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (personaje != "")
        {
            botonIinicar.interactable = true;
        }
        else {
            botonIinicar.interactable = false;
        }


        if (review)
        {
            if (count == 0)
            {
                previousArrow.SetActive(false);
            }
    
            if (count < elementos.Count && count > 0)
            {
                nextArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
        }

    }

    public void ActivateReview()
    {
        review = true;
        count = 0;
        resultado.gameObject.SetActive(false);
        foreach (var item in elementos)
        {
            item.SetActive(false);
        }

        elementos[0].SetActive(true);
    }
    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == elementos.Count)
        {
            ControlNavegacion.Forward();
        }
        else if (count <= elementos.Count)
        {
            foreach (var item in elementos)
            {
                item.SetActive(false);
            }
            elementos[count].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        if (count > 0)
        {
            count--;
            foreach (var item in elementos)
            {
                item.SetActive(false);
            }
            elementos[count].SetActive(true);
        }
    }




    public void Cambiar(Toggle t) {
        _Audio.PlayAudio(0);
        if (t.name == "ToggleMan")
        {
            m = true;
            w = false;
            resultado.m = true;
            resultado.w = false;
        }

        if (t.name == "ToggleWoman")
        {
            m = false;
            w = true;
            resultado.m = false;
            resultado.w = true;
        }

        if (t.isOn)
        {
            personaje = t.gameObject.name;
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._selection;
        }
        else {
            personaje = "";
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
        }
    }

    public void Iniciar() {
        _Audio.PlayAudio(0);
        foreach (var e in elementos) {
            e.SetActive(false);
        }
        elementos[0].SetActive(true);
    }

    public void ResetAll() {
        foreach (var e in elementos)
        {
            e.SetActive(false);
            e.GetComponent<L6A275_managerSeleccionar>().ResetSeleccionarToggle();
            
        }
        personaje = "";
        m = false;
        w = false;
        toggleMan.isOn = false;
        toggleWoman.isOn = false;
        resultado.m = false;
        resultado.w = false;
        enunciado.SetActive(true);
        toggleMan.gameObject.SetActive(true);
        toggleWoman.gameObject.SetActive(true);
    }
}

