using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A060_ManagerPopUp : MonoBehaviour
{
    ControlAudio _controlAudio;
    
    [Header("List Buttons")] public List<Button> _button;
    [Header("List Teoria")] public List<GameObject> _teory;

    [Header("Button Video Graphic")] public Button _btnVideo;
    [Header("Video Object")] public GameObject _video;

    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        foreach (var btn in _button)
            btn.onClick.AddListener(delegate { SetElementActive(btn); });

        foreach (var x in _teory)
            x.SetActive(false);

        _btnVideo.onClick.AddListener(OpenVideo);
    }

    void SetElementActive(Button select)
    {
        int index = _button.IndexOf(select);

        _controlAudio.PlayAudio(0);
            
        for (int i = 0; i < _button.Count; i++)
        {
            SetSpriteState(_button[i].GetComponent<Image>(),(i <= index));
        }

        for (int i = 0; i < _teory.Count; i++)
        {
            _teory[i].SetActive(i==index);
        }
    }
    
    
    /// <summary>
    /// Change sprite select toggle
    /// </summary>
    void SetSpriteState(Image i, bool state)
    {
        i.sprite = state ? i.GetComponent<BehaviourSprite>()._selection : i.GetComponent<BehaviourSprite>()._default;
    }

    public void ClosePopUp()
    {
        _controlAudio.PlayAudio(0);
        
        foreach (var x in _teory)
            x.SetActive(false);
    }

    void OpenVideo()
    {
        _controlAudio.PlayAudio(0);
        _video.SetActive(true);
    }

    public void CloseVideo()
    {
        _controlAudio.PlayAudio(0);
        _video.SetActive(false);
    }
}
