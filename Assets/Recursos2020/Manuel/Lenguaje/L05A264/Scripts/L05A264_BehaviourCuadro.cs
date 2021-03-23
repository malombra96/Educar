using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class L05A264_BehaviourCuadro : MonoBehaviour, IPointerClickHandler
{
    public L05A264_ManagerAplico _ManagerAplico;
    public List<BehaviourLayout> _layouts;
    L05A264_Player _player;

    void Start()
    {
        _player = FindObjectOfType<L05A264_Player>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(_player.currentHit == gameObject)
        {
            //print(name+"click");
            if(_layouts.Contains(_ManagerAplico._current))
            {
                //print(name+"IN");
                _ManagerAplico._current.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

}
