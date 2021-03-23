using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L3A227Pestañas : MonoBehaviour
{
    ControlAudio controlAudio;

    public List<Toggle> toggles;
    public List<GameObject> informacion;
    public GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        foreach (var toggle in toggles)
             toggle.onValueChanged.AddListener(delegate { activarInformacion(); });
    }

    // Update is called once per frame
    void activarInformacion()
    {
        controlAudio.PlayAudio(0);
        int i = 0;
        for (int x = 0; x < toggles.Count; x++)
        {
            if (toggles[x].isOn)
            {
                toggles[x].GetComponent<Image>().sprite = toggles[x].GetComponent<BehaviourSprite>()._selection;
                informacion[x].SetActive(true);              
            }
            else
            {
                toggles[x].GetComponent<Image>().sprite = toggles[x].GetComponent<BehaviourSprite>()._default;
                informacion[x].SetActive(false);                
            }
        }
    }
}
