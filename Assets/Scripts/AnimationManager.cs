using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] WormController wormController;
    [SerializeField] public Animator animator;
    public InputSystem_Actions inputActions;

    //void OnEnable()
    //{
    //    inputActions.Player.Enable();
    //    inputActions.Player.Attack.performed += ctx => HandleAttack();
    //}

    private void Update()
    {
        HandleAnimations();
    }

    //public void HandleAttack()
    //{
    //    Debug.Log("Attacking");
    //    animator.SetTrigger("Attack");
    //}

    public void HandleAnimations()
    {
        if (wormController.moveInput.magnitude != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (wormController.inputActions.Player.Jump.WasReleasedThisFrame())
        {
            animator.SetTrigger("beginJump");
        }
        else
        {
            animator.ResetTrigger("beginJump");
        }

        if (wormController.isGrounded == false)
        {
            animator.SetBool("notGrounded", true);
        }
        else
        {
            animator.SetBool("notGrounded", false);
            //animator.SetBool("isJumping", false);
        }

        if (wormController.inputActions.Player.Attack.WasPressedThisFrame())
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.ResetTrigger("Attack");
        }
    }
}
