using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    private InputAction m_attackAction;
    public InputActionAsset inputActions;
    public int damage = 1;
    private bool attackPressed = false;

    private void Awake()
    {
        m_attackAction = inputActions.FindAction("Attack");
    }

    private void Update()
    {
        if (m_attackAction.IsPressed())
            attackPressed = true;
        else
            attackPressed = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && attackPressed)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.health -= damage;
                Debug.Log("Enemy hit! Remaining health: " + enemy.health);
            }
        }
    }
}