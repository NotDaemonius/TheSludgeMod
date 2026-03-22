using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class LeadPaintProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.tileCollide = true;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 360;
    }

    public override void AI()
    {
        Projectile.velocity.Y += 0.3f;
        Projectile.rotation += Projectile.velocity.X * 0.05f;

        if (Main.rand.NextBool(3))
        {
            int trailDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow, 0f, 0f, 0, Color.White, 1f);
            Main.dust[trailDust].velocity = new Vector2(0f, 3f);
            Main.dust[trailDust].noGravity = false;
        }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.Kill();
        return false;
    }

    public override void Kill(int timeLeft)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient) return;
        SpawnPuddles();
        SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
    }

    private void SpawnPuddles()
    {
        int count = Main.rand.Next(20, 31);

        for (int i = 0; i < count; i++)
        {
            Vector2 velocity = Main.rand.NextVector2Circular(7f, 7f);
            if (velocity.Length() < 2f) velocity = Vector2.Normalize(velocity) * 2f;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<LeadPaintPuddle>(), Projectile.damage / 4, 0f, Projectile.owner);
        }
    }
}