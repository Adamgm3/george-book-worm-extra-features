using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] WormController wormController;
    [SerializeField] public Animator animator;

    private void Update()
    {
        HandleAnimations();
    }

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
    }
}
