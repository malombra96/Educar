using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L3A230ManagerTexto : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    public List<Toggle> toggles;
    public Color32 colorCorrecto;
    public Color32 colorIncorrecto;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();

        transform.GetChild(1).GetChild(0).gameObject.SetActive(false);

        foreach (var toggle in toggles)
            toggle.onValueChanged.AddListener(delegate { StartCoroutine(seleccion()); });
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

                transform.GetChild(1).GetChild(0).GetComponent<Text>().text = toggle.gameObject.name;
                transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(1);
                toggle.GetComponent<Image>().sprite = (b ? toggle.GetComponent<BehaviourSprite>()._right : toggle.GetComponent<BehaviourSprite>()._wrong);
                transform.GetChild(1).GetChild(0).GetComponent<Text>().color = (b ? colorCorrecto : colorIncorrecto);

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
        transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).GetChild(0).GetComponent<Text>().color = new Color32(0, 0, 0, 255);

        foreach (var toggle in toggles)
        {
            toggle.interactable = true;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
        }
    }
}
