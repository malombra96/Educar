using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M06A086_ManagerPopUp : MonoBehaviour
{
    M06A086_ManagerDraw _ManagerDraw;
    ControlAudio _controlAudio;
    
    [Header("List Toggles")] public List<Toggle> _toggle;
    [Header("List Teoria")] public List<GameObject> _teory;

    [Tooltip("Seleccione si necesita que inicie activo el primer popup")] [Header("Start Active")] public bool startActive;

    private void Start()
    {   
        _ManagerDraw = FindObjectOfType<M06A086_ManagerDraw>();

        _controlAudio = FindObjectOfType<ControlAudio>();

        SetFirstTime();

        foreach (var toggle in _toggle)
            toggle.onValueChanged.AddListener(delegate { SetElementActive(toggle); });
    }

    /// Configura el estado inicial del layout 
    void SetFirstTime()
    {
        foreach (var x in _teory)
            x.SetActive(false);

        if(startActive)
        {
            _toggle[0].isOn = true;
            _teory[0].SetActive(true);
            SetSpriteState();
        }
    }

    void SetElementActive(Toggle select)
    {
        SetSpriteState();

        if (select.isOn)
        {
            _controlAudio.PlayAudio(0);
            
            foreach (var t in _toggle)
                _teory[_toggle.IndexOf(t)].SetActive(t == @select);

            foreach (Line line in _ManagerDraw._lines)
                line.lineRender.gameObject.SetActive(false);
        }   
        else
        {
            int n = _toggle.IndexOf(select);
            _toggle[n].isOn = false;
            _teory[n].SetActive(false);

            foreach (Line line in _ManagerDraw._lines)
                line.lineRender.gameObject.SetActive(true);
        }
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

    public void ClosePopUp()
    {
        _controlAudio.PlayAudio(0);
        
        foreach (var toggle in _toggle)
        {
            toggle.isOn = false;
            _teory[_toggle.IndexOf(toggle)].SetActive(false);
            
        }

        foreach (Line line in _ManagerDraw._lines)
                line.lineRender.gameObject.SetActive(true);
    }
}
