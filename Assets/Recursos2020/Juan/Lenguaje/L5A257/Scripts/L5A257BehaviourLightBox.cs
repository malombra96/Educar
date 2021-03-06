using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A257BehaviourLightBox : MonoBehaviour
{
    ControlAudio _controlAudio;
    private GameObject _navBar;

    [Header("List Toggles")] public List<Button> _button;
    [Header("List Teoria")] public List<GameObject> _lighBox;

    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        if (FindObjectOfType<BehaviourNavBar>())
            _navBar = FindObjectOfType<BehaviourNavBar>().gameObject;
        else
            _navBar = FindObjectOfType<BehaviourNavBarLeccion>().gameObject;


        foreach (var btn in _button)
            btn.onClick.AddListener(delegate { SetActiveElement(_button.IndexOf(btn)); });
    }

    void SetActiveElement(int index)
    {
        _controlAudio.PlayAudio(0);
        _navBar.gameObject.SetActive(false);
        _lighBox[index].SetActive(true);
    }

    public void CloseElement()
    {       

        if(_navBar)
            _navBar.gameObject.SetActive(true);

        foreach (var box in _lighBox)
            if (box.activeSelf)
                box.SetActive(false);
    }

}
