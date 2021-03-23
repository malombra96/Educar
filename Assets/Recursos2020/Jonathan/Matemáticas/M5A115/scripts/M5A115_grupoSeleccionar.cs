using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M5A115_grupoSeleccionar : MonoBehaviour
{
    M5A115_managerSeleccionar _M5A115_managerSeleccionar;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector] public int _sizeCorrect;

    private void Start()
    {
        _M5A115_managerSeleccionar = transform.parent.GetComponent<M5A115_managerSeleccionar>();
        _M5A115_managerSeleccionar._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }

    void SetComponentGroupToggle()
    {
        switch (_M5A115_managerSeleccionar._TypeSelect)
        {
            case M5A115_managerSeleccionar.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();

                break;

            case M5A115_managerSeleccionar.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<M5A115_toggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
