using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.NPCs.TheMainframe;

public class MainframeGhostSpooky : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 242;
        Projectile.height = 182;
        Projectile.damage = 0;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 15;
    }

    public override void AI()
    {
        Projectile.alpha = (int) MathHelper.Lerp(200f, 255f, (15f - Projectile.timeLeft) / 15f);
    }
}
