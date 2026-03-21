using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class PalmWoodTreeWandProj : ModProjectile
    {
        private const int FrameSpeed = 4;
        private const float CurveMagnitude = 0.003f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.velocity.Y += 0.05f;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = Main.rand.NextBool() ? 1f : -1f;
                Projectile.netUpdate = true;
            }

            float curveDir = Projectile.ai[0];
            float baseRotate = curveDir * CurveMagnitude;
            float jitter = Main.rand.NextFloat(-0.004f, 0.004f);
            Projectile.velocity = Projectile.velocity.RotatedBy(baseRotate + jitter);
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= FrameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }

            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.JunglePlants, Projectile.velocity.X * 0.3f, Projectile.velocity.Y * 0.3f, Scale: Main.rand.NextFloat(0.4f, 0.8f));
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            SpawnDeathDust();
        }
        public override void OnHitNPC(Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        {
            SpawnDeathDust();
        }
        private void SpawnDeathDust()
        {
            SoundEngine.PlaySound(SoundID.Grass, Projectile.position);

            for (int i = 0; i < 8; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.JunglePlants, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), Alpha: 80, Scale: Main.rand.NextFloat(0.6f, 1.1f));
                Main.dust[dust].noGravity = false;
            }

            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.JunglePlants, Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-1.5f, 1.5f), Scale: Main.rand.NextFloat(0.5f, 0.9f));
                Main.dust[dust].noGravity = true;
            }
        }
    }
}