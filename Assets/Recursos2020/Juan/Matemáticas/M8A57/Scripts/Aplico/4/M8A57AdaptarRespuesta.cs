using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A57AdaptarRespuesta : MonoBehaviour
{
    public BehaviourGroupInputField behaviourGroupInputField;
   public List<GameObject> inpunt;
    public string respuesta1A;
    public string respuesta2A;

    public bool p;
    public string respuesta1B;
    public string respuesta2B;
    
    void Start()
    {
        behaviourGroupInputField = GetComponent<BehaviourGroupInputField>();
        for (int x = 0; x < behaviourGroupInputField.transform.childCount; x++)
        {
            inpunt.Add(behaviourGroupInputField.transform.GetChild(x).gameObject);
        }
        p = true;
    }

    private void OnEnable()
    {
        quitarRespuestas();
    }
    void Update()
    {
        for (int x = 0; x < inpunt.Count; x++)
        {
            if (inpunt[x].GetComponent<InputField>().text != "" && p)
            {
                p = false;
                quitarRespuestas();
                switch (x)
                {
                    case 0:

                        if (inpunt[x].GetComponent<InputField>().text == respuesta1A)
                        {
                            inpunt[0].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(respuesta1A);
                            inpunt[1].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(respuesta2A);
                        }
                        else if (inpunt[x].GetComponent<InputField>().text == respuesta1B)
                        {

                            inpunt[0].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(respuesta1B);
                            inpunt[1].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(respuesta2B);
                        }

                        break;

                    case 1:

                        if (inpunt[x].GetComponent<InputField>().text == respuesta2A)
                        {
                            inpunt[0].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(respuesta1A);
                            inpunt[1].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(respuesta2A);
                        }
                        else if (inpunt[x].GetComponent<InputField>().text == respuesta2B)
                        {

                            inpunt[0].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(respuesta1B);
                            inpunt[1].GetComponent<BehaviourInputField>().respuestaCorrecta.Add(respuesta2B);
                        }

                        break;
                }
                break;
            }
            else
                p = true;

        }


    }
    void quitarRespuestas()
    {
        for (int i = 0; i < inpunt.Count; i++)
        {
            inpunt[i].GetComponent<BehaviourInputField>().respuestaCorrecta.Clear();
        }
    }


}
