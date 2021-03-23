using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L11A269inicio : MonoBehaviour
{
    bool play = true;
    ControlAudio controlAudio;
    public AudioClip audio1;
    public AudioClip audio2;

    private void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
    }
    private void OnEnable()
    {
        if (play)
        {
            GetComponent<AudioSource>().clip = audio1;
            GetComponent<AudioSource>().Play();
            transform.GetChild(0).GetComponent<Animator>().Play("hablando");
            StartCoroutine(reprduccion(GetComponent<AudioSource>().clip.length));
            play = false;
        }
    }
    IEnumerator reprduccion(float t)
    {
        yield return new WaitForSeconds(t);
        GetComponent<AudioSource>().clip = audio2;
        GetComponent<AudioSource>().Play();
        StartCoroutine(detener(GetComponent<AudioSource>().clip.length));
        transform.GetChild(2).GetComponent<Animator>().Play("hablando");
        transform.GetChild(0).GetComponent<Animator>().Play("quieto");
    }
    IEnumerator detener(float t)
    {
        yield return new WaitForSeconds(t);
        transform.GetChild(2).GetComponent<Animator>().Play("quieto");
    }
    public void desactivar()
    {
        if(controlAudio)
            controlAudio.PlayAudio(0);
        gameObject.SetActive(false);
        GetComponent<AudioSource>().Stop();
        play = false;
    }
    public void resetAll()
    {
        gameObject.SetActive(true);
        play = true;
    }
}
