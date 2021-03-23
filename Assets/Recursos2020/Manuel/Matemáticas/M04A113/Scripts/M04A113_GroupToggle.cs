using UnityEngine;
using UnityEngine.UI;

public class M04A113_GroupToggle : MonoBehaviour
{
    public M04A113_ManagerToggle _managerSeleccionarToggle;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector]  public int _sizeCorrect;

    private void Start()
    {
        _managerSeleccionarToggle._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }
    
    void SetComponentGroupToggle()
    {
        switch (_managerSeleccionarToggle._TypeSelect)
        {
            case M04A113_ManagerToggle.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    
                break;

            case M04A113_ManagerToggle.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<M04A113_Toggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
