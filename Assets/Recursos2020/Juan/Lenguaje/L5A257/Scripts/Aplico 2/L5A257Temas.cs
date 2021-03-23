using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A257Temas : MonoBehaviour
{
    ControlAudio controlAudio;
    public List<Toggle> toggles;
    public List<GameObject> informacion;
    public List<GameObject> seleccion;
    int temp;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        foreach (var toggle in toggles)
            toggle.onValueChanged.AddListener(delegate { Activar(); });

        foreach (var info in informacion)
        {
            info.SetActive(false);
            for (int x = 0; x < info.transform.childCount; x++)
            {
                var boton = info.transform.GetChild(x).GetComponent<Button>();
                boton.onClick.AddListener(delegate { seleccionar(boton.name); });
            }
        }

        foreach (var sele in seleccion)
            sele.SetActive(false);
    }

    void Activar()
    {
        controlAudio.PlayAudio(0);
        
        for (int x = 0; x < toggles.Count; x++)
        {
            var toggle = toggles[x];
            if (toggle.isOn)
            {
                temp = x;
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
                seleccion[temp].SetActive(false);
                informacion[x].SetActive(true);                
            }
            else
            {
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
                informacion[x].SetActive(false);
            }
        }
    }
    void seleccionar(string s)
    {
        controlAudio.PlayAudio(0);

        foreach (var toggle in toggles)
            toggle.isOn = false;

        informacion[temp].SetActive(false);
        seleccion[temp].SetActive(true);
        seleccion[temp].transform.GetChild(1).GetComponent<Text>().text = s;
    }
    public void resetAll()
    {
        foreach (var toggle in toggles)
            toggle.isOn = false;

        foreach (var sele in seleccion)
            sele.SetActive(false);

        foreach (var info in informacion)
            info.SetActive(false);
    }
}
