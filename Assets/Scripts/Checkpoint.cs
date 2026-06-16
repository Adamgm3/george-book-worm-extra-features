using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint triggered by: " + other.gameObject.name + " tag: " + other.tag);

        if (!other.CompareTag("Player")) return;
        if (activated) return;

        activated = true;
        CheckpointManager.Instance.SetCheckpoint(transform.position);
        GetComponent<Renderer>().material.color = Color.green;
    }
}