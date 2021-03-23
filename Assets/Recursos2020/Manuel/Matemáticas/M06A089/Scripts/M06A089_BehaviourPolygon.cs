using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M06A089_BehaviourPolygon : MonoBehaviour
{
    ControlAudio _controlAudio;
    
    [Header("List Toggles")] public List<Toggle> _toggle;
    [Header("List Teoria")] public List<GameObject> _teory;

    [Header("Draw Polygon")] public Button _draw;

    [Header("Polygon Shape")] public Image _polygon;

    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _draw.onClick.AddListener( delegate{ShowPolygon(true);});

        foreach (var toggle in _toggle)
        {
            toggle.onValueChanged.AddListener(delegate { SetElementActive(toggle); });
            toggle.isOn = false;
        }
            
        foreach (GameObject t in _teory)
            t.SetActive(false);

        _polygon.gameObject.SetActive(false);

        SetDrawButton();
    }

    void SetElementActive(Toggle select)
    {
        SetSpriteState();
        SetDrawButton();

        if (select.isOn)
        {
            _controlAudio.PlayAudio(0);
            
            _teory[_toggle.IndexOf(select)].SetActive(true);
        }   
        else
        {
            int n = _toggle.IndexOf(select);
            _toggle[n].isOn = false;
            _teory[n].SetActive(false);
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

    void SetDrawButton()
    {
        bool state = true;

        foreach (var toggle in _toggle)
        {
            if(!toggle.isOn)
            {   
                state = false;
                ShowPolygon(false);
                break;
            }
                
        }

        _draw.interactable = state;
    }

    void ShowPolygon(bool state)
    {
        if(state)
            _controlAudio.PlayAudio(0);

        _polygon.gameObject.SetActive(state);
    }

}
