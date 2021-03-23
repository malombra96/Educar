using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L108Espaciomuestral : MonoBehaviour
{
    ControlAudio controlAudio;
    public GameObject toggles;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        for (int x = 0; x < toggles.transform.childCount; x++)
             toggles.transform.GetChild(x).GetComponent<Toggle>().onValueChanged.AddListener(delegate { ActivadorToggle(); });

    }

    // Update is called once per frame
    void ActivadorToggle()
    {
        controlAudio.PlayAudio(0);
        for (int x = 0; x < toggles.transform.childCount; x++)
        {
            var toggle = toggles.transform.GetChild(x).GetComponent<Toggle>();

            if (toggle.isOn)
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
            else
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;

        }
    }
}
