using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class M07L122_VideoPortada : MonoBehaviour
{
    VideoPlayer videoPlayer;
    [Header("Video Name ")] public string video;

    void OnEnable()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.targetTexture.Release();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath,video);

        PlayVideo();

    } 

    void PlayVideo()
    {
        videoPlayer.targetTexture.Release();
        videoPlayer.Play();
    }
}
