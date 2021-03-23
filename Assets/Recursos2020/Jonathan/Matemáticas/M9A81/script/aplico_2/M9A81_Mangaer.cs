using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A81_Mangaer : MonoBehaviour
{
    // Start is called before the first frame update.

    public List<Toggle> ListaToggles;
    public List<GameObject> Jugadores,preguntas;
    public Button botonIniciar;
    public GameObject seleccionarPersonajeGameobject;
    public GameObject player;
    public bool playerSeleccionado,review;
    public ControlAudio controlAudio;
    public ControlNavegacion controlNavegacion;
    public int contador;
    

    void Start()
    {
        if (!review) {
            ListaToggles[0].onValueChanged.AddListener(delegate { ClickPersonaje(ListaToggles[0]); });
            ListaToggles[1].onValueChanged.AddListener(delegate { ClickPersonaje(ListaToggles[1]); });
            ListaToggles[2].onValueChanged.AddListener(delegate { ClickPersonaje(ListaToggles[2]); });
            ListaToggles[3].onValueChanged.AddListener(delegate { ClickPersonaje(ListaToggles[3]); });
            botonIniciar.onClick.AddListener(delegate { SeleccionarPersonaje(); });
            playerSeleccionado = false;
            foreach (var x in preguntas)
                x.SetActive(false);
            contador = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerSeleccionado) {
            if (player != null)
            {
                botonIniciar.interactable = true;
            }
            else
            {
                botonIniciar.interactable = false;
            }
        }

    }
    public void Review() {
        review = true;
        for (int i = 0; i < Jugadores.Count; i++)
        {
            Jugadores[i].GetComponent<M9A81_jugador>().mover = false;
        }
    }
    public void SeleccionarPersonaje() {
        controlAudio.PlayAudio(0);
        seleccionarPersonajeGameobject.SetActive(false);
        playerSeleccionado = true;
        for (int i = 0; i < ListaToggles.Count; i++)
        {
            ListaToggles[i].interactable = false;
        }
        int p = ListaToggles.IndexOf(player.GetComponent<Toggle>());
        print(p);
        Jugadores[p].name = "player";
        preguntas[0].SetActive(true);
    }

    public void ClickPersonaje(Toggle t) {
        controlAudio.PlayAudio(0);
        if (t.isOn)
        {
            player = t.gameObject;
            t.name = "player";
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._selection;
        }
        else {
            player = null;
            t.name = "toggle";
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
        }
    }

    public void ResetAll() {
        contador = 0;
        playerSeleccionado = false;
        for (int i = 0; i < ListaToggles.Count; i++)
        {
            ListaToggles[i].interactable = true;
            ListaToggles[i].isOn = false;
            ListaToggles[i].GetComponent<Image>().sprite = ListaToggles[i].GetComponent<BehaviourSprite>()._default;

        }
        seleccionarPersonajeGameobject.SetActive(true);

        for (int i = 0; i < Jugadores.Count; i++)
        {
            if (Jugadores[i].name == "player")
            {
                Jugadores[i].GetComponent<M9A81_jugador>().movespeed = 0f;
                Jugadores[i].GetComponent<M9A81_jugador>().mover = false;
                Jugadores[i].name = "jugador";
            }
            else
            {
                Jugadores[i].GetComponent<M9A81_jugador>().movespeed = 0f;
                Jugadores[i].GetComponent<M9A81_jugador>().mover = false;
            }

            Jugadores[i].GetComponent<RectTransform>().anchoredPosition = Jugadores[i].GetComponent<M9A81_jugador>().inicial;
            foreach (var p in preguntas) {
                p.GetComponent<M9A81_managerSeleccionarA2>().ResetSeleccionarToggle();
                p.SetActive(false);
            }
            preguntas[0].SetActive(true);
        }
    }

    public void MoverJugadores(bool respuesta, GameObject g) {
        if (respuesta)
        {
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].name == "player")
                {
                    Jugadores[i].GetComponent<M9A81_jugador>().movespeed = 0.0053f;
                    Jugadores[i].GetComponent<M9A81_jugador>().mover = true;
                }
                else
                {
                    Jugadores[i].GetComponent<M9A81_jugador>().movespeed = Random.Range(0.003f, 0.004f);
                    Jugadores[i].GetComponent<M9A81_jugador>().mover = true;
                }

            }
        }
        else {
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].name == "player")
                {
                    Jugadores[i].GetComponent<M9A81_jugador>().movespeed = Random.Range(0.003f, 0.004f);
                    Jugadores[i].GetComponent<M9A81_jugador>().mover = true;
                }
                else
                {
                    Jugadores[i].GetComponent<M9A81_jugador>().movespeed = Random.Range(0.0045f, 0.005f);
                    Jugadores[i].GetComponent<M9A81_jugador>().mover = true;
                }

            }
        }
        
        StartCoroutine(DetenerJugadores(g));
        contador++;
        if (contador == preguntas.Count)
        {
            controlNavegacion.Forward(2.0f);
        }
        if (contador < preguntas.Count) {
            preguntas[contador].SetActive(false);
        }
        
        

    }

    IEnumerator DetenerJugadores(GameObject g) {
        yield return new WaitForSeconds(1f);
        g.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < Jugadores.Count; i++)
        {
            Jugadores[i].GetComponent<M9A81_jugador>().mover = false;
        }
        if (contador < preguntas.Count)
        {
            preguntas[contador].SetActive(true);
        }
        


    }
}
