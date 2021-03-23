using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_StepA : MonoBehaviour
{
    M08L037T_ManagerStep _managerStep;
    ControlAudio _controlAudio;
    public InputField _inecuacion;

    public GameObject _advertencia_A;
   
    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _managerStep = FindObjectOfType<M08L037T_ManagerStep>();
    }

    void Start()
    {
        _inecuacion.onValueChanged.AddListener(delegate{_managerStep.SetWarning(_advertencia_A,false);});

        if(!Application.isMobilePlatform)
            _inecuacion.onEndEdit.AddListener(delegate{ CheckStep_1(_inecuacion); });
        else
        {
            Button close =_inecuacion.GetComponent<M08L037T_BehaviourInputField>()._lightBox.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>();
            close.onClick.AddListener(delegate{CheckStep_1(_inecuacion);});
        }
        
        
    }

    public void CheckStep_1(InputField i)
    {
        if (!_managerStep._inReset && i.GetComponent<M08L037T_BehaviourInputField>()._isEnabled && isActiveAndEnabled)
        {
            i.text = DeleteEmptyChar(i.text.ToLower());

            if (i.text == _managerStep._igualdad)
            {
                _managerStep.SetAnswerSprite(i.GetComponent<Image>(), true);
                i.interactable = false;
                i.GetComponent<M08L037T_BehaviourInputField>()._isEnabled = false;
                _controlAudio.PlayAudio(1);
                _managerStep.SetNextStep(1);
            }
            else
            {
                _controlAudio.PlayAudio(2);
                _managerStep.SetAnswerSprite(i.GetComponent<Image>(), false);
                _advertencia_A.SetActive(true);
            }
        }
    }

    string DeleteEmptyChar(string s)
    {
        string[] xs = s.ToCharArray().Select(c => c.ToString()).ToArray();

        for (int i = 0; i < xs.Length; i++)
            if(string.IsNullOrWhiteSpace(xs[i]))
                s = s.Replace(xs[i],string.Empty);


        return s; 
    }

    public void ResetA()
    {
        _inecuacion.GetComponent<Image>().sprite = _inecuacion.GetComponent<BehaviourSprite>()._default;
        _inecuacion.text = "";
        _inecuacion.interactable = true;
        _inecuacion.GetComponent<M08L037T_BehaviourInputField>()._isEnabled = true;
    }

}
