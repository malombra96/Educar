using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L3A230Manger : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    public List<Toggle> toggles;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        transform.GetChild(3).gameObject.SetActive(false);
        foreach (var toggle in toggles)
            toggle.onValueChanged.AddListener(delegate {StartCoroutine(seleccion()); });
    }

    // Update is called once per frame
    IEnumerator seleccion()
    {
        foreach (var toggle in toggles)
        {            
            if (toggle.isOn)
            {
                controlAudio.PlayAudio(0);
                foreach (var tog in toggles)
                    tog.interactable = false;

                bool b = toggle.GetComponent<L3A230Toggle>().correcto;

                transform.GetChild(3).GetComponent<Image>().sprite = toggle.GetComponent<Image>().sprite;
                transform.GetChild(3).gameObject.SetActive(true);

                yield return new WaitForSeconds(1);
                toggle.GetComponent<Image>().sprite = (b ? toggle.GetComponent<BehaviourSprite>()._right : toggle.GetComponent<BehaviourSprite>()._wrong);
                transform.GetChild(3).GetComponent<Image>().sprite = toggle.GetComponent<Image>().sprite;

                if (b)
                    controlPuntaje.IncreaseScore();

                controlAudio.PlayAudio(b ? 1 : 2);
                break;
            }
        }
        controlNavegacion.Forward(2);
    }
    public void resetAll()
    {
        transform.GetChild(3).gameObject.SetActive(false);
        foreach (var toggle in toggles)
        {
            toggle.interactable = true;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;            
        }
    }
}
