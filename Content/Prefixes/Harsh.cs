using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Prefixes
{
    public class Harsh : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Magic;

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
            damageMult *= 1.15f;
            useTimeMult *= 1.05f;
            manaMult *= 1.1f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 0.95f;
        }
    }
}