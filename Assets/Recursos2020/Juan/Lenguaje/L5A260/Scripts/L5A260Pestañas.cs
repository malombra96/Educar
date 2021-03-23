using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A260Pestañas : MonoBehaviour
{
    ControlAudio controlAudio;
    public List<Toggle> toggles;
    public List<GameObject> teorias;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        foreach (var toggle in toggles)
            toggle.onValueChanged.AddListener(delegate { abrir(); });
    }    
    void abrir()
    {
        foreach (var toggle in toggles) 
        {
            if (toggle.isOn)
            {
                controlAudio.PlayAudio(0);
                teorias[toggle.transform.GetSiblingIndex()].SetActive(true);

                if (toggle.GetComponent<BehaviourSprite>())
                    toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
            }
            else
            {
                teorias[toggle.transform.GetSiblingIndex()].SetActive(false);
                if (toggle.GetComponent<BehaviourSprite>())
                    toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
            }

        }
    }
    public void cerrar()
    {
        controlAudio.PlayAudio(0);
        foreach (GameObject teoria in teorias)
        {
            if (teoria.activeSelf)
            {
                teoria.SetActive(false);
                if (toggles[teoria.transform.GetSiblingIndex()].GetComponent<BehaviourSprite>())
                {
                    toggles[teoria.transform.GetSiblingIndex()].GetComponent<Image>().sprite =
                        toggles[teoria.transform.GetSiblingIndex()].GetComponent<BehaviourSprite>()._default;
                }
                toggles[teoria.transform.GetSiblingIndex()].isOn = false;
            }
        }

    }
}
