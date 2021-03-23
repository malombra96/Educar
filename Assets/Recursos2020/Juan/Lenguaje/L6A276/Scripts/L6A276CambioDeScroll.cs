using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L6A276CambioDeScroll : MonoBehaviour
{
    public List<Button> cambios;
    public List<GameObject> casos;
    public Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar.gameObject.SetActive(false);
        
    }
    private void Update()
    { 
        foreach (var caso in casos) 
        {
            if (caso.transform.parent.gameObject.activeSelf)
            {                
                scrollbar.gameObject.SetActive(true);
                caso.transform.GetChild(0).GetComponent<ScrollRect>().verticalScrollbar = scrollbar;
                scrollbar.GetComponent<RectTransform>().anchoredPosition = caso.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition;               
                break;
            }
            else
                scrollbar.gameObject.SetActive(false);
        }
    }
}
