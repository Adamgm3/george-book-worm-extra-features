using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public float chaseRange = 10f; // Distance at which chase starts
    public int health = 3;
    public float stopDistance = 2f;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stopDistance;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            if (distance > stopDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                agent.isStopped = true;

                Vector3 direction = player.position - transform.position;
                direction.y = 0;

                if (direction.sqrMagnitude > 0.01f)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        lookRotation,
                        Time.deltaTime * 5f
                    );
                }
            }
        }
        else
        {
            agent.isStopped = true;
        }



        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


}