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
    public PlayerAbilities player;
    WormController worm;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerAbilities>();
        worm = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<WormController>();
        Debug.Log(player);
        if (player.unlockedAbilities.Contains(abilityType))
        {
            worm.UnlockAbility(abilityType);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            worm = other.GetComponentInParent<WormController>();

            player = other.GetComponentInParent<PlayerAbilities>();
            if (worm != null)
            {
                player.UnlockAbility(abilityType);
                worm.UnlockAbility(abilityType);
                SaveSystem.Instance.SaveGame();
                Destroy(gameObject);
            }
        }
    }
}