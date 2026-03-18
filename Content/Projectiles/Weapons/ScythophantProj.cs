using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class ScythophantProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active || !player.channel)
            {
                Projectile.Kill();
                return;
            }

            player.itemTime = 2;
            player.itemAnimation = 2;
            player.direction = (Main.MouseWorld.X > player.Center.X) ? 1 : -1;
            float spinSpeed = 0.2f;
            Projectile.rotation += spinSpeed;
            float angleOffset = MathHelper.PiOver4;
            float orbitRadius = 56f;
            Vector2 offset = (Projectile.rotation - angleOffset).ToRotationVector2() * orbitRadius;
            Projectile.Center = player.Center + offset;
            Projectile.ai[0]++;
            int fireRate = 6;

            if (Projectile.ai[0] >= fireRate)
            {
                Projectile.ai[0] = 0;

                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 shootVelocity = (Projectile.rotation - angleOffset).ToRotationVector2() * 12f;
                    Projectile.NewProjectile( Projectile.GetSource_FromThis(), Projectile.Center, shootVelocity, ModContent.ProjectileType<ScythophantProj2>(), Projectile.damage / 2, Projectile.knockBack * 0.5f, Projectile.owner);
                }
            }
        }
    }
}