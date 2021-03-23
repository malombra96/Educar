using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M02A110_ManagerRompecabezas : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    public List<BehaviourLayout> _layouts;
    BehaviourLayout _current;
    GameObject _rompecabezas;

    int count;

    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _rompecabezas = transform.GetChild(0).gameObject;

        count = 0;
    }

    void LateUpdate()
    {
        _current = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>();

        if(_current)
        {
            _rompecabezas.SetActive(_layouts.Contains(_current));

            if(_current == _layouts[_layouts.Count-1])
                SetResultultado();
        }
            
    }

    public void SetPiece(int index,bool state)
    {
        count++;
        _rompecabezas.transform.GetChild(index).gameObject.SetActive(state);
    }

    void SetResultultado()
    {
        bool state = count == (_layouts.Count-1);

        _current.GetComponent<Image>().sprite = state?
            _current.GetComponent<BehaviourSprite>()._right:
            _current.GetComponent<BehaviourSprite>()._wrong;

        _rompecabezas.GetComponent<Image>().sprite = state?
            _rompecabezas.GetComponent<BehaviourSprite>()._right:
            _rompecabezas.GetComponent<BehaviourSprite>()._wrong;
    }

    public void ResetGeneral()
    {
        count = 0;

        for (int i = 0; i < _rompecabezas.transform.childCount; i++)
            _rompecabezas.transform.GetChild(i).gameObject.SetActive(false);

         _layouts[_layouts.Count-1].GetComponent<Image>().sprite = null;
         _rompecabezas.GetComponent<Image>().sprite = _rompecabezas.GetComponent<BehaviourSprite>()._default;

    }
}
