using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A255_Inicio : MonoBehaviour
{
    public Toggle toggleMan, toggleWoman;
    public string cadena;
    public Button iniciar;
    public ControlAudio controlAudio;
    public L4A255_player player;
    public GameObject eleentos, botoninfo,toggleMan2,toggleWoman2,informacion,conozco,aplico,salir;
    // Start is called before the first frame update
    void Start()
    {
        toggleMan.onValueChanged.AddListener(delegate { Cambiar(toggleMan); });
        toggleWoman.onValueChanged.AddListener(delegate { Cambiar(toggleWoman); });
        iniciar.onClick.AddListener(Seleccionar);
        player.mover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cadena == "")
        {
            iniciar.interactable = false;
        }
        else {
            iniciar.interactable = true;
        }
    }

    public void Cambiar(Toggle t) {
        controlAudio.PlayAudio(0);
        if (t.isOn)
        {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._selection;
            cadena = t.name;
        }
        else
        {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
            cadena = "";
        }
    }

    public void Seleccionar() {
        controlAudio.PlayAudio(0);
        gameObject.SetActive(false);
        player.mover = true;
        eleentos.SetActive(true);
        botoninfo.SetActive(true);
        informacion.SetActive(true);
        aplico.SetActive(true);
        salir.SetActive(true);
        conozco.SetActive(true);

    }

    IEnumerator x() {
        yield return new WaitForSeconds(4f);
        informacion.SetActive(false);
    }
}
