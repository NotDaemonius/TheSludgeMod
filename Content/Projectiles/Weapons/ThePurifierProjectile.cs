using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class ThePurifierProjectile : ModProjectile
{
    float timer = 0;

    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.timeLeft = 200;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.extraUpdates = 1;
        Projectile.penetrate = 4;
    }

    public override void AI()
    {
        timer += 1;
        for (int i = 0; i < 3; i++)
        {
            int agg = Dust.NewDust(Projectile.Center, 4, 4, 6, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), 0, default, 2);
            Main.dust[agg].noGravity = true;
        }

        if (Projectile.ai[0] == 1)
        {
            if (timer > 10)
            {
                Projectile.velocity += new Vector2(0, 0.5f);
                Projectile.velocity *= 0.96f;
            }

            return;
        }

        if (timer % 15 == 0)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, Projectile.type, Projectile.damage, 0, Projectile.owner, 1);
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 120);
    }

    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 10; i++)
        {
            int agg = Dust.NewDust(Projectile.Center, 10, 10, 6, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), 0, default, 2);
        }
    }
}
