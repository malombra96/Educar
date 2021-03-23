using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class M7A76ControlCartas : MonoBehaviour
{
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;
    ControlAudio controlAudio;

    GameObject cartaTemporal1;
    GameObject cartaTemporal2;
    int Numero_Cartas, Aciertos;

    [HideInInspector] public bool mover, random, resvision;
    Text Tiempo;
    int minutos = 2, segundos = 00;

    //random posicion
    public List<GameObject> cartas;    
    private GameObject randomTemp;
    private Vector3 targetTemp;
    private int statePos, placeTemp;
    
    void Awake()
    {
        mover = true;
        random = true;
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlAudio = FindObjectOfType<ControlAudio>();

        for (int x = 0; x < transform.childCount; x++)
        {
            if (transform.GetChild(x).GetComponent<M7A76CartaSeleccionada>())
            {
                transform.GetChild(x).GetComponent<M7A76CartaSeleccionada>().controlCartas = this;
                cartas.Add(transform.GetChild(x).gameObject);
            }
            else
                Tiempo = transform.GetChild(x).transform.GetChild(0).GetComponent<Text>();
        }

        
    }
    void OnEnable()
    {
        if (random && !resvision)
        {
            for (int x = 0; x < transform.childCount; x++)
            {
                if (transform.GetChild(x).GetComponent<M7A76CartaSeleccionada>())
                    StartCoroutine(transform.GetChild(x).GetComponent<M7A76CartaSeleccionada>().tiempoCambio(5));
            }
            random = false;
            StartCoroutine(temporizador(7));
            StartCoroutine(RandomPosion());
        }
    }
    IEnumerator temporizador(float t)
    {
        yield return new WaitForSeconds(t);
        
        if (segundos == 00)
        {
            if (minutos <= 0)
            {
                mover = false;
                controlNavegacion.Forward(2);
            }
            else
            {
                minutos--;
                segundos = 59;
            }
        }
        else
            segundos--;

        if (segundos < 10)
        {
            Tiempo.text = Convert.ToString(minutos) + ":" + "0" + Convert.ToString(segundos);
            if(minutos == 0)
                Tiempo.text = "0" + Convert.ToString(segundos);
        }
        else if (minutos > 0)
            Tiempo.text = Convert.ToString(minutos) + ":" + Convert.ToString(segundos);
        else
            Tiempo.text = Convert.ToString(segundos);

        Tiempo.GetComponent<AudioSource>().Play();
        StartCoroutine(temporizador(1));
    }
    
    public void modoRevison() => resvision = true;
    IEnumerator RandomPosion()
    {
        yield return new WaitForSeconds(0.1f);
        statePos = 0;

        for (int j = 0; j < cartas.Count; j++)
        {
            int posnew = UnityEngine.Random.Range(0, cartas.Count - 1);

            if (statePos != posnew)
            {
                targetTemp = cartas[j].transform.localPosition;
                placeTemp = j;

                randomTemp = cartas[j].gameObject;

                cartas[j].transform.localPosition = cartas[posnew].transform.localPosition;

                cartas[posnew].transform.localPosition = targetTemp;
                cartas[j] = cartas[posnew];
                cartas[posnew] = randomTemp;
            }

        }
    }
    public IEnumerator cartaSeleccionada(GameObject i)
    {
        controlAudio.PlayAudio(0);
        Numero_Cartas++;
        int Right = 0;

        if (Numero_Cartas == 1)
            cartaTemporal1 = i;
        else if (Numero_Cartas == 2 && cartaTemporal1 != i)
        {
            Numero_Cartas = 0;
            cartaTemporal2 = i;
            mover = false;
            if (cartaTemporal1.GetComponent<M7A76CartaSeleccionada>().carataPareja == cartaTemporal2)
            {
                Right++;
                Aciertos++;
                StartCoroutine(DescativarCartas());
                controlPuntaje.IncreaseScore();
            }
            else
            {
                StartCoroutine(cartaTemporal2.GetComponent<M7A76CartaSeleccionada>().tiempoCambio(4f));
                StartCoroutine(cartaTemporal1.GetComponent<M7A76CartaSeleccionada>().tiempoCambio(4f));
            }
            yield return new WaitForSeconds(3);
            controlAudio.PlayAudio(Right == 1 ? 1 : 2);

            yield return new WaitForSeconds(1);
            mover = true;
        }
        else
            Numero_Cartas--;

        if (Aciertos == (cartas.Count/2))
        {           
            //controlPuntaje.IncreaseScore(Aciertos);
            controlNavegacion.Forward(1);            
        }        
    }
    private void OnDisable()
    {
        for (int x = 0; x < transform.childCount; x++)
        {
            if (transform.GetChild(x).GetComponent<M7A76CartaSeleccionada>())
            {
                transform.GetChild(x).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                transform.GetChild(x).GetComponent<Image>().sprite = transform.GetChild(x).GetComponent<BehaviourSprite>()._selection;
            }
        }
    }
    IEnumerator DescativarCartas()
    {
        yield return new WaitForSeconds(4);
        cartaTemporal1.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        cartaTemporal2.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }
    public void ResetAll()
    {
        minutos = 2;
        segundos = 00;
        if(Tiempo)
            Tiempo.text = Convert.ToString(minutos) + ":"+ "0" + Convert.ToString(segundos);

        resvision = false;
        Aciertos = 0;
        cartaTemporal1 = null;
        cartaTemporal2 = null;
        random = true;
        mover = true;

        foreach (var carta in cartas)
            carta.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

    }
}
