using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoIntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextScene = "StoryScene";

    private bool canSkip = false;
    private bool videoEnded = false;

    void Start()
    {
        // Evita que comience sin estar listo
        videoPlayer.playOnAwake = false;
        videoPlayer.loopPointReached += EndReached;

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;

        StartCoroutine(AllowSkipAfter(1f));
    }

    void OnPrepared(VideoPlayer vp)
    {
        videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        if (!videoEnded)
        {
            videoEnded = true;
            SceneManager.LoadScene(nextScene);
        }
    }

    IEnumerator AllowSkipAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        canSkip = true;
    }

    void Update()
    {
        if (canSkip && Input.anyKeyDown && !videoEnded)
        {
            videoEnded = true;
            videoPlayer.Stop();
            SceneManager.LoadScene(nextScene);
        }
    }
}

