using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the Video Player component
    public VideoClip introVideo1;   // First video clip
    public VideoClip introVideo2;   // Second video clip
    public VideoClip introVideo3;   // Third video clip

    private int currentVideoIndex = 0; // Track the current video index

    void Start()
    {
        // Assign the first video and play it
        videoPlayer.clip = introVideo1;
        videoPlayer.Play();

        // Subscribe to the video end event
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        // Pressing Space moves to the next video or scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayNextVideoOrScene();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Automatically play the next video or load the scene
        PlayNextVideoOrScene();
    }

    void PlayNextVideoOrScene()
    {
        currentVideoIndex++;

        if (currentVideoIndex == 1)
        {
            // Switch to the second video
            videoPlayer.Stop();
            videoPlayer.clip = introVideo2;
            videoPlayer.Play();
        }
        else if (currentVideoIndex == 2)
        {
            // Switch to the third video
            videoPlayer.Stop();
            videoPlayer.clip = introVideo3;
            videoPlayer.Play();
        }
        else
        {
            // Load the scene after the third video
            LoadSampleScene();
        }
    }

    void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnDestroy()
    {
        // Unsubscribe from event to avoid memory leaks
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
