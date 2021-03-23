using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09L080_BehaviourToggleGroup : MonoBehaviour
{
    M09L080_ManagerSeleccionarToggle _managerSeleccionarToggle;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector]  public int _sizeCorrect;

    private void Awake()
    {
        _managerSeleccionarToggle = transform.parent.GetComponent<M09L080_ManagerSeleccionarToggle>();
        _managerSeleccionarToggle._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }
    
    void SetComponentGroupToggle()
    {
        switch (_managerSeleccionarToggle._TypeSelect)
        {
            case M09L080_ManagerSeleccionarToggle.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    
                break;

            case M09L080_ManagerSeleccionarToggle.TypeSelection.variousxgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = null;

                break;
        }
    }
    
    int GetSizeCorrect()
    {
        int c = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            bool state = transform.GetChild(i).GetComponent<M09L080_BehaviourToggle>().isRight;

            if (state)
            {
                c++;
                _managerSeleccionarToggle._toggleRight = transform.GetChild(i).GetComponent<M09L080_BehaviourToggle>();
            }   
                
        }

        return c;
    }

}
