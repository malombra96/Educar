using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

public class L5A259_general : MonoBehaviour
{
    public List<L5A259_isla> islas;
    public bool op;
    public GameObject desempeño, aplico,aplico2d;
    public List<GameObject> ejercicios;
    public int co;
    public L5A259_barco barco;

    public bool review;
    public int count = 0;
    public List<GameObject> informacion,notas;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;

    // Start is called before the first frame update
    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        // review = false;
        //foreach (var item in informacion)
        //{
        //    item.gameObject.SetActive(false);
        //}
        //informacion[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {


        if (review)
        {
            if (count == 0)
            {
                previousArrow.SetActive(false);
            }

            if (count < informacion.Count - 1 && count > 0)
            {
                nextArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
        }

        else {
            if (op)
            {
                if (islas[0].habilitado == false && islas[1].habilitado == false && islas[2].habilitado == false)
                {
                    for (int i = 3; i < islas.Count; i++)
                    {
                        if (islas[i].op == true)
                        {
                            islas[i].habilitado = true;
                        }

                    }
                }

                co = 0;
                for (int i = 0; i < islas.Count; i++)
                {
                    if (islas[i].op == false)
                    {
                        co++;
                    }
                }

                if (co == ejercicios.Count)
                {
                    barco.gameObject.SetActive(false);
                    barco.mover = false;
                    aplico.SetActive(false);
                    desempeño.SetActive(true);
                    aplico2d.SetActive(false);
                    aplico2d.SetActive(false);

                }
            }
            else
            {
                barco.gameObject.SetActive(false);
                barco.mover = false;
                aplico.SetActive(false);
                desempeño.SetActive(true);
                aplico2d.SetActive(true);
                aplico2d.SetActive(false);
            }
        }
        
        
            
        
    }

    public void ActivateReview()
    {
        review = true;
        count = 0;
        foreach (var item in informacion)
        {
            item.SetActive(false);
        }

        informacion[0].SetActive(true);
    }
    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == informacion.Count)
        {
            barco.mover = false;
            aplico.SetActive(false);
            desempeño.SetActive(true);
            aplico2d.SetActive(false);
            barco.gameObject.SetActive(false);
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

    public void Revisar() {
        print("sSSD");
        aplico.SetActive(true);
        desempeño.SetActive(false);
        aplico2d.SetActive(true);
        op = false;
        barco.mover = false;
        barco.gameObject.SetActive(false);
        barco.mover = false;
        review = true;
        count = 0;
        foreach (var item in informacion)
        {
            item.SetActive(false);
        }

        foreach (var item in notas)
        {
            item.SetActive(false);
        }

        informacion[0].SetActive(true);

    }
    IEnumerator x() {
        yield return new WaitForSeconds(2f);
        aplico.SetActive(false);
        desempeño.SetActive(true);
    }

    public void ResetAll() {
        islas[0].habilitado = true;
        islas[1].habilitado = true;
        islas[2].habilitado = true;
        for (int i = 3; i < islas.Count; i++)
        {
            islas[i].habilitado = false;
            islas[i].op = true;
        }
    }

    public void pausa() {
        barco.mover = false;
    }

    public void resumen() {
        barco.mover = true;
    }

    public void terminar() {
        barco.mover = false;
        aplico.SetActive(false);
        desempeño.SetActive(true);
        aplico2d.SetActive(false);
        barco.gameObject.SetActive(false);
    }
    
}
