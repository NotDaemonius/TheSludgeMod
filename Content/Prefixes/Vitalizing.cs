using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Prefixes
{
    public class Vitalizing : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item) => 5f;

        public override bool CanRoll(Item item) => true;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.44f;
        }
    }

    public class VitalizingTooltip : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix == ModContent.PrefixType<Vitalizing>())
            {
                player.statLifeMax2 += 20;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.prefix == ModContent.PrefixType<Vitalizing>())
            {
                var line = new TooltipLine(Mod, "PrefixMaxLife", "+20 max life")
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