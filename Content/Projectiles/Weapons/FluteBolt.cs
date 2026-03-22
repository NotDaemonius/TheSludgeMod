using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class FluteBolt : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.TopazBolt;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.TopazBolt);
        AIType = ProjectileID.TopazBolt;
        Projectile.soundDelay = -1;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.penetrate = -1;
    }
}