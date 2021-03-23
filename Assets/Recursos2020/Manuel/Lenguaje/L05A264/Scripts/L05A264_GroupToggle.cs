using UnityEngine;
using UnityEngine.UI;

public class L05A264_GroupToggle : MonoBehaviour
{
    public L05A264_ManagerToggle _managerSeleccionarToggle;

    /* [HideInInspector] */ public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector]  public int _sizeCorrect;

    private void Start()
    {
        /* _managerSeleccionarToggle = transform.parent.GetComponent<L05A264_ManagerToggle>();
        _managerSeleccionarToggle._groupToggle.Add(this); */
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }
    
    void SetComponentGroupToggle()
    {
        switch (_managerSeleccionarToggle._TypeSelect)
        {
            case L05A264_ManagerToggle.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    
                break;

            case L05A264_ManagerToggle.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<L05A264_Toggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }
}
