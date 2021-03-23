using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6A104_toggleGroup : MonoBehaviour
{
    M6A104_managerToggle _managerSeleccionarToggle;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector] public int _sizeCorrect;

    private void Start()
    {
        _managerSeleccionarToggle = transform.parent.GetComponent<M6A104_managerToggle>();
        _managerSeleccionarToggle._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }

    void SetComponentGroupToggle()
    {
        switch (_managerSeleccionarToggle._TypeSelect)
        {
            case M6A104_managerToggle.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    if (transform.GetChild(i).GetComponent<Toggle>() != null) {
                        transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    }
                    

                break;

            case M6A104_managerToggle.TypeSelection.variousxgroup:

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
            if (transform.GetChild(i).GetComponent<Toggle>() != null)
            {
                bool state = transform.GetChild(i).GetComponent<M6A104_toggle>().isRight;

                if (state)
                    c++;
            }
        }

        return c;
    }
}
