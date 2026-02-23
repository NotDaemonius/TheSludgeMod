using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class TheRulebookProj : ModProjectile
    {
        private ref float SoundCooldown => ref Projectile.ai[1];
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Football);
            AIType = ProjectileID.Football;
            Projectile.timeLeft = 180;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.damage = 25;
            Projectile.penetrate = 5;
            Projectile.npcProj = false;
            Projectile.tileCollide = true;
            Projectile.noDropItem = true;
        }
        public override void AI()
        {
            if (SoundCooldown > 0)
            SoundCooldown--;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            const float bounceDamping = 0.65f;

            if (Projectile.velocity.X != oldVelocity.X)
            Projectile.velocity.X = -oldVelocity.X * bounceDamping;

            if (Projectile.velocity.Y != oldVelocity.Y)
            Projectile.velocity.Y = -oldVelocity.Y * bounceDamping;

            const float soundThreshold = 2.5f;
            bool hitHard = (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > soundThreshold) || (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > soundThreshold);

            if (hitHard)
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            return false;
        }

        public override void OnKill(int timeLeft){}
    }
}