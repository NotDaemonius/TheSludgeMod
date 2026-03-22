using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class ScythophantProj2 : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CultistBossFireBallClone;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.CloneDefaults(ProjectileID.CultistBossFireBallClone);
            AIType = ProjectileID.CultistBossFireBallClone;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            float maxDetectRadius = 800f;
            NPC closestNPC = FindClosestNPC(maxDetectRadius);

            if (closestNPC != null)
            {
                Vector2 direction = closestNPC.Center - Projectile.Center;
                direction.Normalize();
                direction *= 10f;
                Projectile.velocity = (Projectile.velocity * 20f + direction) / 21f;
            }

            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
        public NPC FindClosestNPC(float maxRange)
        {
            NPC closest = null;
            float distance = maxRange;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.CanBeChasedBy())
                {
                    float tempDist = Vector2.Distance(npc.Center, Projectile.Center);

                    if (tempDist < distance)
                    {
                        distance = tempDist;
                        closest = npc;
                    }
                }
            }

            return closest;
        }
    }
}