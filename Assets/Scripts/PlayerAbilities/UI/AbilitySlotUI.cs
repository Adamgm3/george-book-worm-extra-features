using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotUI : MonoBehaviour
{
    public AbilityType abilityType;

    public TextMeshProUGUI nameText;
    public Image iconImage;
    public Image lockedOverlay;

    private PlayerAbilities playerAbilities;

    public void Init(PlayerAbilities player)
    {
        playerAbilities = player;

        nameText.text = abilityType.ToString();

        Refresh();
    }

    public void Refresh()
    {
        bool unlocked = playerAbilities.Has(abilityType);

        lockedOverlay.enabled = !unlocked;
        iconImage.color = unlocked ? Color.white : Color.grey;
    }
}
