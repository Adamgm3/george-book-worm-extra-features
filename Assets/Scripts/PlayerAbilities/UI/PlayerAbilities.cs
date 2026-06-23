using System;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public HashSet<AbilityType> unlockedAbilities = new();

    public Action<AbilityType> OnAbilityUnlocked;

    public void UnlockAbility(AbilityType ability)
    {
        if (unlockedAbilities.Contains(ability))
            return;

        unlockedAbilities.Add(ability);

        OnAbilityUnlocked?.Invoke(ability);
    }

    public bool HasAbility(AbilityType ability)
    {
        return unlockedAbilities.Contains(ability);
    }
    public void SaveTo(Save data)
    {
        data.unlockedAbilities.Clear();
        foreach (var ability in unlockedAbilities)
        {
            data.unlockedAbilities.Add(ability.ToString());
        }
    }
    public void LoadFrom(Save data)
    {
        unlockedAbilities.Clear();

        foreach (string abilityName in data.unlockedAbilities)
        {
            if (System.Enum.TryParse(abilityName, out AbilityType ability))
            {
                unlockedAbilities.Add(ability);

                // IMPORTANT: re-trigger UI update event
                OnAbilityUnlocked?.Invoke(ability);
            }
        }
    }
}