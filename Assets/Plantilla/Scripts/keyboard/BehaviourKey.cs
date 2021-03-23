using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourKey : MonoBehaviour
{
    private Text _text;
    
    private void Start()
    {
        /// Asigna el nombre de la tecla [objeto] al texto
        
        if (gameObject.name != "ENTER" && gameObject.name != "CLEAR_ALL" && gameObject.name != "BACKSPACE")
        {
            _text = transform.GetChild(0).GetComponent<Text>();
            _text.text = gameObject.name;
        }
        
        gameObject.AddComponent<BehaviourPuntero>();
        GetComponent<Button>().onClick.AddListener(delegate { PressButton(gameObject); });
        
    }

    /// <summary>
    /// Cuando una tecla ["button"] es presionada realiza su transicion según corresponda [Color o Sprite]
    /// </summary>
    /// <param name="button"></param>
    void PressButton(GameObject button)
    {
        if (button.name == "ENTER" || button.name == "CLEAR_ALL" )
        {
            gameObject.GetComponent<Image>().color= Color.white;
            transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(237,120,7,255);
            StartCoroutine(DelayKey(button));
        }else if (button.name == "BACKSPACE")
        {
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = transform.GetChild(0).gameObject
                .GetComponent<BehaviourSprite>()._selection; 
            StartCoroutine(DelayKey(button));
        }
        else
        {           
            //print(button.name);
            
            if (!button.GetComponent<KeyPressed>())
            {
                if (button.GetComponent<BehaviourSprite>())
                {
                    //print("Tíldes");
                    button.GetComponent<Image>().sprite = button.GetComponent<BehaviourSprite>()._selection;
                    StartCoroutine(DelayKey(button));
                }
                else
                {
                    //print("Haven't key pressed");
                    _text.color = Color.white;
                    gameObject.GetComponent<Image>().color = new Color32(237,120,7,255);
                    StartCoroutine(DelayKey(button));
                }
            }
            else
            {
                //print("Have Key pressed");
                
                if (!button.GetComponent<KeyPressed>().hold)
                {
                    //print("Only Down");
                    _text.color = Color.white;
                    gameObject.GetComponent<Image>().color = new Color32(237,120,7,255);
                    StartCoroutine(DelayKey(button));
                }
                else
                {
                    //print("Hold");
                }
            }

        }
        
    }

    /// <summary>
    /// Retorna el estado default de la tecla [button"] presionada según corresponda [Color o Sprite]
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    IEnumerator DelayKey(GameObject button)
    {
        yield return new WaitForSeconds(.1f);
        
        //print(button.name);
        
        if (button.name == "CLEAR_ALL")
        {
            gameObject.GetComponent<Image>().color =new Color32(237,120,7,255);
            transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }
        else if (button.name == "ENTER")
        {
            transform.parent.parent.gameObject.GetComponent<Animator>().enabled = true;
            transform.parent.parent.gameObject.GetComponent<Animator>().Play("KeyBoard_out");// Desactivar Keypad
            gameObject.GetComponent<Image>().color =new Color32(237,120,7,255);
            transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            
        }
        else if (button.name == "BACKSPACE")
        {
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = transform.GetChild(0).gameObject
                .GetComponent<BehaviourSprite>()._default;
            
        }
        else
        {            
            if (!button.GetComponent<KeyPressed>())
            {
                if (button.GetComponent<BehaviourSprite>())
                {
                    button.GetComponent<Image>().sprite = button.GetComponent<BehaviourSprite>()._default;
                }
                else
                {
                    _text.color = Color.black;
                    gameObject.GetComponent<Image>().color = Color.white;
                }
                
            }
            else
            {               
                if (!button.GetComponent<KeyPressed>().hold)
                {
                    _text.color = Color.black;
                    gameObject.GetComponent<Image>().color = Color.white;
                }

            }

        }

    }
    
    
    
}
