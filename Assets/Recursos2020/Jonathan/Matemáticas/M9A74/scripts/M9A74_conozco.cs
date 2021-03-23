using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M9A74_conozco : MonoBehaviour
{


    ControlAudio _controlAudio;

    [Header("List Toggles")] public List<Toggle> _toggle,_toggles;
    [Header("List Teoria")] public List<GameObject> _teory;

    [Tooltip("Seleccione si necesita que inicie activa la primer pestaña")]
    [Header("Start Active")]
    public bool startActive;
    public ToggleGroup _group;

    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

       

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
            foreach (var t in _toggle)
                _teory[_toggle.IndexOf(t)].SetActive(t == @select);

        if (!_group.AnyTogglesOn())
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
