using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M7A117_t : MonoBehaviour
{
    // Start is called before the first frame update

    public Toggle toggle;
    public List<GameObject> hijos;
    public GameObject imagen;
    void Start()
    {
        toggle.onValueChanged.AddListener(delegate { habilitar(toggle); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void habilitar(Toggle t) {
        if (t.isOn)
        {
            imagen.SetActive(false);
            foreach (var h in hijos) {
                if (h.GetComponent<Toggle>()) {
                    h.GetComponent<Toggle>().interactable = true;
                }
                if (h.GetComponent<Button>()) {
                    h.GetComponent<Button>().interactable = true;
                }
            }
        }
        else {
            foreach (var h in hijos)
            {
                if (h.GetComponent<Toggle>())
                {
                    h.GetComponent<Toggle>().interactable = false;
                }
                if (h.GetComponent<Button>())
                {
                    h.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
