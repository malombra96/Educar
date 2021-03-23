using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M6L116informacion : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    ControlAudio controlAudio;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        transform.GetChild(0).gameObject.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        controlAudio.PlayAudio(0);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
