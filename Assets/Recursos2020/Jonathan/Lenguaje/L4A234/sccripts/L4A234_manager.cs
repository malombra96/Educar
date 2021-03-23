using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A234_manager : MonoBehaviour
{
    public Button botonPresente, botonImperfecto, botonPerfecto, botonFuturo, botonIniciar;
    public ControlAudio controlAudio;
    public ControlNavegacion controlNavegacion;
    public ControlPuntaje controlPuntaje;
    public GameObject palabraActual;
    public List<GameObject> palabras;
    public int contador;
    public bool bandera;
    public bool review;
    public int count = 0;
    public GameObject nextArrow, previousArrow;

    // Start is called before the first frame update
    void Start()
    {
        botonFuturo.onClick.AddListener(delegate { SeleccionarPalabra("futuro"); });
        botonImperfecto.onClick.AddListener(delegate { SeleccionarPalabra("imperfecto"); });
        botonPerfecto.onClick.AddListener(delegate { SeleccionarPalabra("perfecto"); });
        botonPresente.onClick.AddListener(delegate { SeleccionarPalabra("presente"); });
        botonIniciar.onClick.AddListener(delegate { IniciarJuego(); });
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        bandera = true;

        if (!review) {
            Inicio();
            shuffle();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (review)
        {
            if (count == 0)
            {
                previousArrow.SetActive(false);
                nextArrow.SetActive(true);
            }
            if (count == palabras.Count)
            {
                nextArrow.SetActive(false);
                previousArrow.SetActive(true);
            }
            if (count < palabras.Count  && count > 0)
            {
                nextArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
        }
    }

    public void shuffle() {
        for (int i = 0; i < palabras.Count; i++)
        {
            GameObject temp = palabras[i];
            int randomIndex = Random.Range(i, palabras.Count);
            palabras[i] = palabras[randomIndex];
            palabras[randomIndex] = temp;
        }
    }

    public void ActivateReview()
    {
        review = true;
        count = 0;
        foreach (var item in palabras)
        {
            item.GetComponent<Animator>().SetInteger("x", 0);
            item.GetComponent<Animator>().enabled = false;
            item.transform.localScale = Vector3.one;
            item.SetActive(false);
        }


        palabras[0].SetActive(true);
        palabras[0].GetComponent<Animator>().SetInteger("x", 0);
        palabras[0].GetComponent<Animator>().enabled = false;
        palabras[0].transform.localScale = Vector3.one;
        
    }
    public void NextQuestion()
    {
        count++;

        controlAudio.PlayAudio(0);
        if (count == palabras.Count)
        {
            controlNavegacion.Forward();
        }
        else if (count <= palabras.Count)
        {
            foreach (var item in palabras)
            {
                item.SetActive(false);
            }
            palabras[count].SetActive(true);
            
        }
    }
    public void PreviousQuestion()
    {
        controlAudio.PlayAudio(0);

        if (count > 0)
        {
            count--;
            foreach (var item in palabras)
            {
                item.SetActive(false);
            }
            palabras[count].SetActive(true);
        }
    }

    public void Inicio() {

        contador = 0;
        palabraActual = null;
        foreach (var p in palabras)
        {
            p.GetComponent<Animator>().SetInteger("x", 0);
            p.SetActive(false);
        }
        controlPuntaje.resetScore();
        botonFuturo.interactable = false;
        botonImperfecto.interactable = false;
        botonPerfecto.interactable = false;
        botonPresente.interactable = false;


        botonIniciar.interactable = true;
        botonIniciar.gameObject.SetActive(true);
    }

    public void IniciarJuego() {
        botonIniciar.gameObject.SetActive(false);
        botonIniciar.interactable = false;
        controlAudio.PlayAudio(0);
        palabras[contador].SetActive(true);
        palabraActual = palabras[0];
        botonFuturo.interactable = true;
        botonImperfecto.interactable = true;
        botonPerfecto.interactable = true;
        botonPresente.interactable = true;
    }

    public void SeleccionarPalabra(string boton) {
        controlAudio.PlayAudio(0);
        botonFuturo.interactable = false;
        botonImperfecto.interactable = false;
        botonPerfecto.interactable = false;
        botonPresente.interactable = false;
        if (palabraActual != null)
        {

            switch (boton) {
                case "presente":
                    if (palabraActual.GetComponent<L4A234_palabra>().presente && boton.Contains("presente"))
                    {
                        
                        controlPuntaje.IncreaseScore();
                        StartCoroutine(x(true));
                    }
                    else {
                        
                        StartCoroutine(x(false));
                    }
                    break;
                case "futuro":
                    if (palabraActual.GetComponent<L4A234_palabra>().futuro && boton.Contains("futuro"))
                    {
                        
                        controlPuntaje.IncreaseScore();
                        StartCoroutine(x(true));
                    }
                    else
                    {
                        
                        StartCoroutine(x(false));
                    }
                    break;
                case "imperfecto":
                    if (palabraActual.GetComponent<L4A234_palabra>().imperfecto && boton.Contains("imperfecto"))
                    {
                        
                        controlPuntaje.IncreaseScore();
                        StartCoroutine(x(true));
                    }
                    else
                    {
                        
                        StartCoroutine(x(false));
                    }
                    break;
                case "perfecto":
                    if (palabraActual.GetComponent<L4A234_palabra>().perfecto && boton.Contains("perfecto"))
                    {
                        
                        controlPuntaje.IncreaseScore();
                        StartCoroutine(x(true));
                    }
                    else
                    {
                        
                        StartCoroutine(x(false));
                    }
                    break;
            }
        }
    }

    IEnumerator x(bool value) {
        if (value) {
            controlAudio.PlayAudio(1);
            palabraActual.GetComponent<Animator>().SetInteger("x", 1);
            yield return new WaitForSeconds(0.5f);
            //palabraActual.GetComponent<Animator>().SetInteger("x", 0);
           // palabraActual.GetComponent<Image>().sprite = palabraActual.GetComponent<L4A234_palabra>().correcto;
        }
        else {
            controlAudio.PlayAudio(2);
            palabraActual.GetComponent<Animator>().SetInteger("x", 2);
            yield return new WaitForSeconds(0.5f);
            //palabraActual.GetComponent<Animator>().SetInteger("x", 0);
           // palabraActual.GetComponent<Image>().sprite = palabraActual.GetComponent<L4A234_palabra>().incorrecto;
        }
        
        yield return new WaitForSeconds(2);
        palabras[contador].SetActive(false);
        palabraActual = null;
        contador++;
        if (contador < palabras.Count)
        {
            palabras[contador].SetActive(true);
            palabraActual = palabras[contador];
            botonFuturo.interactable = true;
            botonImperfecto.interactable = true;
            botonPerfecto.interactable = true;
            botonPresente.interactable = true;
        }

        if (contador == palabras.Count) {
            controlNavegacion.Forward();
        }
    }

    public void ResetAll() {
        if (bandera) {
            Inicio();
            shuffle();
            foreach (var p in palabras)
            {
                p.GetComponent<Image>().enabled = true;
                p.GetComponent<Image>().sprite = p.GetComponent<L4A234_palabra>().original;
                p.GetComponent<Image>().SetNativeSize();
                p.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
