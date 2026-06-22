using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public HashSet<AbilityType> unlocked = new();

    public void Unlock(AbilityType ability)
    {
        unlocked.Add(ability);
    }

    public bool Has(AbilityType ability)
    {
        return unlocked.Contains(ability);
    }
}