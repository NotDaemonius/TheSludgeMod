using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.Players
{
    public class DodgyBuilderPlayer : ModPlayer
    {
        public bool hasDodgyBuilder;
        private int originalTileTargetX;
        private int originalTileTargetY;
        private bool offsetApplied;

        public override void ResetEffects() => hasDodgyBuilder = false;

        public override bool PreItemCheck()
        {
            offsetApplied = false;

            if (hasDodgyBuilder && Player.controlUseItem && (Player.HeldItem.createTile >= 0 || Player.HeldItem.pick > 0) && !Player.HeldItem.IsAir)
            {
                originalTileTargetX = Player.tileTargetX;
                originalTileTargetY = Player.tileTargetY;
                Player.tileTargetX += Main.rand.Next(-1, 2);
                Player.tileTargetY += Main.rand.Next(-1, 2);
                offsetApplied = true;
            }

            return true;
        }

        public override void PostItemCheck()
        {
            if (offsetApplied)
            {
                Player.tileTargetX = originalTileTargetX;
                Player.tileTargetY = originalTileTargetY;
                offsetApplied = false;
            }
        }
    }
}