using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Prefixes
{
    public class Bountiful : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item) => 5f;

        public override bool CanRoll(Item item) => true;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.21f;
        }
    }

    public class BountifulTooltip : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix == ModContent.PrefixType<Bountiful>())
            {
                player.statLifeMax2 += 10;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.prefix == ModContent.PrefixType<Bountiful>())
            {
                var line = new TooltipLine(Mod, "PrefixMaxLife", "+10 max life")
                {
                    IsModifier = true
                };

                int researchIndex = tooltips.FindIndex(t => t.Name == "JourneyResearch");

                if (researchIndex != -1)
                {
                    tooltips.Insert(researchIndex, line);
                }
                else
                {
                    tooltips.Add(line);
                }
            }
        }
    }
}