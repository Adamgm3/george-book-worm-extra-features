using UnityEngine;


public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] PlayerHealth player;
    [Header("Attack Settings")]
    public int damage = 1;
    public float attackCooldown = 1.5f;

    private float lastAttackTime;




    public void OnTriggerEnter(Collider other)
    {
        TryAttack(other);
    }
    private void TryAttack(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Check cooldown
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        // Attack
        player.health -= damage;
        Debug.Log("Player hit! Remaining Player health: " + player.health);

        // Save attack time
        lastAttackTime = Time.time;
    }
}
