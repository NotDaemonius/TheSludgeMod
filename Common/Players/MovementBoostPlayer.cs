using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.Players
{
    public class MovementBoostPlayer : ModPlayer
    {
        private static readonly bool CalamityLoaded = ModLoader.TryGetMod("CalamityMod", out _);

        public override void PostUpdateRunSpeeds()
        {
            if (CalamityLoaded) return;
            Player.maxRunSpeed += 1f;
            Player.runAcceleration += 0.01f;
            Player.jumpHeight += 4;
        }
    }
}