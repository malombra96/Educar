using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A095_ViewLightBox : MonoBehaviour
{
    [Header("Activity Layout")] public List<GameObject> _managerLayout;

    ControlNavegacion _controlNavegacion;
    ControlAudio _controlAudio;
    private GameObject _navBar;
    
    [Header("LightBox")] public List<GameObject> _lighBox;

    [Header("BTN ViewChart")] public Button _button;

    int count;

    public Text _countText;

    GameObject _current;
    int _indexCurrent;
    
    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        if(FindObjectOfType<BehaviourNavBar>())
            _navBar = FindObjectOfType<BehaviourNavBar>().gameObject;
        else
            _navBar = FindObjectOfType<BehaviourNavBarLeccion>().gameObject;

        
        count = 3;
        SetCount();
        
        _button.onClick.AddListener(delegate { SetActiveElement(); });


        foreach (var l in _lighBox)
            l.SetActive(false);
            
    }

    void LateUpdate()
    {
        _current = _controlNavegacion.GetLayoutActual();

        if(_current)
        {
            if(_managerLayout.Contains(_current))
                _indexCurrent = _managerLayout.IndexOf(_current);

            _button.gameObject.SetActive(_managerLayout.Contains(_current));
        }
    }

    void SetActiveElement()
    {
        if(count > 0)
        {
            _controlAudio.PlayAudio(0);
            _navBar.gameObject.SetActive(false);
            _lighBox[_indexCurrent].SetActive(true);
            count--;
            SetCount();
        }

        _button.interactable = (count != 0);
    }

    void SetCount() => _countText.text = count.ToString();

    public void CloseElement()
    {
        _controlAudio.PlayAudio(0);

        _navBar.gameObject.SetActive(true);

        foreach (var box in _lighBox)
            if(box.activeSelf)
                box.SetActive(false);
    }

    public void Reset()
    {
        count = 3;
        SetCount();
        _button.interactable = (count != 0);
    }
}
