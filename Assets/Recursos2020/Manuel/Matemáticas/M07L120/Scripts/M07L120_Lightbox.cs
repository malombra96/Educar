using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M07L120_Lightbox : MonoBehaviour
{
    ControlAudio _controlAudio;
    
    [Header("List Toggles")] public List<Button> _button;
    [Header("List Teoria")] public List<GameObject> _lighBox;
    
    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        
        foreach (var btn in _button)
            btn.onClick.AddListener(delegate { SetActiveElement(_button.IndexOf(btn)); });

        foreach (var l in _lighBox)
            l.SetActive(false);
            
    }

    void SetActiveElement(int index)
    {
        _controlAudio.PlayAudio(0);
        _lighBox[index].SetActive(true);
    }

    public void CloseElement()
    {
        _controlAudio.PlayAudio(0);
        
        foreach (var box in _lighBox)
            if(box.activeSelf)
                box.SetActive(false);
    }
}
