using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
public class L4A238_input : MonoBehaviour, IPointerClickHandler
{
    [Header("Mobile Object")] public GameObject KeyPad;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Application.isMobilePlatform )
        {
            KeyPad.SetActive(true);
        }
    }
}
