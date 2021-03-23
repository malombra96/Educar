using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M4A112_pestañas : MonoBehaviour
{
    ControlAudio _controlAudio;

    [Header("List Toggles")] public List<Toggle> _toggle,_toggleCurrent;
    ToggleGroup _group;
    [Header("List Teoria")] public List<GameObject> _teory;

    [Tooltip("Seleccione si necesita que inicie activa la primer pestaña")]
    [Header("Start Active")]
    public bool startActive;

    public List<Vector2> posToggleIni, posToggleCurrentNext, posToggleCurrentBack;

    public GameObject currentToggle;

    public bool a;

    private void Awake()
    {
        foreach (var toggle in _toggle) {
            posToggleIni.Add(toggle.GetComponent<RectTransform>().anchoredPosition);
        }
        foreach (var toggle in _toggle)
        {
            posToggleCurrentNext.Add(toggle.GetComponent<RectTransform>().anchoredPosition);
        }
        foreach (var toggle in _toggle)
        {
            _toggleCurrent.Add(toggle);
        }
    }
    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        SetFirstTime();
        SetSpriteState();

        _group = _toggle[0].transform.parent.GetComponent<ToggleGroup>();

        foreach (var toggle in _toggle)
            toggle.onValueChanged.AddListener(delegate { SetElementActive(toggle); });

    }

    private void Update()
    {
        if (a) {
            if (currentToggle.GetComponent<RectTransform>().anchoredPosition == 
                new Vector2(currentToggle.GetComponent<M4A112_togglePestaa>().nextPosition.x, currentToggle.GetComponent<M4A112_togglePestaa>().nextPosition.y))
            {
                _toggleCurrent.Clear();
                for (int i = 0; i< _toggle.Count;i++) {
                    if (_toggle[i].GetComponent<M4A112_togglePestaa>().click ) {
                        _toggleCurrent.Add(_toggle[i]);
                    }

                }
                for (int i = 0; i < _toggle.Count; i++)
                {
                    if (!_toggle[i].GetComponent<M4A112_togglePestaa>().noClick && !_toggle[i].GetComponent<M4A112_togglePestaa>().click)
                    {
                        _toggleCurrent.Add(_toggle[i]);
                    }
                }
                for (int i = 0; i < _toggle.Count; i++)
                {
                    if (_toggle[i].GetComponent<M4A112_togglePestaa>().noClick )
                    {
                        _toggleCurrent.Add(_toggle[i]);
                    }
                }

                posToggleCurrentNext.Clear();
                foreach (var toggle in _toggleCurrent)
                {
                    posToggleCurrentNext.Add(toggle.GetComponent<RectTransform>().anchoredPosition);
                }
               
                currentToggle = null;
                foreach (var t in _toggle)
                {
                    t.GetComponent<M4A112_togglePestaa>().backPosition = Vector3.zero;
                    t.GetComponent<M4A112_togglePestaa>().nextPosition = Vector3.zero;
                    t.GetComponent<M4A112_togglePestaa>().noClick = false;
                    t.GetComponent<M4A112_togglePestaa>().click = false;
                }
                a = false;
            }
        }
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

        if (select.isOn) {
            a = true;
            currentToggle = select.gameObject;
            int index = _toggleCurrent.IndexOf(select);
            currentToggle.GetComponent<M4A112_togglePestaa>().nextPosition = posToggleCurrentNext[0];
            currentToggle.GetComponent<M4A112_togglePestaa>().click = true;

            _toggleCurrent[0].GetComponent<M4A112_togglePestaa>().backPosition = posToggleCurrentNext[index];
            _toggleCurrent[0].GetComponent<M4A112_togglePestaa>().noClick = true;

            foreach (var t in _toggle) {
                _teory[_toggle.IndexOf(t)].SetActive(t == @select);
            }
            
        }


        if (!_group.AnyTogglesOn())
            select.isOn = true;
    }

    IEnumerator x() {
        yield return new WaitForSeconds(0.1f);
       

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
