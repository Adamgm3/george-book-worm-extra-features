using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    private Vector3 respawnPosition;
    private bool checkpointSet = false;

    void Awake()
    {
        Instance = this;
    }

    public void SetCheckpoint(Vector3 position)
    {
        respawnPosition = position;
        checkpointSet = true;
        Debug.Log("Checkpoint set at: " + position);
    }

    public bool HasCheckpoint()
    {
        return checkpointSet;
    }

    public void Respawn(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
        player.transform.position = respawnPosition;
    }
}