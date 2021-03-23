using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class M09A082_VideoPath : MonoBehaviour
{
    VideoPlayer videoPlayer;
    [Header("Video Name ")] public string video;

    [Header("Play")] public Button _play;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.targetTexture.Release();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath,video);

        _play.onClick.AddListener(PlayVideo);
    } 

    void PlayVideo()
    {
        videoPlayer.targetTexture.Release();
        videoPlayer.Play();
    }

    void Update() => _play.transform.parent.gameObject.SetActive(!videoPlayer.isPlaying);
}
