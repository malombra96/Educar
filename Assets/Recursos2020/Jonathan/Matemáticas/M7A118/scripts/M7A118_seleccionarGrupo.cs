using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class M7A118_seleccionarGrupo : MonoBehaviour
{
    M7A118_managerSeleccionar _M7A118_managerSeleccionar;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector] public int _sizeCorrect;

    private void Start()
    {
        _M7A118_managerSeleccionar = transform.parent.GetComponent<M7A118_managerSeleccionar>();
        _M7A118_managerSeleccionar._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }

    void SetComponentGroupToggle()
    {
        switch (_M7A118_managerSeleccionar._TypeSelect)
        {
            case M7A118_managerSeleccionar.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++) {
                    if (transform.GetChild(i).GetComponent<Toggle>()) {
                        transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    }
                    

                }
                

                break;

            case M7A118_managerSeleccionar.TypeSelection.variousxgroup:

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
            if (transform.GetChild(i).GetComponent<M7A118_toggle>()) {
                bool state = transform.GetChild(i).GetComponent<M7A118_toggle>().isRight;
                if (state)
                    c++;
            }
            

            
        }

        return c;
    }
}
