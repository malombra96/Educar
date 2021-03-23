using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A262_general : MonoBehaviour
{
    public Toggle man, woman;
    public List<GameObject> hombres, mujeres;
    public string cadena;
    public Button iniciar;
    public List<GameObject> elementos;
    public GameObject fin,buena,mala;
    public int correctas;

    public bool review;
    public int count = 0;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;
    public ControlNavegacion ControlNavegacion;
    // Start is called before the first frame update
    void Start()
    {
        man.onValueChanged.AddListener(delegate { cambiar(man); });
        woman.onValueChanged.AddListener(delegate { cambiar(woman); });
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        iniciar.onClick.AddListener(Iniciar);
    }

    // Update is called once per frame
    void Update()
    {
        if (cadena == "")
        {
            iniciar.interactable = false;
        }
        else 
        {
            iniciar.interactable = true;
        }

        if (review)
        {
          
            if (count > 0)
            {
                previousArrow.SetActive(true);
            }
        }
    }

    public void ResetAll() {
        fin.SetActive(false);
        foreach (var item in elementos)
        {
            if (item.GetComponent<L5A262_managerSeleccionar>()) {
                item.GetComponent<L5A262_managerSeleccionar>().ResetSeleccionarToggle();
            }
            if (item.GetComponent<L5A262_managerDrag>()) {
                item.GetComponent<L5A262_managerDrag>().ResetDragDrop();
            }
            item.SetActive(false);
        }

        cadena = "";
        correctas = 0;
        mala.SetActive(false);
        buena.SetActive(false);
    }

    public void ActivateReview()
    {
        review = true;
        count = 0;
        foreach (var item in elementos)
        {
            item.SetActive(false);
        }

        elementos[0].SetActive(true);
        fin.SetActive(false);
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

    public void cambiar(Toggle t) {
        _Audio.PlayAudio(0);
        if (t.isOn)
        {
            cadena = t.name;

            if (cadena == "Man")
            {
                foreach (var h in hombres) {
                    h.SetActive(true);
                }

                foreach (var m in mujeres)
                {
                    m.SetActive(false);
                }
            }
            if (cadena == "Woman") 
            {
                foreach (var h in hombres)
                {
                    h.SetActive(false);
                }

                foreach (var m in mujeres)
                {
                    m.SetActive(true);
                }
            }
        }
        else {
            cadena = null;
        }
    }

    public void Iniciar() {
        _Audio.PlayAudio(0);
        foreach (var e in elementos) {
            e.SetActive(false);
        }

        elementos[0].SetActive(true);
    }

    public void Calificar(bool value,bool x) {
        if (value)
        {
            correctas++;
            if (x)
            {
                StartCoroutine(y());
            }
        }
        else {
            if (x)
            {
                StartCoroutine(y());
            }
        }
        
    }

    IEnumerator y() {
        yield return new WaitForSeconds(2f);
        fin.SetActive(true);
        if (correctas == 4)
        {
            buena.SetActive(true);
            mala.SetActive(false);
        }
        else
        {
            buena.SetActive(false);
            mala.SetActive(true);
        }
        ControlNavegacion.Forward(2f);
    }
}
