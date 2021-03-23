using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L029_PestanasSpecial : MonoBehaviour
{
    ControlAudio _controlAudio;

    [Header("Menu Teory")] 
    public GameObject _menu;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    public int n;

    void OnEnable()
    {
        n = 0;

        for (int i = 0; i < _menu.transform.childCount; i++)
            _menu.transform.GetChild(i).gameObject.SetActive(i<=n);   

        SetStateArrows();
    }

    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _left.onClick.AddListener(delegate{ChangeInstruction(-1);});
        _right.onClick.AddListener(delegate{ChangeInstruction(1);});
    }

    void ChangeInstruction(int dir)
    {
        if (n + dir <= 3 && n + dir >= 0)
        {
            n += dir;
            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }

        for (int i = 0; i < _menu.transform.childCount; i++)
        {
            _menu.transform.GetChild(i).gameObject.SetActive(i<=n); 

           if(n>=2)
             _menu.transform.GetChild(1).gameObject.SetActive(false); 
            
        }
            
        
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=3);

    }
}
