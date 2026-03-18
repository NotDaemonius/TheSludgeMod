using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Content.Prefixes
{
    public class Ardent : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Magic;

        public override float RollChance(Item item) => 5f;

        public override bool CanRoll(Item item) => true;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 1.1f;
            useTimeMult *= 0.95f;
        }

        public override void Apply(Item item)
        {
            item.ArmorPenetration += 5;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.2f;
        }
    }

    public class ArdentTooltip : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.prefix == ModContent.PrefixType<Ardent>())
            {
                var line = new TooltipLine(Mod, "PrefixArmorPenetration", "+5 armor penetration")
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