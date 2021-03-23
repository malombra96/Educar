using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10A190_groupSeleccionar : MonoBehaviour
{
    L10A190_managerSeleccionar _L10A190_managerSeleccionar;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector] public int _sizeCorrect;

    private void Start()
    {
        _L10A190_managerSeleccionar = transform.parent.GetComponent<L10A190_managerSeleccionar>();
        _L10A190_managerSeleccionar._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }

    void SetComponentGroupToggle()
    {
        switch (_L10A190_managerSeleccionar._TypeSelect)
        {
            case L10A190_managerSeleccionar.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();

                break;

            case L10A190_managerSeleccionar.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<L10A190_toggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
