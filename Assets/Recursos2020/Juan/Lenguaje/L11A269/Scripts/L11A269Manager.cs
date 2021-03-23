using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L11A269Manager : MonoBehaviour
{
    [HideInInspector]public ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    public List<Toggle> toggles;
    GameObject temp = null;    
    int right;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();       
    }
    
    public void Calificar(GameObject g)
    {        
        controlAudio.PlayAudio(0);
        
        g.GetComponent<Animator>().Play("AbrirCofre");
        g.GetComponent<Toggle>().interactable = false;

        if (temp == null)
            temp = g;
        else
        {
            foreach (var toggle in toggles)
                toggle.GetComponent<Toggle>().interactable = false;

            if (temp.GetComponent<L11A269Seleccion>().hermano == g)
            {
                right++;
                StartCoroutine(temp.GetComponent<L11A269Seleccion>().StateClificar(temp.transform.GetChild(0).GetComponent<Image>(), true));
                StartCoroutine(g.GetComponent<L11A269Seleccion>().StateClificar(g.transform.GetChild(0).GetComponent<Image>(), true));
                controlPuntaje.IncreaseScore();
                temp.GetComponent<Animator>().enabled = false;                

                StartCoroutine(desactivarAnimator(g.GetComponent<Animator>()));                
            }
            else
            {
                temp.GetComponent<Toggle>().isOn = false;
                g.GetComponent<Toggle>().isOn = false;
                StartCoroutine(temp.GetComponent<L11A269Seleccion>().StateClificar(temp.transform.GetChild(0).GetComponent<Image>(), false));
                StartCoroutine(g.GetComponent<L11A269Seleccion>().StateClificar(g.transform.GetChild(0).GetComponent<Image>(), false));
            }
            
            StartCoroutine(reActivar(temp.GetComponent<L11A269Seleccion>().hermano == g));
            temp = null;            
        }

        if (right == (toggles.Count / 2))                    
            controlNavegacion.Forward(3);                    
    }
    IEnumerator desactivarAnimator(Animator a)
    {
        yield return new WaitForSeconds(1);
        a.GetComponent<Animator>().enabled = false;
    }
    IEnumerator reActivar(bool b)
    {
        yield return new WaitForSeconds(1f);
        controlAudio.PlayAudio(b ? 1 : 2);

        yield return new WaitForSeconds(3.1f);
        foreach (var toggle in toggles)
        {
            if (toggle.GetComponent<Toggle>().isOn)            
                toggle.GetComponent<Toggle>().interactable = false;
            else
                toggle.GetComponent<Toggle>().interactable = true;
        }
    }
    public void modoRevicion()
    {
        foreach (var toggle in toggles)
        {
            toggle.GetComponent<Animator>().enabled = true;
            toggle.GetComponent<L11A269Seleccion>().inicio = false;            
        }
    }
    public void resetAll()
    {
        right = 0;
        temp = null;
        foreach (var toggle in toggles)
        {
            toggle.GetComponent<Animator>().enabled = true;            
            toggle.GetComponent<L11A269Seleccion>().inicio = true;
            toggle.transform.GetChild(0).GetComponent<Image>().sprite = toggle.transform.GetChild(0).GetComponent<BehaviourSprite>()._default;
        }
    }    
}
