using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Content.Prefixes;

public class Perfect : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override float RollChance(Item item) => 5f;

    public override bool CanRoll(Item item) => true;

    public override void ModifyValue(ref float valueMult) => valueMult *= 2f;

}

public class PerfectTooltip : GlobalItem
{
    private static List<TooltipLine> _cachedTooltipLines;

    public override void SetStaticDefaults()
    {
        _cachedTooltipLines = new List<TooltipLine>
        {
            new TooltipLine(Mod, "PerfixMaxLife",      "+10 max life")          { IsModifier = true },
            new TooltipLine(Mod, "PrefixMaxMana",      "+10 max mana")          { IsModifier = true },
            new TooltipLine(Mod, "PrefixDamage",       "+1% damage")            { IsModifier = true },
            new TooltipLine(Mod, "PrefixMeleeSpeed",   "+1% melee speed")       { IsModifier = true },
            new TooltipLine(Mod, "PrefixCritChance",   "+1% crit chance")       { IsModifier = true },
            new TooltipLine(Mod, "PrefixMoveSpeed",    "+1% movement speed")    { IsModifier = true },
            new TooltipLine(Mod, "PrefixDefense",      "+1 defense")            { IsModifier = true },
            new TooltipLine(Mod, "PrefixArmorPen",     "+2 armor penetration")  { IsModifier = true },
        };
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        if (item.prefix != ModContent.PrefixType<Perfect>()) return;
        player.statLifeMax2 += 10;
        player.statManaMax2 += 10;
        player.GetDamage(DamageClass.Generic) += 0.01f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.01f;
        player.GetCritChance(DamageClass.Generic) += 1f;
        player.moveSpeed += 0.01f;
        player.statDefense += 1;
        player.GetArmorPenetration(DamageClass.Generic) += 2;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (item.prefix != ModContent.PrefixType<Perfect>()) return;
        int researchIndex = tooltips.FindIndex(t => t.Name == "JourneyResearch");
        if (researchIndex != -1) tooltips.InsertRange(researchIndex, _cachedTooltipLines);
        else tooltips.AddRange(_cachedTooltipLines);
    }
}