using UnityEngine;

public class FlyingBook : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;
    public float speed = 3f;

    [Header("Bobbing")]
    public float bobHeight = 0.3f;
    public float bobSpeed = 2f;

    private int currentWaypoint = 0;
    private Vector3 startPos;

    void Start()
    {
        if (waypoints.Length == 0) return;
        transform.position = waypoints[0].position;
        startPos = transform.position;
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        MoveAlongPath();
        Bob();
        FaceDirection();
    }

    void MoveAlongPath()
    {
        Transform target = waypoints[currentWaypoint];

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(target.position.x, target.position.y, target.position.z),
            speed * Time.deltaTime
        );

        // Check if we've reached the waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // Loop back to start instead of ping ponging
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    void Bob()
    {
        // Gentle up and down float
        float newY = transform.position.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void FaceDirection()
    {
        // Rotate to face the direction of travel
        Transform target = waypoints[currentWaypoint];
        Vector3 dir = (target.position - transform.position).normalized;
        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 5f * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PS1CameraEffect.collectibleCount++;
            Destroy(gameObject);
        }
    }
}