using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L40_Inputs : MonoBehaviour
{
    [Header("Respuestas :")] public List<String> respuestas;
    [Header("Entradas :")]  public  List<GameObject> inputs,grupo;
    [Header("Boton Validar :")]  public GameObject btn_validar;
    [Header("Plataformas :")]  public GameObject plataformas;
    [Header("Player :")]  public GameObject Player;
    [Header("Manager :")] public M8L40_Manager_aplico1 manager;
    [HideInInspector] public M8L40_Conozco back;
    public  bool _on = true;
    public bool g1,g2,g3;
    public enum aplicotipo
    {
        normal,
        ultimo
    }

    [Header("tipo de aplico :")] public aplicotipo tipo;
    
    public int contador = 0;

    public Button informacion, pregunta;

    private void Update()
    {
        //if (inputs[contador].GetComponent<InputField>().text != "?" && inputs[contador].GetComponent<InputField>().text != "" && inputs[contador].GetComponent<InputField>().interactable == true)
        //{
        //    btn_validar.GetComponent<Button>().interactable = true;
        //}
        //else {
        //    btn_validar.GetComponent<Button>().interactable = false;
        //}
    }

    private void OnEnable()
    {
        back = FindObjectOfType<M8L40_Conozco>();
        switch (tipo)
        {
            case aplicotipo.normal:

                if (!back.nivel1 && !_on)
                {
                    if (contador == 0)
                    {
                        //Player.GetComponent<Animator>().Play("Salto");

                    }

                    if (contador == 1)
                    {
                        Player.GetComponent<Animator>().Play("Salto");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[1].SetActive(true);
                        // Player.GetComponent<Animator>().Play("salto2_1");
                    }

                    if (contador == 2)
                    {
                        Player.GetComponent<Animator>().Play("back_1");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[1].SetActive(true);
                    }
                }
                break;
            case aplicotipo.ultimo:
                if (contador == 0)
                {
                    
                }

                if (contador == 1)
                {
                    Player.GetComponent<Animator>().Play("Salto");
                    plataformas.GetComponent<M8L40_Plataformas>().plataformas[1].SetActive(true);
                }

                if (contador == 2)
                {
                    Player.GetComponent<Animator>().Play("salto2_1");
                    plataformas.GetComponent<M8L40_Plataformas>().plataformas[1].SetActive(true);
                }
                break;
        }

        _on = false;
    }

    void Start()
    {
        Activacion();

        btn_validar.GetComponent<Button>().interactable = false;
        btn_validar.GetComponent<Button>().onClick.AddListener(Changen);
    }

    public void Changen()
    {
        informacion.enabled = false;
        pregunta.enabled = false;
        btn_validar.GetComponent<Button>().interactable = false;

        switch (tipo)
        {
            case aplicotipo.normal:
                if (inputs[contador].GetComponent<InputField>().text == respuestas[contador])
                {
                    manager.Right();
                    Color32 green = new Color32(0,255, 0,255);
                    for (int i = 0; i < grupo[contador].GetComponent<M8L40_group>().inputs.Count; i++)
                    {
                        grupo[contador].GetComponent<M8L40_group>().inputs[i].interactable = false;
                        grupo[contador].GetComponent<M8L40_group>().inputs[i].transform.GetChild(1).GetComponent<Text>().color = green;
                    }
                    if (contador == 0)
                    {
                        Player.GetComponent<Animator>().Play("Salto");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[1].SetActive(true);
                        g1 = true;
                    }
            
                    if (contador == 1)
                    {
                        Player.GetComponent<Animator>().Play("salto2_1");
                        g2 = true;
                    }
            
                    if (contador == 2)
                    {
                        Player.GetComponent<Animator>().Play("salto3_1");
                        g3 = true;
                    }
                    contador++;
                    if (contador < 3)
                    {
                        for (int i = 0; i < grupo[contador].GetComponent<M8L40_group>().inputs.Count; i++)
                        {
                            grupo[contador].GetComponent<M8L40_group>().inputs[i].interactable = true;
                        }
                    }
                    StartCoroutine(Activacion1());
                }
                else
                {

                    btn_validar.GetComponent<Button>().interactable = false;
                    manager.Wrong();
                    Color32 red = new Color32(255,0, 0,255);
                    for (int i = 0; i < grupo[contador].GetComponent<M8L40_group>().inputs.Count; i++)
                    {
                        grupo[contador].GetComponent<M8L40_group>().inputs[i].interactable = false;
                        grupo[contador].GetComponent<M8L40_group>().inputs[i].transform.GetChild(1).GetComponent<Text>().color = red;
                    }
                    if (contador == 0)
                    {
                        Player.GetComponent<Animator>().Play("Salto 0");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[0].GetComponent<Animator>().Play("p_error_1");
                    }
            
                    if (contador == 1)
                    {
                        Player.GetComponent<Animator>().Play("salto2_1 0");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[1].GetComponent<Animator>().Play("p_error_2");
                    }
            
                    if (contador == 2)
                    {
                        Player.GetComponent<Animator>().Play("salto3_1 0");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[2].GetComponent<Animator>().Play("p_error_3");
                    }
                }
                
                break;
            case aplicotipo.ultimo:
                int total = 0;
                for (int i = 0; i < 2; i++)
                {
                    if (inputs[contador + i].GetComponent<InputField>().text == respuestas[contador + i])
                    {
                        Color32 green = new Color32(0, 255, 0, 255);
                        inputs[contador + i].transform.GetChild(1).GetComponent<Text>().color = green;
                        total++;
                    }
                    else
                    {
                        Color32 red = new Color32(255, 0, 0, 255);
                        inputs[contador + i].transform.GetChild(1).GetComponent<Text>().color = red;
                    }
                }

                if (total == 2)
                {
                    manager.Right();
                    Color32 green = new Color32(0, 255, 0, 255);
                    for (int i = 0; i < grupo[contador].GetComponent<M8L40_group>().inputs.Count; i++)
                    {
                        grupo[contador].GetComponent<M8L40_group>().inputs[i].interactable = false;
                        
                        grupo[contador].GetComponent<M8L40_group>().inputs[i].transform.GetChild(1).GetComponent<Text>().color = green;
                    }
                    if (contador == 0)
                    {
                        Player.GetComponent<Animator>().Play("Salto");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[1].SetActive(true);
                    }

                    if (contador == 1)
                    {
                        Player.GetComponent<Animator>().Play("salto2_1");
                    }

                    if (contador == 2)
                    {
                        Player.GetComponent<Animator>().Play("salto3_1");
                    }

                    contador ++;
                    if (contador<3) {
                        for (int i = 0; i < grupo[contador].GetComponent<M8L40_group>().inputs.Count; i++)
                        {
                            grupo[contador].GetComponent<M8L40_group>().inputs[i].interactable = true;
                        }
                    }
                    StartCoroutine(Activacion1());
                }
                else
                {
                    manager.Wrong();
                    Color32 red = new Color32(255, 0, 0, 255);
                    for (int i = 0; i < grupo[contador].GetComponent<M8L40_group>().inputs.Count; i++)
                    {
                        grupo[contador].GetComponent<M8L40_group>().inputs[i].interactable = false;
                        grupo[contador].GetComponent<M8L40_group>().inputs[i].transform.GetChild(1).GetComponent<Text>().color = red;
                    }
                    if (contador == 0)
                    {
                        Player.GetComponent<Animator>().Play("Salto 0");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[0].GetComponent<Animator>()
                            .Play("p_error_1");
                    }

                    if (contador == 1)
                    {
                        Player.GetComponent<Animator>().Play("salto2_1 0");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[1].GetComponent<Animator>()
                            .Play("p_error_2");
                    }

                    if (contador == 2)
                    {
                        Player.GetComponent<Animator>().Play("salto3_1 0");
                        plataformas.GetComponent<M8L40_Plataformas>().plataformas[2].GetComponent<Animator>()
                            .Play("p_error_3");
                    }
                }
                
                break;
        }
        //Activacion();
        
    }

    IEnumerator habilitar(int i) {
        yield return new WaitForSeconds(3);
        inputs[i].GetComponent<InputField>().interactable = true;
    }

    IEnumerator Activacion1()
    {
        yield return new WaitForSeconds(2.5f);
        informacion.enabled = true;
        pregunta.enabled = true;
    }

    public void Activacion()
    {
        for (int i = 0; i < grupo.Count; i++)
        {
            if (i != contador)
            {
                for (int j = 0; j < grupo[i].GetComponent<M8L40_group>().inputs.Count; j++)
                {
                    grupo[i].GetComponent<M8L40_group>().inputs[j].interactable = false;
                }
            }
            else
            {

                for (int j = 0; j < grupo[i].GetComponent<M8L40_group>().inputs.Count; j++)
                {
                    grupo[i].GetComponent<M8L40_group>().inputs[j].interactable = true;
                }
            }
        }
        informacion.enabled = true;
        pregunta.enabled = true;

        /*switch (tipo)
        {
            case aplicotipo.normal:
                for (int i = 0; i < inputs.Count; i ++)
                    if (contador == i)
                    {
                        if (inputs[i].GetComponent<M8L40_Input_Numpad>().calificado) {
                            inputs[i].GetComponent<InputField>().interactable = false;
                            StartCoroutine(habilitar(i));
                            
                        }
                        else
                        {
                            inputs[i].GetComponent<InputField>().interactable = true;
                            btn_validar.GetComponent<Button>().interactable = true;
                        }
                        
                    }
                    else
                    {
                        inputs[i].GetComponent<InputField>().interactable = false;
                    }
                break;
            case aplicotipo.ultimo:
                for (int i = 0; i < inputs.Count; i += 2)
                    if (contador == i)
                    {
                        btn_validar.GetComponent<Button>().interactable = true;
                        inputs[i].GetComponent<InputField>().interactable = true;
                        inputs[i + 1].GetComponent<InputField>().interactable = true;
                    }
                    else
                    {
                        inputs[i].GetComponent<InputField>().interactable = false;
                        inputs[i + 1].GetComponent<InputField>().interactable = false;
                    }
                break;
        }*/
    }


    public void RestarAll()
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            if (inputs[i].transform.childCount == 1)
            {
                inputs[i].transform.GetChild(0).GetComponent<Text>().color = Color.white;
                inputs[i].GetComponent<InputField>().text = "?";
                inputs[i].GetComponent<M8L40_Input_Numpad>().calificado = false;
            }
            else
            {
                inputs[i].transform.GetChild(1).GetComponent<Text>().color = Color.white;
                inputs[i].GetComponent<InputField>().text = "?";
                inputs[i].GetComponent<M8L40_Input_Numpad>().calificado = false;
            }

            
        }
        btn_validar.GetComponent<Button>().interactable = true;
        informacion.enabled = true;
        pregunta.enabled = true;
    }
}
