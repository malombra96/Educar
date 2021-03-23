using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class L9A282MostrarInformacionOve : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject info;
    ControlAudio controlAudio;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        info.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        info.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        info.SetActive(false);
    }
    public void closed() 
    {
        controlAudio.PlayAudio(0);
        info.SetActive(false); 
    }
}
