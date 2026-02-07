using UnityEngine;
using UnityEngine.Video;

public class InventoryVideoController : MonoBehaviour
{
    
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.url = "https://your-link.com/video.mp4";
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += vp => vp.Play();
    }

    public void PlayVideo() 
    {
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }
}
