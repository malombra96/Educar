using UnityEngine;
using UnityEngine.UI;

public class M06A089_GroupToggle : MonoBehaviour
{
    M06A089_ManagerToggle _managerSeleccionarToggle;

    [HideInInspector] public DictionaryToggle _dictionarySelection = new DictionaryToggle();

    [HideInInspector]  public int _sizeCorrect;

    private void Start()
    {
        _managerSeleccionarToggle = transform.parent.GetComponent<M06A089_ManagerToggle>();
        _managerSeleccionarToggle._groupToggle.Add(this);
        SetComponentGroupToggle();
        _sizeCorrect = GetSizeCorrect();

    }
    
    void SetComponentGroupToggle()
    {
        switch (_managerSeleccionarToggle._TypeSelect)
        {
            case M06A089_ManagerToggle.TypeSelection.onexgroup:

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
                    
                break;

            case M06A089_ManagerToggle.TypeSelection.variousxgroup:

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
            bool state = transform.GetChild(i).GetComponent<M06A089_Toggle>().isRight;

            if (state)
                c++;
        }

        return c;
    }

    public void SetJoystick()
    {
        if(_dictionarySelection._toggle.Count > 0)
        {
            if(_dictionarySelection._toggle[0].GetComponent<RectTransform>().anchoredPosition.x > 0)
                _managerSeleccionarToggle._joystick.sprite = _managerSeleccionarToggle._joystick.GetComponent<BehaviourSprite>()._disabled;
            else if(_dictionarySelection._toggle[0].GetComponent<RectTransform>().anchoredPosition.x < 0)
                _managerSeleccionarToggle._joystick.sprite = _managerSeleccionarToggle._joystick.GetComponent<BehaviourSprite>()._selection;
            else
                _managerSeleccionarToggle._joystick.sprite = _managerSeleccionarToggle._joystick.GetComponent<BehaviourSprite>()._default;
        }
        else
            _managerSeleccionarToggle._joystick.sprite = _managerSeleccionarToggle._joystick.GetComponent<BehaviourSprite>()._default;
    }
}
