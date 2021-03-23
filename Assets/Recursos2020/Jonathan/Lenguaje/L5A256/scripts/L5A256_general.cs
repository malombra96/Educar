using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A256_general : MonoBehaviour
{
    public GameObject ImagenInicio, Cielo,hombre,mujer,montana;
    public Toggle botonHombre, botonMujer;
    public Button botonEmpezar;
    public ControlAudio controlAudio;
    public ControlNavegacion ControlNavegacion;
    public ControlPuntaje ControlPuntaje;
    public int avatar;
    public Vector2 montanaIni, personajeIni;
    public bool moveBg,first;
    public float speed;
    public List<L5A256_managerDrag> elementos;
    public int c,b;
    public GameObject life;
    public bool review;
    public int count = 0;
    public GameObject nextArrow, previousArrow;
    public GameObject mujer1, mujer2, hombre1, hombre2,correcto,incorrecto;

    // Start is called before the first frame update
    void Start()
    {
        botonHombre.onValueChanged.AddListener(delegate { Seleccionar(1,botonHombre); });
        botonMujer.onValueChanged.AddListener(delegate { Seleccionar(2,botonMujer); });
        botonEmpezar.onClick.AddListener(Comenzar);
        iniciar();
        first = true;
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        // review = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (avatar == 0)
        {
            botonEmpezar.interactable = false;
        }
        else {
            botonEmpezar.interactable = true;
        }

        if (moveBg)
        {
            if (avatar == 1) {
                hombre.GetComponent<Animator>().SetBool("x", true);
                StartCoroutine(x(1));
            }
            if (avatar == 2) {
                mujer.GetComponent<Animator>().SetBool("x", true);
                StartCoroutine(x(2));
            }
            
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
        correcto.SetActive(false);
        incorrecto.SetActive(false);
        ImagenInicio.SetActive(false);
        Cielo.SetActive(true);
        review = true;
        count = 0;
        foreach (var item in elementos)
        {
            item.gameObject.SetActive(false);
        }

        elementos[0].gameObject.SetActive(true);
    }
    public void NextQuestion()
    {
        count++;

        if (count == elementos.Count)
        {
            ControlNavegacion.Forward();
        }
        else if (count <= elementos.Count)
        {
            foreach (var item in elementos)
            {
                item.gameObject.SetActive(false);
            }
            elementos[count].gameObject.SetActive(true);
        }
    }
    public void PreviousQuestion()
    { 

        if (count > 0)
        {
            count--;
            foreach (var item in elementos)
            {
                item.gameObject.SetActive(false);
            }
            elementos[count].gameObject. SetActive(true);
        }
    }

    IEnumerator x(int i)
    {
        if (i == 1) {
           montana.GetComponent<RectTransform>().anchoredPosition += Vector2.down* Time.deltaTime * speed;
            //hombre.GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime * speed;
            yield return new WaitForSeconds(3);
            moveBg = false;
            hombre.GetComponent<Animator>().SetBool("x", false);
        }
        if (i == 2) {
            montana.GetComponent<RectTransform>().anchoredPosition += Vector2.down * Time.deltaTime * speed;
           // mujer.GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime * speed;
            yield return new WaitForSeconds(3);
            moveBg = false;
            mujer.GetComponent<Animator>().SetBool("x", false);
        }
       
    }


    public void iniciar() {
        if (!review) {
            b = 0;
            ImagenInicio.SetActive(true);
            montana.GetComponent<RectTransform>().anchoredPosition = montanaIni;
            hombre.GetComponent<RectTransform>().anchoredPosition = personajeIni;
            mujer.GetComponent<RectTransform>().anchoredPosition = personajeIni;
            Cielo.SetActive(false);
            hombre.SetActive(false);
            mujer.SetActive(false);
            hombre.GetComponent<Animator>().SetBool("x", false);
            mujer.GetComponent<Animator>().SetBool("x", false);
            avatar = 0;
            moveBg = false;
            botonHombre.isOn = false;
            botonMujer.isOn = false;
            c = 0;
            foreach (var e in elementos)
            {
                e.gameObject.SetActive(false);
            }

            for (int i = 0; i < life.transform.childCount; i++)
            {
                life.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = life.transform.GetChild(i).gameObject.GetComponent<BehaviourSprite>()._default;
            }
        }
      
    }

    public void Seleccionar(int i,Toggle t) {
        controlAudio.PlayAudio(0);
        if (t.isOn)
        {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._selection;
            if (i == 1)
            {
                avatar = 1;
                hombre.SetActive(true);
                hombre1.SetActive(true);
                hombre2.SetActive(true);
            }
            if (i == 2)
            {
                avatar = 2;
                mujer.SetActive(true);
                mujer1.SetActive(true);
                mujer2.SetActive(true);
            }
        }
        else {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
            avatar = 0;
        }
    }

    public void pregunta(bool value) {
        if (value) {
            life.transform.GetChild(b).GetComponent<Image>().sprite = life.transform.GetChild(b).GetComponent<BehaviourSprite>()._selection;
            b++;
            moveBg = true;
        }
        StartCoroutine(y());
        
    }

    public IEnumerator y() {
        yield return new WaitForSeconds(2);
        if (c < elementos.Count)
        {
            elementos[c].gameObject.SetActive(false);
            c++;
            if (c == elementos.Count) {
                //                ControlNavegacion.Forward();
                if (ControlPuntaje._rightAnswers == ControlPuntaje.questions) {
                    correcto.SetActive(true);
                }
                else {
                    incorrecto.SetActive(true);
                }
                ControlNavegacion.Forward(1f);
            }
            else {
                elementos[c].gameObject.SetActive(true);
            }
            
        }

    } 


    public void Comenzar() {
        controlAudio.PlayAudio(0);
        if (avatar == 1)
        {
            hombre.SetActive(true);
        }
        if (avatar == 2){
            mujer.SetActive(true);
        }
        ImagenInicio.SetActive(false);
        Cielo.SetActive(true);
        elementos[0].gameObject.SetActive(true);
    }

    public void ResetAll() {
        if (first) {
            iniciar();
            foreach (var e in elementos)
            {
                e.ResetDragDrop();
                e.gameObject.SetActive(false);
            }
        }
    }
}


