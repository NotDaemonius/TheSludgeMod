using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Prefixes
{
    public class Rigid : ModPrefix
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
            damageMult *= 1.1f;
            knockbackMult *= 1.1f;
            shootSpeedMult *= 0.9f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 0.95f;
        }
    }
}