using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.NPCs.TheMainframe;
public class TheMainframeLaser : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Type] = 5;
        ProjectileID.Sets.TrailingMode[Type] = 0;
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.EyeLaser);
        Projectile.width = 4;
        Projectile.height = 4;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.EyeLaser;
        Projectile.light = 0f;
    }

    public override void PostAI() => Lighting.AddLight(Projectile.Center, 1.0f, 0.0f, 0.0f);

    public override void OnKill(int timeLeft) => Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
}
