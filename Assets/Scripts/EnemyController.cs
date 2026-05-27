using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool isCorrupted;
    [SerializeField] GameObject Player;
    Vector3 playerPosition;
    Vector3 enemyPosition;
    Vector3 direction;

    [SerializeField] float speed = 2f;
    public float chaseRange = 10f;
    public float stopDistance = 2f;
    public int health = 3;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        playerPosition = Player.transform.position;
        enemyPosition = transform.position;

        float distance = Vector3.Distance(playerPosition, enemyPosition);

        if (distance > chaseRange || distance < stopDistance)
            return;

        direction = playerPosition - enemyPosition;
        direction.y = 0;
        direction = direction.normalized;

        transform.rotation = Quaternion.LookRotation(direction);
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}