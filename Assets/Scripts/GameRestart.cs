using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public bool useCheckpoint = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (useCheckpoint && CheckpointManager.Instance != null && CheckpointManager.Instance.HasCheckpoint())
        {
            // Respawn at checkpoint instead of restarting
            CheckpointManager.Instance.Respawn(other.transform.root.gameObject);
        }
        else
        {
            // No checkpoint set, restart scene as before
            SceneManager.LoadScene(sceneName);
        }
    }
}