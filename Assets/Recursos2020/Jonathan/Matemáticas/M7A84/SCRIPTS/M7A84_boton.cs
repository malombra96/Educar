using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M7A84_boton : MonoBehaviour
{
    public Toggle toggle;
    public GameObject texto;
    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener(delegate { ChangeSprite(); });
    }

    void ChangeSprite() {
        if (toggle.isOn)
        {
            texto.SetActive(true);
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
        }
        else {
            texto.SetActive(false);
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
        }
    }
}
