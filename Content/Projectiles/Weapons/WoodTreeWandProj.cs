using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class WoodTreeWandProj : ModProjectile
    {
        // How quickly the leaf animates through its 5 frames (lower = faster)
        private const int FrameSpeed = 4;

        // Controls how strongly the leaf curves each tick.
        // Keep this small — large values make it look erratic rather than leafy.
        private const float CurveMagnitude = 0.003f;

        // We seed per-projectile randomness in AI_Style so the curve direction is
        // consistent for a given leaf (not re-rolled every tick).
        // Store curve direction in Projectile.ai[0]: +1 or -1.

        public override void SetStaticDefaults()
        {
            // Tell the engine this sprite has 5 rows (frames stacked vertically,
            // exactly like the Leaf Blower projectile sheet).
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;          // Hits one enemy then disappears
            Projectile.timeLeft = 120;        // ~3 seconds at 60 fps
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;

            // Slight gravity so the leaf arcs downward naturally
            Projectile.velocity.Y += 0.05f;
        }

        public override void AI()
        {
            // ── 1. Seed curve direction on the very first tick ───────────────────
            // ai[0] starts at 0; we use that as a "not yet seeded" sentinel.
            if (Projectile.ai[0] == 0f)
            {
                // Assign +1 or -1 randomly. Avoid 0 by using the sign trick.
                Projectile.ai[0] = Main.rand.NextBool() ? 1f : -1f;
                Projectile.netUpdate = true;   // Sync the seed to other clients in multiplayer
            }

            // ── 2. Random leaf curving ────────────────────────────────────────────
            // Rotate the velocity vector slightly each tick in the seeded direction,
            // then add a tiny extra random nudge so it feels organic, not mechanical.
            float curveDir = Projectile.ai[0];
            float baseRotate = curveDir * CurveMagnitude;
            float jitter = Main.rand.NextFloat(-0.004f, 0.004f);   // tiny per-frame noise
            Projectile.velocity = Projectile.velocity.RotatedBy(baseRotate + jitter);

            // ── 3. Sprite rotation — face the direction of travel ────────────────
            Projectile.rotation = Projectile.velocity.ToRotation();

            // ── 4. Frame animation (5-frame loop, like the Leaf Blower) ──────────
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= FrameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }

            // ── 5. Ambient leaf dust trail ────────────────────────────────────────
            if (Main.rand.NextBool(4))   // ~25 % chance per tick — subtle, not spammy
            {
                int dust = Dust.NewDust(
                    Projectile.position, Projectile.width, Projectile.height,
                    DustID.GrassBlades,
                    Projectile.velocity.X * 0.3f,
                    Projectile.velocity.Y * 0.3f,
                    Scale: Main.rand.NextFloat(0.4f, 0.8f)
                );
                Main.dust[dust].noGravity = true;
            }
        }

        // Called when the projectile hits a tile or an enemy, OR when timeLeft hits 0
        public override void OnKill(int timeLeft)
        {
            SpawnDeathDust();
        }

        // Also called specifically on enemy hit (before the projectile is killed)
        public override void OnHitNPC(Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        {
            SpawnDeathDust();
        }

        private void SpawnDeathDust()
        {
            SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
            // Spawn a small burst of leaf + green dust on impact/expiry
            for (int i = 0; i < 8; i++)
            {
                int dust = Dust.NewDust(
                    Projectile.position, Projectile.width, Projectile.height,
                    DustID.GrassBlades,
                    Main.rand.NextFloat(-2f, 2f),
                    Main.rand.NextFloat(-2f, 2f),
                    Alpha: 80,
                    Scale: Main.rand.NextFloat(0.6f, 1.1f)
                );
                Main.dust[dust].noGravity = false;
            }

            // A couple of green nature dust particles for extra pop
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(
                    Projectile.position, Projectile.width, Projectile.height,
                    DustID.GrassBlades,
                    Main.rand.NextFloat(-1.5f, 1.5f),
                    Main.rand.NextFloat(-1.5f, 1.5f),
                    Scale: Main.rand.NextFloat(0.5f, 0.9f)
                );
                Main.dust[dust].noGravity = true;
            }
        }
    }
}