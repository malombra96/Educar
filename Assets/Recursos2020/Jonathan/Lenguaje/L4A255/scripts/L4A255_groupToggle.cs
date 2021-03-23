using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class L4A255_groupToggle : MonoBehaviour
{
    L4A255_managerToggle _L4A255_managerToggle;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector] public int _sizeCorrect;

    private void Start()
    {
        _L4A255_managerToggle = transform.parent.GetComponent<L4A255_managerToggle>();
        _L4A255_managerToggle._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }

    void SetComponentGroupToggle()
    {
        switch (_L4A255_managerToggle._TypeSelect)
        {
            case L4A255_managerToggle.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();

                break;

            case L4A255_managerToggle.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<L4A255_toggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
