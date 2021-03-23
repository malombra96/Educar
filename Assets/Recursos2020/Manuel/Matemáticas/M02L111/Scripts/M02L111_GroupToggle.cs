using UnityEngine;
using UnityEngine.UI;

public class M02L111_GroupToggle : MonoBehaviour
{
    M02L111_ManagerToggle _managerSeleccionarToggle;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector]  public int _sizeCorrect;

    private void Start()
    {
        _managerSeleccionarToggle = transform.parent.GetComponent<M02L111_ManagerToggle>();
        _managerSeleccionarToggle._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }
    
    void SetComponentGroupToggle()
    {
        switch (_managerSeleccionarToggle._TypeSelect)
        {
            case M02L111_ManagerToggle.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    
                break;

            case M02L111_ManagerToggle.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<M02L111_Toggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
