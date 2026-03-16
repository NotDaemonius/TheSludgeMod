using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Prefixes
{
    public class Inoperable : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Ranged;

        public override float RollChance(Item item)
        {
            return 5f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 0.95f;
            useTimeMult *= 1.15f;
            shootSpeedMult *= 0.95f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 0.65f;
        }
    }
}