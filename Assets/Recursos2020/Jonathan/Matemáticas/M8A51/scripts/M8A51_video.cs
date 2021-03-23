using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class M8A51_video : MonoBehaviour
{

    VideoPlayer videoPlayer;
    public string file;
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, file);
        videoPlayer.Play();
    }
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, file);
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
