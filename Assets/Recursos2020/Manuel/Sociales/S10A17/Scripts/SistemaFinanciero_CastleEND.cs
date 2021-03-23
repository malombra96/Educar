using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaFinanciero_CastleEND : MonoBehaviour
{
    [Header("Manager World")] public SistemaFinanciero_Result _controlResult;
    ControlNavegacion _controlNavegacion;

    void Awake() => _controlNavegacion = FindObjectOfType<ControlNavegacion>();

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            _controlResult._managerWorld._BehaviourPlayer.SetBehaviourPlayer(false);
            _controlResult._managerWorld._BehaviourPlayer.GetComponent<Animator>().enabled = false;
           
            int temp = (_controlResult._managerWorld._countRight);

           _controlResult._coinResult.sprite = _controlResult._statesCoin[temp];


           if(!_controlResult.GetComponent<BehaviourLayout>()._isEvaluated)
                _controlNavegacion.GoToLayout(14,2);

        }
    }
}
