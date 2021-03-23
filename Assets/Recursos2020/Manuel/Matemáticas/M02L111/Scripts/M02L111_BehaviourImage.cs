using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M02L111_BehaviourImage : MonoBehaviour
{
    ControlAudio _controlAudio;
    Image  _image;

    public List<Sprite> _sprites;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    int n;

    [Header("Aditional Objects")] public List<GameObject> _objects;
    [Header("Aditional Objects")] public List<string> _indexRange;

    void OnEnable()
    {
        _image = GetComponent<Image>();
        _image.sprite = _sprites[0];

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
        if (n + dir <= (_sprites.Count-1) && n + dir >= 0)
        {
            n += dir;
            _image.sprite = _sprites[n];

            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        /* _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=(_sprites.Count-1)); */

        _left.interactable =  (n!=0);
        _right.interactable = (n!=(_sprites.Count-1));

        for (int i = 0; i < _objects.Count; i++)
        {
            int min = int.Parse(_indexRange[i].Split('-')[0]);
            int max = int.Parse(_indexRange[i].Split('-')[1]);

            _objects[i].SetActive( (n>=min)&&(n<=max) );
        }
    }
}
