using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common;

namespace TheSludgeMod.Content.Projectiles
{
    /// <summary>
    /// Handles the visual swing arc, melee hit detection, red fire trail,
    /// afterimages, additive glow, and camera shake for CoolSword.
    /// </summary>
    public class CoolSwordSwingProj : ModProjectile
    {
        // ─── Constants ────────────────────────────────────────────────────────────
        private const int SwingDuration = 12;          // Matches Item.useAnimation
        private const float SwingArc = MathHelper.Pi * 1.1f; // ~200° arc
        private const float ArmLength = 62f;          // Pixels from player centre

        // ─── AI slot shortcuts ────────────────────────────────────────────────────
        /// <summary>Player.direction captured on first frame (+1 or -1).</summary>
        private ref float SwingDirection => ref Projectile.ai[0];
        /// <summary>World-space angle at which the swing begins.</summary>
        private ref float StartAngle => ref Projectile.ai[1];

        /// <summary>True once the first-frame initialisation has run.</summary>
        private bool Initialized => SwingDirection != 0f;

        // ─── Setup ────────────────────────────────────────────────────────────────
        public override void SetStaticDefaults()
        {
            // Enable position/rotation caching for afterimage rendering
            ProjectileID.Sets.TrailingMode[Type] = 2; // Precise per-frame caching
            ProjectileID.Sets.TrailCacheLength[Type] = 9;
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;  // Line-of-sight check before hitting
            Projectile.penetrate = -1;    // Hit multiple enemies during swing
            Projectile.timeLeft = SwingDuration;
            // Per-enemy cooldown so the sword doesn't spam the same target mid-swing
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        // ─── Core AI ─────────────────────────────────────────────────────────────
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // ── First-frame init ──────────────────────────────────────────────
            if (!Initialized)
            {
                SwingDirection = player.direction;

                // Compute start angle so the arc is centred on the aim direction.
                // Multiplying by SwingDirection mirrors the sweep correctly for
                // left-facing players.
                float midAngle = Projectile.velocity.ToRotation();
                StartAngle = midAngle - (SwingArc / 2f) * SwingDirection;

                // Camera punch toward the swing direction
                if(Main.myPlayer == Projectile.owner
    && !ModContent.GetInstance<ClientConfig>().DisableScreenShake)
{
                    Vector2 punchDir = Projectile.velocity.SafeNormalize(Vector2.UnitX);
                    Main.instance.CameraModifiers.Add(
                        new PunchCameraModifier(player.Center, punchDir, 2f, 6f, 8, 1000f, FullName));
                }
            }

            // ── Kill if owner disappears ──────────────────────────────────────
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            // ── Swing progress ────────────────────────────────────────────────
            // progress: 0 = start of swing, 1 = end
            float progress = 1f - Projectile.timeLeft / (float)SwingDuration;

            // SmoothStep: slow → fast → slow gives a snappy sword feel
            float smooth = MathHelper.SmoothStep(0f, 1f, progress);

            float currentAngle = StartAngle + SwingArc * SwingDirection * smooth;

            // Sword tip direction and position (hitbox centre)
            Vector2 direction2D = Vector2.UnitX.RotatedBy(currentAngle);
            Projectile.Center = player.MountedCenter + direction2D * ArmLength;

            // Sprite rotation: +45° offset aligns typical sword diagonal sprites
            Projectile.rotation = currentAngle + MathHelper.PiOver4;

            // ── Player arm pose ───────────────────────────────────────────────
            // heldProj suppresses the default held-item graphic
            player.heldProj = Projectile.whoAmI;
            // Arm rotates to track the blade throughout the arc
            player.SetCompositeArmFront(
                true,
                Player.CompositeArmStretchAmount.Full,
                currentAngle - MathHelper.PiOver2);

            // ── Red fire particles along the blade ────────────────────────────
            EmitFireParticles(currentAngle, direction2D, smooth);

            // ── Dynamic red point light ───────────────────────────────────────
            // Pulses slightly based on game time for a living-fire feel
            float pulse = 0.8f + 0.2f * (float)Math.Sin(Main.timeForVisualEffects * 0.4f);
            Lighting.AddLight(Projectile.Center, 0.95f * pulse, 0.12f * pulse, 0.05f * pulse);
        }

        // ─── Fire particle trail ──────────────────────────────────────────────────
        private void EmitFireParticles(float angle, Vector2 swingDir, float progress)
        {
            // Emit more particles during the fast mid-swing phase
            bool peakPhase = progress > 0.2f && progress < 0.85f;
            int count = peakPhase ? 3 : 1;

            for (int i = 0; i < count; i++)
            {
                // Scatter particles along the blade's length
                float t = Main.rand.NextFloat();
                Vector2 bladePoint = Projectile.Center + swingDir * (t * 26f - 13f);

                Dust dust = Dust.NewDustDirect(
                    bladePoint - new Vector2(5f),
                    10, 10,
                    DustID.RedTorch,
                    0f, 0f,
                    Alpha: 0,
                    newColor: new Color(255, 0, 0),
                    Scale: Main.rand.NextFloat(1.2f, 2.2f));

                dust.noGravity = true;
                // Velocity: perpendicular to blade direction (follows the swing arc)
                dust.velocity = swingDir.RotatedBy(MathHelper.PiOver2 * SwingDirection) * 2.5f
                               + Main.rand.NextVector2Circular(1.2f, 1.2f);
            }
        }

        // ─── On-hit burst ─────────────────────────────────────────────────────────
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Burst of crimson sparks at the hit point
            for (int i = 0; i < 18; i++)
            {
                Dust spark = Dust.NewDustDirect(
                    target.Center - new Vector2(22f),
                    44, 44,
                    DustID.RedTorch,
                    0f, 0f,
                    Alpha: 0,
                    newColor: new Color(255, 0, 0),
                    Scale: Main.rand.NextFloat(1.5f, 2.8f));

                spark.velocity = Main.rand.NextVector2Circular(6f, 6f);
                spark.noGravity = Main.rand.NextBool(2);
            }

            // Extra camera punch away from the hit on the owning client only
            if (Main.myPlayer == Projectile.owner
    && !ModContent.GetInstance<ClientConfig>().DisableScreenShake)
            {
                Vector2 punchDir = (target.Center - Main.player[Projectile.owner].Center)
                                   .SafeNormalize(Vector2.UnitY);
                Main.instance.CameraModifiers.Add(
                    new PunchCameraModifier(target.Center, punchDir, 7f, 9f, 7, 1000f, FullName + "Hit"));
            }
        }

        // ─── Custom drawing ───────────────────────────────────────────────────────
        public override bool PreDraw(ref Color lightColor)
        {
            // Load the sword's own item texture to draw it at the swing position.
            // The string must match your mod's folder structure exactly.
            Texture2D tex = ModContent.Request<Texture2D>(
                "TheSludgeMod/Content/Items/CoolSword",
                AssetRequestMode.ImmediateLoad).Value;
            Vector2 origin = tex.Size() / 2f;
            SpriteBatch sb = Main.spriteBatch;

            // ── 1. Red afterimage trail ───────────────────────────────────────
            for (int i = Projectile.oldPos.Length - 1; i >= 1; i--)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;

                float age = i / (float)Projectile.oldPos.Length;
                float alpha = (1f - age) * 0.55f;
                // Slightly expand older ghosts for a heat-distortion look
                float scale = Projectile.scale * (1f + age * 0.15f);

                sb.Draw(
                    tex,
                    Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition,
                    null,
                    new Color(255, 0, 0) * alpha,
                    Projectile.oldRot[i],
                    origin,
                    scale,
                    SpriteEffects.None,
                    0f);
            }

            // ── 2. Additive red glow layer (fiery aura) ───────────────────────
            sb.End();
            sb.Begin(
                SpriteSortMode.Immediate,
                BlendState.Additive,
                Main.DefaultSamplerState,
                DepthStencilState.None,
                RasterizerState.CullNone,
                null,
                Main.GameViewMatrix.TransformationMatrix);

            float glowPulse = 0.38f + 0.12f * (float)Math.Sin(Main.timeForVisualEffects * 0.35f);

            // Soft wide glow
            sb.Draw(
                tex,
                Projectile.Center - Main.screenPosition,
                null,
                new Color(255, 0, 0) * glowPulse,
                Projectile.rotation,
                origin,
                Projectile.scale * 1.18f,
                SpriteEffects.None,
                0f);

            // Tighter bright core
            sb.Draw(
                tex,
                Projectile.Center - Main.screenPosition,
                null,
                new Color(255, 0, 0) * (glowPulse * 0.55f),
                Projectile.rotation,
                origin,
                Projectile.scale * 1.02f,
                SpriteEffects.None,
                0f);

            // Return to standard alpha blending
            sb.End();
            sb.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                Main.DefaultSamplerState,
                DepthStencilState.None,
                RasterizerState.CullNone,
                null,
                Main.GameViewMatrix.TransformationMatrix);

            // ── 3. Main sword sprite ──────────────────────────────────────────
            sb.Draw(
                tex,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                origin,
                Projectile.scale,
                SpriteEffects.None,
                0f);

            return false; // Skip Terraria's default drawing
        }
    }
}