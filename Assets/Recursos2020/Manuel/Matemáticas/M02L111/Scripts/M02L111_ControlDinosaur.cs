using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M02L111_ControlDinosaur : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    public List<BehaviourLayout> _layouts;
    BehaviourLayout _current;
    [Header("Cartel Resultado")] public GameObject _resultado;

    [Header("Animation Dinosaur")] public GameObject _animation;    

    GameObject _dinosaur;


    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _dinosaur = transform.GetChild(0).gameObject;

        _resultado.SetActive(false);
        _animation.SetActive(false);
        
        
    }

    void LateUpdate()
    {
        _current = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>();

        if(_current)
            _dinosaur.SetActive(_layouts.Contains(_current));
            
    }

    public void SetResultultado()
    {
        bool state = _dinosaur.GetComponent<M02L111_BehaviourDinosaur>()._correctSize;

        if(state)
        {
            _dinosaur.SetActive(false);
            _animation.SetActive(true);
        }

        _resultado.SetActive(true);

        _resultado.GetComponent<Image>().sprite = state?
            _resultado.GetComponent<BehaviourSprite>()._right:
            _resultado.GetComponent<BehaviourSprite>()._wrong; 
    }

    public void ResetGeneral()
    {
        _animation.SetActive(false);
        _resultado.GetComponent<Image>().sprite = null;
        _resultado.SetActive(false);
        _dinosaur.GetComponent<M02L111_BehaviourDinosaur>().ResetDinosaur();
        

    }
}
