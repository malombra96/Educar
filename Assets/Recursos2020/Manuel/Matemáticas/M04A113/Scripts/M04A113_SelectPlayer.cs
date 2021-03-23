using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class M04A113_SelectPlayer : MonoBehaviour
{
    [Header("Manager Worlds")] public M04A113_ManagerWorlds _managerWorlds;
    ControlAudio _controlAudio;
    ControlNavegacion _controlNavegacion;
    [Header("Current Avatar")] public string _currentAvatar;
    [Header("Comenzar BTN")] public Button _comenzar;

    [Header("List Toggles")] public List<Toggle> _toggle;

    ToggleGroup _toggleGroup;


    void OnEnable()
    {
        _toggleGroup = GetComponent<ToggleGroup>();

        _toggleGroup.SetAllTogglesOff();

        _comenzar.interactable = false;

    }

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        foreach (var toggle in _toggle)
            toggle.onValueChanged.AddListener(delegate { SetStates(); });

        _comenzar.onClick.AddListener(SaveSelectPlayer);
    }

    void SetStates()
    {
        _comenzar.interactable = _toggleGroup.AnyTogglesOn();
        IEnumerable<Toggle> t = _toggleGroup.ActiveToggles();
        List<Toggle> active = t.ToList();

        _currentAvatar = (active.Count > 0)? active[0].gameObject.name : null;

        _controlAudio.PlayAudio(0);
        
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

    void SaveSelectPlayer()
    {
        _controlAudio.PlayAudio(0);

        foreach (GameObject prefab in _managerWorlds._prefabsPlayers)
        {
            if(prefab.name == _currentAvatar)
                _managerWorlds._playerSelection = prefab;
        }

        _controlNavegacion.Forward(1);
        
    }

    public void ResetPlayer()
    {
        foreach (var t in _toggle)
            t.isOn = false;

        _currentAvatar = "";
        _managerWorlds._playerSelection = null;

        
    }
}
