
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
public class CutSceneManager : MonoBehaviour
{
     [Header("Video Settings")]
    [SerializeField] private VideoPlayer videoPlayer;
     [SerializeField] private RawImage cutsceneImage;
       private bool isTransitioning = false;
    private bool videoFinished = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    

    // Update is called once per frame
  

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

        
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoFinished = true;
    }
    
}