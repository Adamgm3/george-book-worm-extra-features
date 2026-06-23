using System.Collections.Generic;
using UnityEngine;

public class AbillityUI : MonoBehaviour
{
    public PlayerAbilities player;
    public AbilitySlotUI slotPrefab;
    public Transform contentParent;

    private Dictionary<AbilityType, AbilitySlotUI> slots = new();

    private void Start()
    {
        BuildUI();

        // listen for ability unlocks
        player.OnAbilityUnlocked += HandleAbilityUnlocked;
    }

    private void BuildUI()
    {
        AbilityType[] abilities =
            (AbilityType[])System.Enum.GetValues(typeof(AbilityType));

        foreach (AbilityType ability in abilities)
        {
            AbilitySlotUI slot =
                Instantiate(slotPrefab, contentParent);

            slot.Init(player, ability);

            slots.Add(ability, slot);
        }
    }

    private void HandleAbilityUnlocked(AbilityType ability)
    {
        if (slots.TryGetValue(ability, out AbilitySlotUI slot))
        {
            slot.Refresh();
        }
    }
}