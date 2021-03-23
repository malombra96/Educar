using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6A92_managerBarra : MonoBehaviour
{
    // Start is called before the first frame update
    public List<M6A92_barra> sliders;
    [Header("Arrastre Boton Validar")]
    public Button _validar;
    public ControlPuntaje controlPuntaje;
    public ControlNavegacion controlNavegacion;
    public ControlAudio controlAudio;
    public int count;

    void Start()
    {
        _validar.onClick.AddListener(delegate { ValidarGraficas(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void  ValidarGraficas() {

        _validar.interactable = false;
        for (int i = 0; i < sliders.Count;i++) {
            sliders[i].GetComponent<Slider>().interactable = false;
            if (sliders[i].GetComponent<Slider>().value == sliders[i].value)
            {
                controlPuntaje.IncreaseScore();
                sliders[i].gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = sliders[i].correcto;
                count++;
            }
            else {
                sliders[i].gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = sliders[i].incorrecto;
            }
        }

        if (count == sliders.Count)
        {
            controlAudio.PlayAudio(1);
        }
        else {
            controlAudio.PlayAudio(2);
        }
        controlNavegacion.Forward(1.5f);
    }

    public void ResetAll() {
        _validar.interactable = true;
        for (int i = 0; i < sliders.Count; i++)
        {
            sliders[i].GetComponent<Slider>().interactable = true;
            sliders[i].gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = sliders[i].original;
            sliders[i].GetComponent<Slider>().value = 0;
        }
        count = 0;

    }
}
