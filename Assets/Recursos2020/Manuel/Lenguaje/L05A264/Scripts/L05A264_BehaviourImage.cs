using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L05A264_BehaviourImage : MonoBehaviour
{
    ControlAudio _controlAudio;
    public List<GameObject> _images;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    int n;

    void OnEnable()
    {
        for (int i = 0; i < _images.Count; i++)
            _images[i].SetActive(i==0);
        
        n = 0;
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
        if (n + dir <= (_images.Count-1) && n + dir >= 0)
        {
            n += dir;
            
            for (int i = 0; i < _images.Count; i++)
                _images[i].SetActive(i==n);

            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=(_images.Count-1));
    }
}
