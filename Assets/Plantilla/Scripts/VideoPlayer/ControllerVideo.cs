using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControllerVideo : MonoBehaviour
{
    [Header("Path Video")] [Tooltip("Ingrese le nombre y extension del video")]  public string _pathVideo;

    VideoPlayer _videoPlayer;
    IDisplayController display;

    [Header("Buttons Controller")] 
    public Toggle _play;
    public Button _restart;
    public Toggle _fullScreen;
    public Button _forward;
    public Button _backward;

    [Header("Text UI")] 
    public Text _time;
    
    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        
        display = DisplayController.ForDisplay(0);
        _fullScreen.onValueChanged.AddListener(delegate { FullScreen(); });
        
        _forward.onClick.AddListener(ForwardVideo);
        _backward.onClick.AddListener(BackwardVideo);
        
        _play.onValueChanged.AddListener(delegate { PlayPause(); });
        _restart.onClick.AddListener(Restart);

        PlayWebGL();
    }

    public void PlayWebGL()
    {
        _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath,_pathVideo);
        _videoPlayer.Prepare();
    }


    void PlayPause()
    {
        if (_play.isOn)
        {
            _videoPlayer.Play();
            _play.GetComponent<Image>().sprite = _play.GetComponent<BehaviourSprite>()._selection;
        }
        else
        {
            _videoPlayer.Pause();
            _play.GetComponent<Image>().sprite = _play.GetComponent<BehaviourSprite>()._default;
        }
            
    }

    void FullScreen()
    {
        if (_fullScreen.isOn)
        {
            display.ToFullscreen(gameObject.transform as RectTransform);
            _fullScreen.GetComponent<Image>().sprite = _fullScreen.GetComponent<BehaviourSprite>()._selection;
        }
        else
        {
            display.ToNormal();
            _fullScreen.GetComponent<Image>().sprite = _fullScreen.GetComponent<BehaviourSprite>()._default;
        }
        
    }

    void ForwardVideo()
    {
        //print("Forward");
        
        if (!_videoPlayer.isPlaying && !_videoPlayer.isPaused)
        {
            _videoPlayer.Play();
            _videoPlayer.Pause();
        }
       
        
        _videoPlayer.time += 5;
        _forward.GetComponent<Animator>().SetBool("state",true);
        StartCoroutine(DelayAnimation());
    }

    void BackwardVideo()
    {
        //print("Backward");
        
        if (!_videoPlayer.isPlaying && !_videoPlayer.isPaused)
        {
            _videoPlayer.Play();
            _videoPlayer.Pause();
        }
        
        _videoPlayer.time -= 5;
        _backward.GetComponent<Animator>().SetBool("state",true);
        StartCoroutine(DelayAnimation());
    }
    
    IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(.1f);
        _forward.GetComponent<Animator>().SetBool("state",false);
        _backward.GetComponent<Animator>().SetBool("state",false);
    }

    void Restart()
    {
        _play.isOn = false;
        _videoPlayer.Stop();
        _videoPlayer.waitForFirstFrame = true;
        _videoPlayer.Prepare();
    }

    private void Update()
    {
        if (Math.Abs(_videoPlayer.time - _videoPlayer.length) < .2f)
            Restart();

        TimeSpan time = TimeSpan.FromSeconds(_videoPlayer.time);
        _time.text = $"{time.Minutes:00}:{time.Seconds:00}";
    }

    private void OnDisable()
    {
        Restart();
    }
}
