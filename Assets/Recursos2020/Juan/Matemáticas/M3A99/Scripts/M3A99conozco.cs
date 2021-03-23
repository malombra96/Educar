using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M3A99conozco : MonoBehaviour
{
    public Button next, back;
    public List<Sprite> sprites;
    public List<AudioClip> audios;
    public Image image;
    ControlAudio controlAudio;
    int x;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        next.onClick.AddListener(siguiente);
        back.onClick.AddListener(devolver);
        image.sprite = sprites[0];
        back.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void siguiente()
    {
        controlAudio.PlayAudio(0);
        transform.GetChild(0).GetComponent<M3A99BehaviourAudioClip>().PausaLayaout(false);        
        if (x < sprites.Count-1)
        {
            x++;
            image.sprite = sprites[x];
            transform.GetChild(0).GetComponent<M3A99BehaviourAudioClip>().clip = audios[x];
            transform.GetChild(0).GetComponent<AudioSource>().clip = audios[x];
            transform.GetChild(0).GetComponent<M3A99BehaviourAudioClip>().duracionAudio = transform.GetChild(0).GetComponent<AudioSource>().clip.length;
            if (x == 1)
                back.gameObject.SetActive(true);
            else if (x == 4)
                next.gameObject.SetActive(false);
        }  

    }
    void devolver()
    {
        controlAudio.PlayAudio(0);
        transform.GetChild(0).GetComponent<M3A99BehaviourAudioClip>().PausaLayaout(false);        
        if (x > 0)
        {
            x--;
            image.sprite = sprites[x];
            transform.GetChild(0).GetComponent<M3A99BehaviourAudioClip>().clip = audios[x];
            transform.GetChild(0).GetComponent<AudioSource>().clip = audios[x];
            transform.GetChild(0).GetComponent<M3A99BehaviourAudioClip>().duracionAudio = transform.GetChild(0).GetComponent<AudioSource>().clip.length;

            if (x == 3)
                next.gameObject.SetActive(true);
            else if (x == 0)
                back.gameObject.SetActive(false);
        }        
    }
    public void close()
    {
        x = 0;
        image.sprite = sprites[x];

        transform.GetChild(0).GetComponent<M3A99BehaviourAudioClip>().clip = audios[x];
        transform.GetChild(0).GetComponent<AudioSource>().clip = audios[x];
        transform.GetChild(0).GetComponent<M3A99BehaviourAudioClip>().duracionAudio = transform.GetChild(0).GetComponent<AudioSource>().clip.length;

        next.gameObject.SetActive(true);
        back.gameObject.SetActive(false);
    }
}
