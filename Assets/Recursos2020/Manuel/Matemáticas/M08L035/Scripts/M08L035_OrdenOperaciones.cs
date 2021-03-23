using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L035_OrdenOperaciones : MonoBehaviour
{
    ControlAudio _controlAudio;
    [Header("Steps")] public List<Toggle> _steps;
    [HideInInspector] public List<GameObject> _teory;

    [Header("Pestañas Group")] public Transform _group;

    Transform x;
    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        foreach (GameObject t in _teory)
            t.SetActive(false);

        foreach (Toggle step in _steps)
            step.onValueChanged.AddListener(delegate{ SetStep(step);});
            
    }   

    void SetStep(Toggle t)
    {
        int index = _steps.IndexOf(t);
        index+=1;

        if(t.isOn)
        {
            _controlAudio.PlayAudio(0);
            
            for (int i = 0; i < _teory.Count; i++)
                _teory[i].SetActive(i <= index);
        }

        SetSpriteState(t.GetComponent<Image>(),t.isOn);
        
    }
    
    void LateUpdate()
    {
        for (int i = 0; i < _group.childCount; i++)
        {
            if(_group.GetChild(i).gameObject.activeSelf && _group.GetChild(i) != x)
            {
                x = _group.GetChild(i);

                foreach (Toggle step in _steps)
                    step.isOn = false;

                foreach (GameObject t in _teory)
                    t.SetActive(false);

                _teory.Clear();
                
                for (int j = 0; j < x.childCount; j++)
                    _teory.Add(x.GetChild(j).gameObject);
            }
        }
             
    }

    void SetSpriteState(Image image,bool state)
    {
        image.sprite =  state? 
            image.GetComponent<BehaviourSprite>()._selection : 
            image.GetComponent<BehaviourSprite>()._default;
    }

}
