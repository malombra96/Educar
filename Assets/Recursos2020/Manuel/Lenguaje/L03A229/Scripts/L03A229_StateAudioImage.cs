using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L03A229_StateAudioImage : MonoBehaviour
{
    ControlAudio _controlAudio;
    [Header("Image Text")] public Image  _image;

    L03A229_BehaviourAudio _behaviourAudio;

    public List<Sprite> _sprites;
    public List<AudioClip> _clips;

    [Header("General Controllers")] 
    public Button _left;
    public Button _right;

    [Header("Note")] public GameObject _note;


    int n;

    void OnEnable()
    {
        _image.sprite = _sprites[0];

        _behaviourAudio = GetComponent<L03A229_BehaviourAudio>();
        _behaviourAudio.clip = _clips[0];
        _behaviourAudio.duracionAudio = _clips[0].length;

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

            _behaviourAudio.PauseAudio(false);
            _behaviourAudio.clip = _clips[n];
            _behaviourAudio.duracionAudio = _clips[n].length;

            _controlAudio.PlayAudio(0);
            SetStateArrows();
        }
    }

    void SetStateArrows()
    {
        _left.gameObject.SetActive(n!=0);
        _right.gameObject.SetActive(n!=(_sprites.Count-1));
        _note.SetActive(n==(_sprites.Count-1));
    }
}
