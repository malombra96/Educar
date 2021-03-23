using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BehaviourPuntero))]
public class BehaviourCharNumPad : MonoBehaviour
{
    Button boton;
    string label;
    ManagerDisplay _managerDisplay;
    ControlAudio _controlAudio;


    private void Awake()
    {
        boton = GetComponent<Button>();
        _controlAudio = FindObjectOfType<ControlAudio>();
        _managerDisplay = FindObjectOfType<ManagerDisplay>();
    }

    void Start()
    {
        label = transform.GetChild(0).GetComponent<Text>().text;
        boton.onClick.AddListener(delegate { DoClick(boton.gameObject); });

    }

    public void DoClick(GameObject boton )
    {
        _controlAudio.PlayAudio(0);
        _managerDisplay.AddCharacter(label);
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;
        transform.GetChild(0).GetComponent<Text>().color = new Color32(255,121,0,255);
        StartCoroutine(DelaryClick());
    }

    IEnumerator DelaryClick()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
        transform.GetChild(0).GetComponent<Text>().color = Color.white;
    }
}
