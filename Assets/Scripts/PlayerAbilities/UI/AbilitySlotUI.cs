using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySlotUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image iconImage;
    public Image lockedOverlay;

    private PlayerAbilities player;
    private AbilityType ability;

    public void Init(PlayerAbilities player, AbilityType ability)
    {
        this.player = player;
        this.ability = ability;

        nameText.text = ability.ToString();

        Refresh();
    }

    public void Refresh()
    {
        bool unlocked = player.HasAbility(ability);

        lockedOverlay.enabled = !unlocked;
        iconImage.color = unlocked ? Color.white : Color.grey;
    }
}