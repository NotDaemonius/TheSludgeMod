using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.Players;

public class BloodThinnerPlayer : ModPlayer
{
    public bool bloodThinnerEquipped = false;

    public override void ResetEffects() => bloodThinnerEquipped = false;

    public override void UpdateEquips()
    {
        if (bloodThinnerEquipped) Player.PotionDelayModifier *= 2.0f;
    }

    public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
    {
        if (bloodThinnerEquipped && item.healLife > 0) healValue *= 3;
    }
}