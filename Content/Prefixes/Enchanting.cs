using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Prefixes;

public class Enchanting : ModPrefix
{
    public override PrefixCategory Category => PrefixCategory.Accessory;

    public override float RollChance(Item item) => 5f;

    public override bool CanRoll(Item item) => true;

    public override void ModifyValue(ref float valueMult) => valueMult *= 1.44f;
}

public class EnchantingTooltip : GlobalItem
{
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        if (item.prefix != ModContent.PrefixType<Enchanting>()) return;
        player.statManaMax2 += 40;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (item.prefix == ModContent.PrefixType<Enchanting>())
        {
            var line = new TooltipLine(Mod, "PrefixMaxMana", "+40 max mana") {IsModifier = true};
            int researchIndex = tooltips.FindIndex(t => t.Name == "JourneyResearch");
            if (researchIndex != -1) tooltips.Insert(researchIndex, line);
            else tooltips.Add(line);
        }
    }
}