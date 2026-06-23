using Unity.VisualScripting;
using UnityEngine;

public enum AbilityType
{
    DoubleJump,
    WallJump,
    Dash
}

public class Collectible : MonoBehaviour
{
    public AbilityType abilityType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WormController worm = other.GetComponentInParent<WormController>();

            PlayerAbilities player =
                other.GetComponentInParent<PlayerAbilities>();
            if (worm != null)
            {
                player.UnlockAbility(abilityType);
                worm.UnlockAbility(abilityType);
                Destroy(gameObject);
            }
        }
    }
}