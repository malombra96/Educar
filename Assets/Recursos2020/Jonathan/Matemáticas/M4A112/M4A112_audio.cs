using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M4A112_audio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip clip;
    public float lenghtClip;
    public Image play, pause;
    public AudioSource audioSource;
    public bool op;
    
    void Start()
    {
        lenghtClip = clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (op) {
            //print(audioSource.time);
            if (audioSource.time == lenghtClip)
            {
                play.sprite = play.GetComponent<BehaviourSprite>()._default;
                pause.sprite = pause.GetComponent<BehaviourSprite>()._default;
                op = false;
            }
        }
    }

    public void PlayAudio() {
        op = true;
        audioSource.clip = clip;
        audioSource.Play();
        play.sprite = play.GetComponent<BehaviourSprite>()._selection;
    }

    public void StopAudio() {
        op = false;
        pause.sprite = pause.GetComponent<BehaviourSprite>()._selection;
        play.sprite = play.GetComponent<BehaviourSprite>()._default;
        StartCoroutine(x());
    }

    IEnumerator x() {
        yield return new WaitForSeconds(0.3f);
        pause.sprite = pause.GetComponent<BehaviourSprite>()._default;
    }
    private void OnDisable()
    {
        audioSource.Stop();
        play.sprite = play.GetComponent<BehaviourSprite>()._default;
        pause.sprite = pause.GetComponent<BehaviourSprite>()._default;
        op = false;
    }
}
