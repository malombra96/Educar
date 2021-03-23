using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L42ActivadorPregunta : MonoBehaviour
{
    public GameObject toggles;
    public GameObject pregunta;
    public List<M8L42ActivadorPregunta> activadorPreguntas;
    /*[HideInInspector]*/public List<Toggle> opcionesUsadas;

    public GameObject buton;

    ControlAudio controlAudio;

    // Start is called before the first frame update
    void Start()
    {
        buton.SetActive(false);
        controlAudio = FindObjectOfType<ControlAudio>();

        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            toggles.transform.GetChild(x).GetComponent<Toggle>().onValueChanged.AddListener(delegate { AbrirPregunta(); });            
        }        
    }
    private void OnEnable()
    {        

        foreach (var i in opcionesUsadas)
        {
            for (int x = 0; x < toggles.transform.childCount; x++)
            {
                if (i.name == toggles.transform.GetChild(x).name)
                {
                    toggles.transform.GetChild(x).GetComponent<Toggle>().interactable = false;
                    toggles.transform.GetChild(x).GetComponent<Animator>().SetBool("interactable", false);
                    toggles.transform.GetChild(x).GetComponent<Image>().sprite = toggles.transform.GetChild(x).GetComponent<BehaviourSprite>()._default;
                    toggles.transform.GetChild(x).GetComponent<Image>().color = new Color32(255, 255, 255, 180);
                }
            }         
            
        }
    }
    // Update is called once per frame
    void AbrirPregunta()
    {
        controlAudio.PlayAudio(0);
        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            var toggle = toggles.transform.GetChild(x).GetComponent<Toggle>();
            if (toggle.isOn)
            {
                toggle.GetComponent<Animator>().SetBool("interactable", false);

                buton.SetActive(true);
                pregunta.transform.GetChild(x).gameObject.SetActive(true);

                toggles.transform.GetChild(x).GetComponent<Image>().sprite = toggles.transform.GetChild(x).GetComponent<BehaviourSprite>()._default;
                toggles.transform.GetChild(x).GetComponent<Image>().color = new Color32(255, 255, 255, 180);
                for (int i = 0; i < activadorPreguntas.Count; i++)
                    activadorPreguntas[i].opcionesUsadas.Add(toggle);

            }
            else
                toggle.interactable = false;
        }
    }

    public void ResetAll()
    {
        GetComponent<M8L42ManagerInputField>().resetAll();
        buton.SetActive(false);
        activadorPreguntas.Clear();

        for(int i=0;i< pregunta.transform.childCount;i++)
            pregunta.transform.GetChild(i).gameObject.SetActive(false);

        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            toggles.transform.GetChild(x).GetComponent<Toggle>().enabled = true;

            toggles.transform.GetChild(x).GetComponent<Image>().sprite = toggles.transform.GetChild(x).GetComponent<BehaviourSprite>()._default;
            toggles.transform.GetChild(x).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            toggles.transform.GetChild(x).GetComponent<Toggle>().interactable = true;
            toggles.transform.GetChild(x).GetComponent<Animator>().SetBool("interactable", true);
        }        
    }
}
