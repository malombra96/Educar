using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A31_aplicoMP : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite movil, pc;
    public GameObject comenzar,joystick;
    public bool firstTime;
    
    void Start()
    {
        firstTime = true;
        joystick.SetActive(false);
        if (Application.isMobilePlatform)
        {
            GetComponent<Image>().sprite = movil;
            comenzar.GetComponent<RectTransform>().anchoredPosition = new Vector2(comenzar.GetComponent<RectTransform>().anchoredPosition.x,-126);
        }
        else
        {
            GetComponent<Image>().sprite = pc;
            comenzar.GetComponent<RectTransform>().anchoredPosition = new Vector2(comenzar.GetComponent<RectTransform>().anchoredPosition.x, -220);
        }

    }


    public void verificar(bool x) {
        if (x)
        {
            comenzar.SetActive(false);
            GetComponent<Image>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else {
            if (firstTime) {
                comenzar.SetActive(true);
                GetComponent<Image>().enabled = true;

                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);

                }
            }
            
        }
    }
}
