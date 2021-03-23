using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08A050T_BehaviourToggleGroup : MonoBehaviour
{
    M08A050T_ManagerSeleccionarToggle _M08A050T_ManagerSeleccionarToggle;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector]  public int _sizeCorrect;

    private void Start()
    {
        if(_M08A050T_ManagerSeleccionarToggle)
        {
            _M08A050T_ManagerSeleccionarToggle = transform.parent.GetComponent<M08A050T_ManagerSeleccionarToggle>();
            _M08A050T_ManagerSeleccionarToggle._groupToggle.Add(this);
            SetComponentGroupToggle();
        }
        
        
        _sizeCorrect = GetSizeCorrect();

    }
    
    void SetComponentGroupToggle()
    {
        switch (_M08A050T_ManagerSeleccionarToggle._TypeSelect)
        {
            case M08A050T_ManagerSeleccionarToggle.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    
                break;

            case M08A050T_ManagerSeleccionarToggle.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<M08A050T_BehaviourToggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
