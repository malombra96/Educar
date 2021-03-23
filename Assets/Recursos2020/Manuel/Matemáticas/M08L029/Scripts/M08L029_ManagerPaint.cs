using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L029_ManagerPaint : MonoBehaviour
{
    ControlAudio _controlAudio;
    ControlNavegacion _controlNavegacion;
    ControlPuntaje _controlPuntaje;

    [HideInInspector]  public List<M08L029_DropBox> _dropBox;
    [Header("Bottles")] public List<GameObject> _bottles;
    [Header("Button Validar")] public Button _validar;

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        _validar.onClick.AddListener(Validar);
        _validar.interactable = false;
    }

    public void StateButtonValidar()
    {
        bool b = true;

        foreach (M08L029_DropBox box in _dropBox)
        {
            if(string.IsNullOrEmpty(box._current))
            {
                //print($"{box}is{box._current}");
                b = false;
                break;
            }   
        }

        _validar.interactable = b;

    }

    void Validar()
    {
        int right = 0;

        _validar.interactable = false;

        foreach (var b in _bottles)
            b.GetComponent<M08L029_DragBotle>().enabled = false;

        foreach (M08L029_DropBox box in _dropBox)
        { 
            box._symbol.sprite = (box._current == box._right.ToString())? 
                box._symbol.GetComponent<BehaviourSprite>()._right : 
                box._symbol.GetComponent<BehaviourSprite>()._wrong;

            box._symbol.gameObject.SetActive(true);

            if(box._current == box._right.ToString())
                right++;
        }

        _controlAudio.PlayAudio(right==_dropBox.Count? 1 : 2);
        _controlPuntaje.IncreaseScore(right==_dropBox.Count? 1:0 );
        _controlNavegacion.Forward(2);
    }

    public void Reset()
    {
        foreach (var b in _bottles)
            b.GetComponent<M08L029_DragBotle>().enabled = true;

        foreach (M08L029_DropBox box in _dropBox)
        { 
            box.GetComponent<Image>().sprite =  box.GetComponent<BehaviourSprite>()._default;
            box._current = "";
            box._symbol.gameObject.SetActive(false);
        }

        StateButtonValidar();
    }

}
