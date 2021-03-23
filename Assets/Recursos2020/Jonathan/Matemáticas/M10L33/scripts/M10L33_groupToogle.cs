using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_groupToogle : MonoBehaviour
{

   [SerializeField] M10L33_managerSeleccionar _managerSeleccionarToggle;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector] public int _sizeCorrect;

    private void Start()
    {
        //_managerSeleccionarToggle = transform.parent.GetComponent<M10L33_managerSeleccionar>();
        _managerSeleccionarToggle._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }

    void SetComponentGroupToggle()
    {
        switch (_managerSeleccionarToggle._TypeSelect)
        {
            case M10L33_managerSeleccionar.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++) {
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                }
                

                break;

            case M10L33_managerSeleccionar.TypeSelection.variousxgroup:

                for (int i = 0; i < transform.childCount; i++) {
                    transform.GetChild(i).GetComponent<Toggle>().group = null;
                }
                    

                break;
        }
    }

    int GetSizeCorrect()
    {
        int c = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            bool state = transform.GetChild(i).GetComponent<M10L33_seleccionar>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
