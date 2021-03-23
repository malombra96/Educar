using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A63Conozco : MonoBehaviour
{
    public GameObject buttons;
    public GameObject teoria;
    public GameObject navbar;
    ControlAudio controlAudio;

    int temp;
    int j;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        for (int x = 0; x < buttons.transform.childCount; x++)
        {
            if (buttons.transform.GetChild(x).GetComponent<Toggle>())
            {
                buttons.transform.GetChild(x).GetComponent<Toggle>().onValueChanged.AddListener(delegate { Activar(); });
                buttons.transform.GetChild(x).GetComponent<Toggle>().interactable = false;
            }
            else
                buttons.transform.GetChild(x).gameObject.SetActive(false);

            //buttons.transform.GetChild(x).gameObject.SetActive(false);
        }
        buttons.transform.GetChild(0).gameObject.SetActive(true);
        teoria.transform.GetChild(0).gameObject.SetActive(true);
        buttons.transform.GetChild(1).gameObject.SetActive(true);
        buttons.transform.GetChild(3).GetComponent<Toggle>().interactable = true;
        temp = 2;
    }

    // Update is called once per frame
    void Activar()
    {
        controlAudio.PlayAudio(0);
        int i;        
        
        for(int x = 0; x < buttons.transform.childCount; x++)
        {
            if (buttons.transform.GetChild(x).GetComponent<Toggle>() && buttons.transform.GetChild(x).GetComponent<Toggle>().isOn)
            {
                if (x == 7)
                {
                    teoria.transform.GetChild(4).gameObject.SetActive(true);
                    teoria.transform.GetChild(3).gameObject.SetActive(true);
                    navbar.gameObject.SetActive(false);
                }
                else if (x == 5)
                {
                    teoria.transform.GetChild(x - 3).gameObject.SetActive(true);
                    buttons.transform.GetChild(x + 2).GetComponent<Toggle>().interactable = true;
                    buttons.transform.GetChild(x).GetComponent<Toggle>().interactable = false;
                }
                else
                {
                    teoria.transform.GetChild(x - 2).gameObject.SetActive(true);
                    buttons.transform.GetChild(x + 2).GetComponent<Toggle>().interactable = true;
                    buttons.transform.GetChild(x).GetComponent<Toggle>().interactable = false;
                }
                buttons.transform.GetChild(x).GetComponent<Toggle>().isOn = false;                
                buttons.transform.GetChild(x - 1).gameObject.SetActive(true);
                
                break;
            }
        }
        //else
        //{
        //    teoria.transform.GetChild(3).gameObject.SetActive(true);
        //    teoria.transform.GetChild(4).gameObject.SetActive(true);
        //    navbar.SetActive(false);
        //}
    }
    public void Desactivar()
    {
        controlAudio.PlayAudio(0);
        teoria.transform.GetChild(4).gameObject.SetActive(false);
        navbar.SetActive(true);
    }
}

