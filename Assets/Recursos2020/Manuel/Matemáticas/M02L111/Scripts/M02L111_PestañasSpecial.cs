using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M02L111_PestañasSpecial : MonoBehaviour
{
    ControlAudio _controlAudio;

    [Header("LayoutBackground")] public List<Sprite> _sprite;
    
    [Header("List Toggles")] public List<Toggle> _toggle;
    ToggleGroup _group;
    [Header("List Teoria")] public List<GameObject> _teory;

    [Tooltip("Seleccione si necesita que inicie activa la primer pestaña")] [Header("Start Active")]
    public bool startActive;

    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        SetFirstTime();
        SetSpriteState();

        _group = _toggle[0].transform.parent.GetComponent<ToggleGroup>();
        GetComponent<Image>().sprite = _sprite[0];
        
        foreach (var toggle in _toggle)
            toggle.onValueChanged.AddListener(delegate { SetElementActive(toggle); });
        
    }

    /// Configura el estado inicial del layout 
    void SetFirstTime()
    {
        foreach (var x in _teory)
            x.SetActive(false);

        if (startActive)
        {
            _toggle[0].isOn = true;
            _teory[0].SetActive(true);
        }
    }

    void SetElementActive(Toggle select)
    {
        _controlAudio.PlayAudio(0);
        
        SetSpriteState();
        
        if (select.isOn)
        {
            foreach (var t in _toggle)
                _teory[_toggle.IndexOf(t)].SetActive(t == @select);

            GetComponent<Image>().sprite = _sprite[_toggle.IndexOf(select)];
        }
            

        

        if(!_group.AnyTogglesOn())
            select.isOn = true;
        
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
