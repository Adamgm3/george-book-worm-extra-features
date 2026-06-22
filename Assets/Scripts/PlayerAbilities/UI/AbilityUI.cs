using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    public PlayerAbilities player;
    public AbilitySlotUI slotPrefab;
    public Transform contentParent;

    private AbilitySlotUI[] slots;

    private void Start()
    {
        CreateUI();
    }

    void CreateUI()
    {
        AbilityType[] abilities = (AbilityType[])System.Enum.GetValues(typeof(AbilityType));

        slots = new AbilitySlotUI[abilities.Length];

        for (int i = 0; i < abilities.Length; i++)
        {
            AbilitySlotUI slot = Instantiate(slotPrefab, contentParent);

            slot.abilityType = abilities[i];
            slot.Init(player);

            slots[i] = slot;
        }
    }

    private void Update()
    {
        foreach (var slot in slots)
        {
            slot.Refresh();
        }
    }
}
