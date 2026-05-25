using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy;
    private InputAction m_attackAction;
    public InputActionAsset inputActions;
    public int damage = 1;
    private bool attackPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        attackPressed = m_attackAction.IsPressed();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && attackPressed)
        {
            EnemyMovement enemy = other.GetComponent<EnemyMovement>();

            if (enemy != null)
            {
                enemy.health -= damage;

                Debug.Log("Enemy hit! Remaining health: " + enemy.health);
            }
        }
    }

}
