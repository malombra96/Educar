using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M5A115_ficha : MonoBehaviour
{
    public Button ficha;
    public GameObject fichaPareja,seleccionar,input;
    public M5A115_general_1 general;
    public bool pregunta1, pregunta2, calificado;
    public ControlPuntaje ControlPuntaje;
    
    // Start is called before the first frame update
    void Start()
    {
        ficha = GetComponent<Button>();
        ficha.onClick.AddListener(mostrar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrar() {
        seleccionar.SetActive(true);
        seleccionar.GetComponent<M5A115_managerSeleccionar>().ResetSeleccionarToggle();
        input.GetComponent<M5A115_managerInput>().resetAll();
        pregunta1 = false;
        pregunta2 = false;
    }

    public IEnumerator CalificarPregunta(bool value, GameObject g) {
        
        if (g == seleccionar) {
            if (value)
            {
                pregunta1 = true;
                yield return new WaitForSeconds(1f);
                g.SetActive(false);
                input.SetActive(true);
            }
            else {
                pregunta1 = false;
                yield return new WaitForSeconds(1f);
                g.SetActive(false);
                input.SetActive(true);
            }
        }

        if (g == input)
        {
            if (value)
            {
                pregunta2 = true;
                yield return new WaitForSeconds(1f);
                g.SetActive(false);
            }
            else {
                pregunta2 = false;
                yield return new WaitForSeconds(1f);
                g.SetActive(false);
            }

            if (!pregunta1 && pregunta2) {
                general.RestarVidas();
            }
            else if (pregunta1 && !pregunta2) {
                general.RestarVidas();
            }
            else if (!pregunta1 && !pregunta2) {
                general.RestarVidas();
            } 
            else if (pregunta1 && pregunta2) {
                calificado = true;
                fichaPareja.SetActive(true);
                ficha.interactable = false;
                ControlPuntaje.IncreaseScore();
                general.intentos();
            }
        }

    }
}
