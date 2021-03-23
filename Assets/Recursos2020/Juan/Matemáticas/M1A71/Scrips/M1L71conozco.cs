using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1L71conozco : MonoBehaviour
{
    public Toggle[] toggle;
    public Image[] planeta;
    ControlAudio controlAudio;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        for(int i = 0; i < toggle.Length; i++)
        {
            toggle[i].onValueChanged.AddListener(delegate { activar(); });
            toggle[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void activar()
    {
        controlAudio.PlayAudio(0);
        for (int i = 0; i < toggle.Length; i++)
        {
            if (toggle[i].isOn)
            {
                toggle[i].transform.GetChild(0).gameObject.SetActive(true);
                planeta[i].sprite = planeta[i].GetComponent<BehaviourSprite>()._selection;
                planeta[i].SetNativeSize();
                break;
            }
            else
            {
                planeta[i].sprite = planeta[i].GetComponent<BehaviourSprite>()._default;
                planeta[i].SetNativeSize();
            }
        }        
    }
    public void resetAll()
    {
        for (int i = 0; i < toggle.Length; i++)
        {
            toggle[i].transform.GetChild(0).gameObject.SetActive(false);
            toggle[i].isOn = false;
            planeta[i].sprite = planeta[i].GetComponent<BehaviourSprite>()._default;
            planeta[i].SetNativeSize();
        }
    }
}
