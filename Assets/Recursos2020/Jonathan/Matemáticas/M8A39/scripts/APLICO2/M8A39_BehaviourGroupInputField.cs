using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A39_BehaviourGroupInputField : MonoBehaviour
{
    public M8A39_ManagerInputField _managerInputField;
    public List<InputField> _inputFields;

    [Header("Respuestas correctas del grupo")] public List<string> _respuestasGrupo;

    public enum TipoRespuestas
    {
        individual,
        grupo
    }

    [Header("Cuales respuestas evaluar")] public TipoRespuestas _TipoRespuestas;

    void Awake()
    {
        
        

        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).GetComponent<InputField>())
                _inputFields.Add(transform.GetChild(i).GetComponent<InputField>());
    }

    public void EvaluateGroup()
    {
        List<string> temporary = new List<string>(_respuestasGrupo);

        for (int i = 0; i < _inputFields.Count; i++)
        {
            for (int j = 0; j < temporary.Count; j++)
            {
                if (temporary.Contains(_inputFields[i].text))
                {
                    if (_inputFields[i].text == temporary[j])
                    {
                        _inputFields[i].GetComponent<M8A39_input>()._isRight = true;
                        temporary.RemoveAt(j);
                        break;
                    }
                }
                else
                {
                    _inputFields[i].GetComponent<M8A39_input>()._isRight = false;
                    break;
                }

            }
        }

    }

}
