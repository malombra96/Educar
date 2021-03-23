
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class M6L116Manager : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    [HideInInspector] public List<M6L116Drag> Drags;
    [Range(0, 2)] public int numeroValidar;

    public Button validar;
    
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        validar.onClick.AddListener(calificar);
        validar.interactable = false;
    }
    public IEnumerator activarValidar()
    {
        yield return new WaitForSeconds(0.1f);
        int right = 0;
        foreach(var drag in Drags)
        {
            if (drag.inDrop)
                right++;
        }
        if (numeroValidar != 0)
            validar.interactable = (right == numeroValidar);
        else
            validar.interactable = (right == Drags.Count);
    }
    void calificar()
    {
        int i = 0;
        controlAudio.PlayAudio(0);
        validar.interactable = false;
        
        foreach (var drag in Drags)
        {
            drag.GetComponent<CanvasGroup>().blocksRaycasts = false;
            if (drag.inDrop)
            {
                if (drag.dropCorrecto)
                {
                    if (drag.dropCorrecto.gameObject.name == drag.drop.gameObject.name)
                    {
                        i++;
                        drag.GetComponent<Image>().sprite = drag.GetComponent<BehaviourSprite>()._right;
                    }
                    else
                        drag.GetComponent<Image>().sprite = drag.GetComponent<BehaviourSprite>()._wrong;
                }
                else
                    drag.GetComponent<Image>().sprite = drag.GetComponent<BehaviourSprite>()._wrong;
            }
        }
        if (numeroValidar == 0)
        {
            controlAudio.PlayAudio((i == Drags.Count) ? 1 : 2);            
            if (i == Drags.Count)
                controlPuntaje.IncreaseScore();
        }
        else
        {
            controlAudio.PlayAudio(i == numeroValidar ? 1 : 2);
            if (i == numeroValidar)
                controlPuntaje.IncreaseScore();
        }
        controlNavegacion.Forward(2);
    }
    public void resetAll()
    {
        validar.interactable = false;
        foreach (var drag in Drags)
        {
            drag.drop = null;
            drag.inDrop = false;
            drag.posFinal = Vector2.zero;
            drag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            drag.GetComponent<Image>().sprite = drag.GetComponent<BehaviourSprite>()._default;
            if (drag.cambioTransform)
                drag.transform.SetParent(drag.padre);
            drag.GetComponent<RectTransform>().anchoredPosition = drag.posDefault;
        }
    }
}
