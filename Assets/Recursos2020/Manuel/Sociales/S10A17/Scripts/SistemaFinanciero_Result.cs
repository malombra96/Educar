using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaFinanciero_Result : MonoBehaviour
{
    [Header("Manager World")] public SistemaFinanciero_ControlWorld _managerWorld;

    [Header("Coin Result")] 
    public Image _coinResult;
    public List<Sprite> _statesCoin;

    void Start()
    {
        if(!GetComponent<BehaviourLayout>()._isEvaluated) 
            SetPlayerEND();
            
    }

    public void SetPlayerEND()
    {
        print("ENDPLAYER");

        _managerWorld._BehaviourPlayer.SetBehaviourPlayer(false);
        _managerWorld._BehaviourPlayer.GetComponent<Animator>().enabled = false;

        _managerWorld._BehaviourPlayer.GetComponent<RectTransform>().anchoredPosition = new Vector2(-406,3712);
    }


}
