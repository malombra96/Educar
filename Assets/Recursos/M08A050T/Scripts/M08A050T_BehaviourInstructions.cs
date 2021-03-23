using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08A050T_BehaviourInstructions : MonoBehaviour
{
    ControlAudio _controlAudio;
    Image _image;

    GameObject _A,_B;

    [Header("Sprite Platform")]
    public Sprite _mobile;
    public Sprite _desktop;

    [Header("UI Arrows")]
    public Button _left;
    public Button _right;

    public Button _start;
    
  
    void OnEnable()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _image = transform.GetChild(0).GetComponent<Image>();
        _image.sprite = Application.isMobilePlatform? _mobile : _desktop;

        _A = transform.GetChild(1).gameObject;
        _B = transform.GetChild(0).gameObject;

        _left.onClick.AddListener(delegate{ChangePage(false);});
        _right.onClick.AddListener(delegate{ChangePage(true);});
        
        _left.gameObject.SetActive(false);
        _right.gameObject.SetActive(true);
        _A.SetActive(true);
        _B.SetActive(false);
        _start.gameObject.SetActive(false);

    }

    void ChangePage(bool state)
    {
        _controlAudio.PlayAudio(0);

        _A.SetActive(!state);
        _B.SetActive(state);

        _left.gameObject.SetActive(state);
        _right.gameObject.SetActive(!state);

        _start.gameObject.SetActive(state);

    }

}
