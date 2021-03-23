using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A262_grupoToggle : MonoBehaviour
{
    L5A262_managerSeleccionar _L5A262_managerSeleccionar;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector] public int _sizeCorrect;

    private void Start()
    {
        _L5A262_managerSeleccionar = transform.parent.GetComponent<L5A262_managerSeleccionar>();
        _L5A262_managerSeleccionar._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }

    void SetComponentGroupToggle()
    {
        switch (_L5A262_managerSeleccionar._TypeSelect)
        {
            case L5A262_managerSeleccionar.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();

                break;

            case L5A262_managerSeleccionar.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<L5A262_toggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
