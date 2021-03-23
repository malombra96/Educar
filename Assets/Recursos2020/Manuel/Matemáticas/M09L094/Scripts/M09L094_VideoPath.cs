using UnityEngine;
using UnityEngine.Video;

public class M09L094_VideoPath : MonoBehaviour
{
    VideoPlayer videoPlayer;
    [Header("Video Name ")] public string video;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.targetTexture.Release();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath,video);
    } 
}
