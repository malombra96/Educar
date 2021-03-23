using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M4A112_line : MonoBehaviour
{
    public ManagerDrawLine manager;
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] [Header("Lista de lineas creadas")] public List<Line> _lines;
    [Header("Arrastre el boton validar")] public Button _validar;
    public GameObject napoleon;
    private void Start()
    {
        
        _controlAudio = FindObjectOfType<ControlAudio>();
        _validar.onClick.AddListener(ValidarLines);

    }
    void ValidarLines()
    {
        _validar.interactable = false;
        
        _controlAudio.PlayAudio(0);
        int correct = 0;

        foreach (var line in manager._lines)
        {
            var x = line.toggle[0].GetComponent<BehaviourDrawToggle>();
            var y = line.toggle[1].GetComponent<BehaviourDrawToggle>();

            if (x._match.Contains(line.toggle[1]) || y._match.Contains(line.toggle[0]))
            {
                

                correct++;
            }
            else
            {
                
            }

        }

      


        if (correct == manager._lines.Count)
        {
            napoleon.GetComponent<Image>().sprite = napoleon.GetComponent<BehaviourSprite>()._right;
        }
        else {
            napoleon.GetComponent<Image>().sprite = napoleon.GetComponent<BehaviourSprite>()._wrong;
        }
    }

    public void ResetAll() {
        napoleon.GetComponent<Image>().sprite = napoleon.GetComponent<BehaviourSprite>()._default;
    }
}
