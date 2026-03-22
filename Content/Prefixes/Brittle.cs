using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Prefixes;

public class Brittle : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.Melee;

    public override float RollChance(Item item) => 5f;

    public override bool CanRoll(Item item) => true;

    public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
    {
        damageMult *= 0.92f;
        critBonus += 5;
        knockbackMult *= 1.05f;
    }

    public override void ModifyValue(ref float valueMult) => valueMult *= 0.95f;
}