using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class LevelTeleport : MonoBehaviour
{
    [Header("Video Settings")]
    [SerializeField] private VideoPlayer videoPlayer;
     [SerializeField] private RawImage cutsceneImage;
    private bool isTransitioning = false;
    private bool videoFinished = false;

    private void Start()
    {
        cutsceneImage.gameObject.SetActive(false);
        if (videoPlayer != null)
        {
            videoPlayer.playOnAwake = false;

            // Subscribe to video finished event
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(PlayCutsceneThenLoad());
        }
    }

    private System.Collections.IEnumerator PlayCutsceneThenLoad()
    {
        isTransitioning = true;
        cutsceneImage.gameObject.SetActive(true);

        if (videoPlayer != null)
        {
            videoFinished = false;

            videoPlayer.Play();
            

            // Wait until the video actually finishes
            yield return new WaitUntil(() => videoFinished);
        }

        SceneManager.LoadScene(2);
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoFinished = true;
    }
}