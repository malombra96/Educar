using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L6A278ManagerSeleccion : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;

    public List<Toggle> toggles;
    public GameObject personaje;
    public List<L6A278Drag> drags;
    [HideInInspector] public List<L6A278Drop> drops;
    public Button validar;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();

        foreach (var x in toggles)
            x.onValueChanged.AddListener(delegate { seleccionado(); });

        validar.onClick.AddListener(Calificar);
        validar.interactable = false;
        random();
    }
    void random()
    {
        //yield return new WaitForSeconds(0.1f);
        
        for (int i = 0; i < drops.Count; i++)
        {
            int ran = Random.Range(0, drags.Count);
            var tempDrop = drags[ran].drop;
            var tempDrag = drops[i].drag;
            int index = drops[i].drag.transform.GetSiblingIndex();

            tempDrag.GetComponent<L6A278Drag>().inDrop = true;
            tempDrag.transform.SetSiblingIndex(drags[ran].transform.GetSiblingIndex());
            tempDrag.GetComponent<L6A278Drag>().drop = tempDrop;
            tempDrop.GetComponent<L6A278Drop>().drag = tempDrag;
            tempDrag.GetComponent<RectTransform>().anchoredPosition = tempDrop.GetComponent<RectTransform>().anchoredPosition;
            tempDrag.GetComponent<L6A278Drag>().posDefault = tempDrop.GetComponent<RectTransform>().anchoredPosition;

            drags[ran].inDrop = true;
            drags[ran].transform.SetSiblingIndex(index);
            drags[ran].drop = drops[i].gameObject;
            drops[i].drag = drags[ran].gameObject;
            drags[ran].GetComponent<RectTransform>().anchoredPosition = drops[i].GetComponent<RectTransform>().anchoredPosition;            
            drags[ran].GetComponent<L6A278Drag>().posDefault = drops[i].GetComponent<RectTransform>().anchoredPosition;            
        }
    }
    void seleccionado()
    {
        controlAudio.PlayAudio(0);
        int ensender = 0;
        foreach (var x in toggles)
        {
            if (x.isOn)
            {
                ensender++;
                x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._selection;
            }
            else
                x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._default;
        }
        validar.interactable = (ensender > 0);
    }
    void Calificar()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;
        int right = 0;
        int total = drags.Count + 1;
        foreach (var x in toggles)
        {
            x.interactable = false;
            if (x.isOn)
            {
                if (x.GetComponent<L6A278Toggles>().correcto)
                {
                    right++;
                    controlPuntaje.IncreaseScore();
                    x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._right;
                    personaje.GetComponent<Image>().sprite = personaje.GetComponent<BehaviourSprite>()._right;
                }
                else
                {
                    x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._wrong;
                    personaje.GetComponent<Image>().sprite = personaje.GetComponent<BehaviourSprite>()._wrong;
                }                
            }
        }
        foreach(var drag in drags)
        {
            drag.GetComponent<Image>().raycastTarget = false;
            if (drag.dropRight == drag.drop)
                right++;

            drag.GetComponent<Image>().sprite = 
                (drag.dropRight == drag.drop) ? 
                drag.GetComponent<BehaviourSprite>()._right : 
                drag.GetComponent<BehaviourSprite>()._wrong;
        }        
        controlPuntaje.IncreaseScore(right);
        controlAudio.PlayAudio(right == total ? 1 : 2);        
        controlNavegacion.Forward(2);
    }
    public void ResetAll()
    {
        random();
        validar.interactable = false;
        personaje.GetComponent<Image>().sprite = personaje.GetComponent<BehaviourSprite>()._wrong;
        foreach (var x in toggles)
        {
            x.isOn = false;
            x.interactable = true;
            x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._default;
        }
        foreach (var drag in drags)
        {
            drag.GetComponent<Image>().raycastTarget = true;
            drag.GetComponent<Image>().sprite = drag.GetComponent<BehaviourSprite>()._default;
        }
        
    }
}
