using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A51_group : MonoBehaviour
{

    public M8A51_managerInput _managerInputField;
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
                        _inputFields[i].GetComponent<BehaviourInputField>()._isRight = true;
                        temporary.RemoveAt(j);
                        break;
                    }
                }
                else
                {
                    _inputFields[i].GetComponent<BehaviourInputField>()._isRight = false;
                    break;
                }

            }
        }

    }

}
