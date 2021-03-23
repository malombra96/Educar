using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10A61_pista : MonoBehaviour
{

    public Toggle toggle;
    public GameObject pista;
    public int x;
    ControlAudio controlAudio;

    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener(delegate { ShowHide(); });
        controlAudio = GameObject.FindObjectOfType<ControlAudio>();
    }

    public void ShowHide() {
        controlAudio.PlayAudio(0);
        if (toggle.isOn) {
            pista.SetActive(true);
            gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<BehaviourSprite>()._selection;
        } 
        else { 
            pista.SetActive(false);
            gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<BehaviourSprite>()._default;
        }

    }

    // Update is called once per frame
    void Update()
    {
        var max = transform.parent.transform.childCount; // Obtiene el index max
        pista.GetComponent<RectTransform>().SetSiblingIndex(max - 1); // Posiciona el objeto sobre todos los demas
    }
}
