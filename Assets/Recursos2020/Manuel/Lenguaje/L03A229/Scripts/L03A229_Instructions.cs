using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class L03A229_Instructions : MonoBehaviour
{
    [Header("Current Avatar")] public Sprite _avatar;
    [Header("Comenzar BTN")] public Button _comenzar;

    [Header("List Toggles")] public List<Toggle> _toggle;

    ToggleGroup _toggleGroup;


    void OnEnable()
    {
        _toggleGroup = GetComponent<ToggleGroup>();

        _toggleGroup.SetAllTogglesOff();

        _comenzar.interactable = false;

        foreach (var toggle in _toggle)
            toggle.onValueChanged.AddListener(delegate { SetStates(); });
    }

    void SetStates()
    {
        _comenzar.interactable = _toggleGroup.AnyTogglesOn();
        IEnumerable<Toggle> t = _toggleGroup.ActiveToggles();
        List<Toggle> active = t.ToList();

        _avatar = (active.Count > 0)? active[0].GetComponent<Image>().sprite : null;
        
        SetSpriteState();
    } 

    /// <summary>
    /// Change sprite select toggle
    /// </summary>
    void SetSpriteState()
    {
        foreach (var t in _toggle)
        {
            t.GetComponent<Image>().sprite = t.isOn
                ? t.GetComponent<BehaviourSprite>()._selection
                : t.GetComponent<BehaviourSprite>()._default;
        }
    }
    
}
