using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A075_BehaviourExample2 : MonoBehaviour
{
    ControlAudio _controlAudio;
    public List<Toggle> _toggle;
    public List<GameObject> _images;

    Toggle[] _selection = new Toggle[2];
    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        foreach (Toggle t in _toggle)
            t.onValueChanged.AddListener(delegate{GetSelection(t);});
            
    }
 
    void GetSelection(Toggle t)
    {
        SetSpriteState();

        if (t.isOn)
        {
            _controlAudio.PlayAudio(0);

            if (_selection[0] == null)
                _selection[0] = t;
            else if (_selection[1] == null)
                _selection[1] = t;
            else
                t.isOn = false;
        }
        else
        {
            if (_selection[0] == t)
                _selection[0] = null;
            else if (_selection[1] == t)
                _selection[1] = null;
                
        }

        if(_selection[0] != null && _selection[1] != null)
            CheckContinus();
        else
            foreach (GameObject image in _images)
                image.SetActive(false);
        
    }

    void CheckContinus()
    {
        int n = _toggle.IndexOf(_selection[0]);
        int m = _toggle.IndexOf(_selection[1]);

        string a = _selection[0].name;
        string b = _selection[1].name;

        bool c = ((n+1) == m)  || ((n-1) == m) || ((m+1) == n) || ((m-1) == n);

        if(c)
        {
            foreach (GameObject image in _images)
            {
                string[] s = image.name.Split('/');
                image.SetActive((s[0] == a || s[1] == a) && (s[0] == b || s[1] == b));
            }
        }


    } 

    

    void SetSpriteState()
    {
        foreach (var t in _toggle)
        {
            t.GetComponent<Image>().sprite = t.isOn
                ? t.GetComponent<BehaviourSprite>()._selection
                : t.GetComponent<BehaviourSprite>()._default;
        }
    }
}
