using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class L10A190_input : MonoBehaviour, IPointerClickHandler
{
    public L10A190_manager general;
    [Header("Mobile Object")] public GameObject KeyPad;
    public string palabra;
    public Button validar;
    public InputField input;
    public ControlAudio audio;
    public ControlPuntaje ControlPuntaje;
    public ControlNavegacion ControlNavegacion;
    public Color32 verde, rojo;
    public GameObject aplico, desempeno;
    // Start is called before the first frame update
    void Start()
    {
        validar.onClick.AddListener(calificar);
    }

    private void Update()
    {
        if (input.textComponent.text == "")
        {
            validar.interactable = false;
        }
        else {
            input.textComponent.text = input.textComponent.text.ToLower();
            validar.interactable = true;
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Application.isMobilePlatform)
        {
            KeyPad.SetActive(true);
        }
    }

    public void calificar() {
        validar.interactable = false;
        audio.PlayAudio(0);
        input.interactable = false;
        string x = input.textComponent.text.ToLower();
        StartCoroutine(y(x));
    }

    IEnumerator y(string x) {
        yield return new WaitForSeconds(0.1f);
        if (x.Contains(palabra))
        {
            audio.PlayAudio(1);
            ControlPuntaje.IncreaseScore();
            input.textComponent.GetComponent<Text>().color = verde;
        }
        else
        {
            audio.PlayAudio(2);
            input.textComponent.GetComponent<Text>().color = rojo;
        }
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.parent.gameObject.SetActive(false);
        //aplico.SetActive(false);
        //        desempeno.SetActive(true);
        general.player.canMove = true;
        foreach (var item in general.enemies)
        {
            item.GetComponent<L10A190_enemy>().canMove = true;
        }
        general.pasar();
    }

    public void resetall() {
        input.textComponent.GetComponent<Text>().color = Color.black;
    }
}
