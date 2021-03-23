using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A129_Resultado : MonoBehaviour
{
    [Header("Manager World")] public M09A129_ControlWorld _managerWorld;
    [Header("Tazon Fruis")] public RectTransform _tazonFruits;
    public Image _msn;


    void OnEnable()
    {
        int n = 0;

        foreach (GameObject fruitPanel in _managerWorld._downPanel)
        {
            for (int i = 0; i < _tazonFruits.childCount; i++)
            {
                if(fruitPanel.name == _tazonFruits.GetChild(i).name)
                    _tazonFruits.GetChild(i).gameObject.SetActive(fruitPanel.activeSelf);
            }

            if(fruitPanel.activeSelf)
                n++;
        }

        _msn.sprite = (n == _managerWorld._downPanel.Count)? _msn.GetComponent<BehaviourSprite>()._right : _msn.GetComponent<BehaviourSprite>()._wrong;
        
        SetPlayer();
    }

    void SetPlayer()
    {
        _managerWorld._BehaviourPlayer.SetBehaviourPlayer(false);
        _managerWorld._BehaviourPlayer.GetComponent<Animator>().enabled = false;

        _managerWorld._BehaviourPlayer.GetComponent<RectTransform>().anchoredPosition = new Vector2(65,7400);
    }
}
