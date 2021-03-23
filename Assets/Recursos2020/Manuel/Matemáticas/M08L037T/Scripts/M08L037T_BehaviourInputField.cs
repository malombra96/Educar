using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class M08L037T_BehaviourInputField : MonoBehaviour,IPointerClickHandler
{
    ControlAudio _controlAudio;
    [Header("LightBox")] public GameObject _lightBox;
    InputField _input;
    [Header("State Interaction")] public bool _isEnabled;

    void Awake()
    {
        _isEnabled = true;

        _input = GetComponent<InputField>();
        _controlAudio = FindObjectOfType<ControlAudio>();

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Application.isMobilePlatform && _isEnabled)
        {
            _controlAudio.PlayAudio(0);

            ManagerDisplay _managerDisplay = _lightBox.transform.GetChild(0).GetChild(0).GetComponent<ManagerDisplay>();

            _managerDisplay.currentInput = GetComponent<InputField>();
            _managerDisplay.limiteCaracteres = GetComponent<InputField>().characterLimit;

            _lightBox.SetActive(true);
            _lightBox.GetComponent<Animator>().Play("NumPad_in");
        }
    }
}
