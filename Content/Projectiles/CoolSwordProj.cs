using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles
{
    /// <summary>
    /// Bouncing red energy blade shard — spins, glows, leaves a crimson fire trail,
    /// and explodes in a burst of embers on death. Cooler version of CoolSwordProj.
    /// </summary>
    public class CoolSwordProj : ModProjectile
    {
        // ─── Constants ────────────────────────────────────────────────────────────
        private const float SpinSpeed = 0.28f;  // Radians per tick
        private const float GravityScale = 0.18f;  // Gentle arc (overrides bounce ai gravity)
        private const int TrailLength = 12;
        public static Asset<Texture2D> GlowTexture;

        // ─── AI shortcut ──────────────────────────────────────────────────────────
        /// <summary>Counts bounces so we can escalate effects on each one.</summary>
        private ref float BounceCount => ref Projectile.ai[0];

        // ─── Setup ────────────────────────────────────────────────────────────────
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = TrailLength;

            if (!Main.dedServ)
            {
                GlowTexture = ModContent.Request<Texture2D>(Texture + "Glowmask");
            }
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 0; // No vanilla aiStyle — we handle everything manually
            Projectile.timeLeft = 280;
            Projectile.extraUpdates = 1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 2;
        }

        public override void AI()
        {
            // Manual gravity
            Projectile.velocity.Y += 0.15f;
            // Cap fall speed
            if (Projectile.velocity.Y > 16f)
                Projectile.velocity.Y = 16f;

            // Spin
            Projectile.rotation += SpinSpeed * Math.Sign(Projectile.velocity.X);

            // Red pulsating light
            float pulse = 0.7f + 0.3f * (float)Math.Sin(Main.timeForVisualEffects * 0.5f);
            Lighting.AddLight(Projectile.Center, 0.95f * pulse, 0.10f * pulse, 0.05f * pulse);

            SpawnTrailDust();
        }

        private void SpawnTrailDust()
        {
            // Core ember
            Dust ember = Dust.NewDustDirect(
                Projectile.position, Projectile.width, Projectile.height,
                DustID.CrimtaneWeapons,
                -Projectile.velocity.X * 0.3f, -Projectile.velocity.Y * 0.3f,
                0,
                new Color(255, 0, 0),
                Main.rand.NextFloat(0.9f, 1.6f));
            ember.noGravity = true;

            // Occasional larger flare for visual interest
            if (Main.rand.NextBool(4))
            {
                Dust flare = Dust.NewDustDirect(
                    Projectile.Center - new Vector2(6f),
                    12, 12,
                    DustID.RedTorch,
                    Main.rand.NextFloat(-1.2f, 1.2f),
                    Main.rand.NextFloat(-1.2f, 1.2f),
                    0,
                    Color.Red,
                    Main.rand.NextFloat(0.6f, 1.1f));
                flare.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            BounceCount++;

            // Reflect off tiles
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                Projectile.velocity.X = -oldVelocity.X * 0.75f;
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                Projectile.velocity.Y = -oldVelocity.Y * 0.75f;

            // Kill after too many bounces
            if (BounceCount >= Projectile.maxPenetrate + 1)
                Projectile.Kill();

            SoundEngine.PlaySound(
                BounceCount >= 3 ? SoundID.Item14 : SoundID.Item34,
                Projectile.position);

            int sparkCount = 6 + (int)BounceCount * 2;
            for (int i = 0; i < sparkCount; i++)
            {
                Dust spark = Dust.NewDustDirect(
                    Projectile.position, Projectile.width, Projectile.height,
                    DustID.RedTorch, 0f, 0f, 0,
                    new Color(255, 0, 0),
                    Main.rand.NextFloat(1.2f, 2.4f));
                spark.velocity = Main.rand.NextVector2Circular(5f, 5f);
                spark.noGravity = Main.rand.NextBool(2);
            }

            return false; // We handled collision ourselves
        }

        // ─── On-hit flash ─────────────────────────────────────────────────────────
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Brief red flash on the target
            target.AddBuff(BuffID.OnFire, 90);

            for (int i = 0; i < 10; i++)
            {
                Dust hit2 = Dust.NewDustDirect(
                    target.Center - new Vector2(14f),
                    28, 28,
                    DustID.RedTorch,
                    0f, 0f,
                    0,
                    new Color(255, 0, 0),
                    Main.rand.NextFloat(1.4f, 2.5f));
                hit2.velocity = Main.rand.NextVector2Circular(5f, 5f);
                hit2.noGravity = true;
            }
        }

        // ─── Death explosion ──────────────────────────────────────────────────────
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            // Radial ring of fire
            int dustCount = 24;
            for (int i = 0; i < dustCount; i++)
            {
                float angle = MathHelper.TwoPi / dustCount * i;
                Vector2 vel = angle.ToRotationVector2() * Main.rand.NextFloat(3f, 7f);

                Dust ring = Dust.NewDustDirect(
                    Projectile.Center - new Vector2(8f),
                    16, 16,
                    DustID.RedTorch,
                    vel.X, vel.Y,
                    0,
                    new Color(255, 0, 0),
                    Main.rand.NextFloat(1.5f, 2.8f));
                ring.noGravity = true;
            }

            // Crimtane core burst
            for (int i = 0; i < 10; i++)
            {
                Dust core = Dust.NewDustDirect(
                    Projectile.Center - new Vector2(10f),
                    20, 20,
                    DustID.RedTorch,
                    Main.rand.NextFloat(-4f, 4f),
                    Main.rand.NextFloat(-4f, 4f),
                    0,
                    Color.Red,
                    Main.rand.NextFloat(1.0f, 1.8f));
                core.noGravity = true;
            }

            // Central bright flash (pure white, quick fade)
            for (int i = 0; i < 5; i++)
            {
                Dust flash = Dust.NewDustDirect(
                    Projectile.Center - new Vector2(4f),
                    8, 8,
                    DustID.RedTorch,
                    Main.rand.NextFloat(-2f, 2f),
                    Main.rand.NextFloat(-2f, 2f),
                    0,
                    Color.Red,
                    Main.rand.NextFloat(1.3f, 2.2f));
                flash.noGravity = true;
            }
        }

        // ─── Custom drawing ───────────────────────────────────────────────────────
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>(
                "TheSludgeMod/Content/Projectiles/CoolSwordProj",
                ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Vector2 origin = tex.Size() / 2f;
            SpriteBatch sb = Main.spriteBatch;

            // ── 1. Afterimage ribbon ──────────────────────────────────────────
            for (int i = Projectile.oldPos.Length - 1; i >= 1; i--)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;

                float age = i / (float)Projectile.oldPos.Length;
                float alpha = (1f - age) * 0.5f;
                float scale = Projectile.scale * MathHelper.Lerp(1.15f, 0.55f, age);

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

            // ── 2. Additive glow pass ─────────────────────────────────────────
            sb.End();
            sb.Begin(
                SpriteSortMode.Immediate,
                BlendState.Additive,
                Main.DefaultSamplerState,
                DepthStencilState.None,
                RasterizerState.CullNone,
                null,
                Main.GameViewMatrix.TransformationMatrix);

            float glowPulse = 0.4f + 0.15f * (float)Math.Sin(Main.timeForVisualEffects * 0.45f);

            // Outer soft glow
            sb.Draw(tex,
                Projectile.Center - Main.screenPosition,
                null,
                new Color(255, 0, 0) * glowPulse,
                Projectile.rotation,
                origin,
                Projectile.scale * 1.6f,
                SpriteEffects.None, 0f);

            // Inner bright core
            sb.Draw(tex,
                Projectile.Center - Main.screenPosition,
                null,
                new Color(255, 140, 140) * (glowPulse * 0.6f),
                Projectile.rotation,
                origin,
                Projectile.scale * 1.05f,
                SpriteEffects.None, 0f);

            // Return to normal blending
            sb.End();
            sb.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                Main.DefaultSamplerState,
                DepthStencilState.None,
                RasterizerState.CullNone,
                null,
                Main.GameViewMatrix.TransformationMatrix);

            // ── 3. Main sprite ────────────────────────────────────────────────
            sb.Draw(tex,
                Projectile.Center - Main.screenPosition,
                null,
                lightColor,
                Projectile.rotation,
                origin,
                Projectile.scale,
                SpriteEffects.None, 0f);
            Texture2D texture = ModContent.Request<Texture2D>(
        "TheSludgeMod/Content/Projectiles/CoolSwordProjGlowmask",
        ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                Projectile.rotation,
                texture.Size() * 0.5f,
                Projectile.scale,
                SpriteEffects.None,
                0f);

            return false;
        }
    }
}