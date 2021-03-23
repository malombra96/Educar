using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09L080_BehaviourPremio : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    M09L080_ManagerHelps _ManagerHelps;

    Image _image;

    [Header("Sprite Prize")] 
    public Sprite _noPrize;
    public Sprite _defaultPrize;
    public Sprite _highestPrize;

    [Header("Current Prize")] public string _currentPrize;

    [Header("Text Prize")] public Text _textPrize;

    // Start is called before the first frame update
    void Start()
    {
        _ManagerHelps = FindObjectOfType<M09L080_ManagerHelps>();
        _image = transform.GetChild(0).GetComponent<Image>();

        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        GetPrizeTotal();
    }

    // Update is called once per frame
    void GetPrizeTotal()
    {
        _currentPrize = _ManagerHelps._prizeValue;
        _textPrize.text = _currentPrize;

        if(_currentPrize == "0")
            _image.sprite = _noPrize;
        else if(_currentPrize == "500'000.000")
            _image.sprite = _highestPrize;
        else
            _image.sprite = _defaultPrize;

        
        _textPrize.gameObject.SetActive(_currentPrize != "0");

        
        _controlNavegacion.Forward(5);
    }


}
