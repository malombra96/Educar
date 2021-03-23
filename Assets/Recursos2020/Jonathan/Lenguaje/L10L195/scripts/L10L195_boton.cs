using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10L195_boton : MonoBehaviour
{
    Button boton;
    // Start is called before the first frame update
    void Start()
    {
        boton = GetComponent<Button>();
        boton.onClick.AddListener(click);
    }

    public void click() {
        boton.GetComponent<Image>().sprite = boton.GetComponent<BehaviourSprite>()._selection;
    }
}
