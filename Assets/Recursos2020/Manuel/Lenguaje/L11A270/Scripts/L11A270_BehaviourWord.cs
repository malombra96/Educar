using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class L11A270_BehaviourWord : MonoBehaviour
{
    ControlAudio _controlAudio;
    [Header("Drag Asociado")] public GameObject _dragAssociate;

    void Start() => _controlAudio = FindObjectOfType<ControlAudio>();

    [Header("is seen?")] public bool seen;
    [Header("is selected?")] public bool selected;

    public bool onlyOnce = true;
    
    public void SetSpriteState()
    {
        if(!selected)
        {
            if(seen)
                GetComponent<MeshRenderer>().material.SetTexture("_MainTex",GetComponent<BehaviourSprite>()._selection.texture);
            else
                GetComponent<MeshRenderer>().material.SetTexture("_MainTex",GetComponent<BehaviourSprite>()._default.texture);
        }
        else
        {
            if (onlyOnce)
            {
                onlyOnce = !onlyOnce;
                _controlAudio.PlayAudio(0);
                GetComponent<MeshRenderer>().material.SetTexture("_MainTex", GetComponent<BehaviourSprite>()._disabled.texture);
                _dragAssociate.SetActive(true);
                //gameObject.SetActive(false);
            }
            
        }
    }
}
