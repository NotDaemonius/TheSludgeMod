using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class DevilKnife : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.Bullet;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.friendly && Random.Shared.Next(0, 5) == 4)
            {
                Main.player[Projectile.owner].Heal(1);
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig);

            for (int i = 0; i < 3; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-3f, -1f), 0, Color.White, Main.rand.NextFloat(0.8f, 1.4f));
                Main.dust[dustIndex].noGravity = false;
                Main.dust[dustIndex].velocity *= 1.5f;
            }
        }
    }
}
