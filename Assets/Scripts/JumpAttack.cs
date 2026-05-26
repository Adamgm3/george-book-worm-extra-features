using UnityEngine;
using UnityEngine.InputSystem;

public class JumpAttack : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy;

    public int damage = 1;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

                enemy.health -= damage;
                Debug.Log("Enemy hit! By Jump Remaining Enemy health: " + enemy.health);
            

        }

    }
}
