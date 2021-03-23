using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class M5A115_c : MonoBehaviour
{
    public GameObject hover;
    // Start is called before the first frame update

    private void Start()
    {
        gameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { Cambiar(); });
    }

    public void Cambiar() {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            hover.SetActive(true);
            gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<BehaviourSprite>()._selection;

        }
        else {
            hover.SetActive(false);
            gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<BehaviourSprite>()._default;
        }
    }
}
