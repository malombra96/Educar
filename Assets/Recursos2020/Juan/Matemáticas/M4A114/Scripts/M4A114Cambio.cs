using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M4A114Cambio : MonoBehaviour
{
    ControlAudio controlAudio;

    public Sprite imagen1Next;
    public Sprite imagen2Next;
    public Image othor;
    public Sprite imagen1Back;
    public Sprite imagen2Back;
    public Button buttonNext;
    public Button buttonBack;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();

        buttonNext.onClick.AddListener(cambiar);
        buttonBack.onClick.AddListener(volver);

        buttonNext.gameObject.SetActive(true);
        buttonBack.gameObject.SetActive(false);
    }
    
    void cambiar()
    {
        controlAudio.PlayAudio(0);
        
        GetComponent<Image>().sprite = imagen1Next;
        othor.sprite = imagen2Next;

        buttonNext.gameObject.SetActive(false);
        buttonBack.gameObject.SetActive(true);
    }
    void volver()
    {
        controlAudio.PlayAudio(0);

        GetComponent<Image>().sprite = imagen1Back;
        othor.sprite = imagen2Back;

        buttonNext.gameObject.SetActive(true);
        buttonBack.gameObject.SetActive(false);
    }
}
