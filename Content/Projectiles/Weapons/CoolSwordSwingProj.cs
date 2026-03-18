using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class CoolSwordSwingProj : ModProjectile
    {
        private const int SwingDuration = 12;
        private const float SwingArc = MathHelper.Pi * 1.1f;
        private const float ArmLength = 62f;
        private ref float SwingDirection => ref Projectile.ai[0];
        private ref float StartAngle => ref Projectile.ai[1];
        private bool Initialized => SwingDirection != 0f;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
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
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = SwingDuration;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!Initialized)
            {
                SwingDirection = player.direction;
                float midAngle = Projectile.velocity.ToRotation();
                StartAngle = midAngle - (SwingArc / 2f) * SwingDirection;

                if(Main.myPlayer == Projectile.owner && !ModContent.GetInstance<ClientConfig>().DisableScreenShake)
                {
                    Vector2 punchDir = Projectile.velocity.SafeNormalize(Vector2.UnitX);
                    Main.instance.CameraModifiers.Add(
                    new PunchCameraModifier(player.Center, punchDir, 2f, 6f, 8, 1000f, FullName));
                }
            }

            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            float progress = 1f - Projectile.timeLeft / (float)SwingDuration;
            float smooth = MathHelper.SmoothStep(0f, 1f, progress);
            float currentAngle = StartAngle + SwingArc * SwingDirection * smooth;
            Vector2 direction2D = Vector2.UnitX.RotatedBy(currentAngle);
            Projectile.Center = player.MountedCenter + direction2D * ArmLength;
            Projectile.rotation = currentAngle + MathHelper.PiOver4;
            player.heldProj = Projectile.whoAmI;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, currentAngle - MathHelper.PiOver2);
            EmitFireParticles(currentAngle, direction2D, smooth);
            float pulse = 0.8f + 0.2f * (float)Math.Sin(Main.timeForVisualEffects * 0.4f);
            Lighting.AddLight(Projectile.Center, 0.95f * pulse, 0.12f * pulse, 0.05f * pulse);
        }
        private void EmitFireParticles(float angle, Vector2 swingDir, float progress)
        {
            bool peakPhase = progress > 0.2f && progress < 0.85f;
            int count = peakPhase ? 3 : 1;

            for (int i = 0; i < count; i++)
            {
                float t = Main.rand.NextFloat();
                Vector2 bladePoint = Projectile.Center + swingDir * (t * 26f - 13f);
                Dust dust = Dust.NewDustDirect(bladePoint - new Vector2(5f), 10, 10, DustID.RedTorch, 0f, 0f, Alpha: 0, newColor: new Color(255, 0, 0), Scale: Main.rand.NextFloat(1.2f, 2.2f));
                dust.noGravity = true;
                dust.velocity = swingDir.RotatedBy(MathHelper.PiOver2 * SwingDirection) * 2.5f + Main.rand.NextVector2Circular(1.2f, 1.2f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 18; i++)
            {
                Dust spark = Dust.NewDustDirect(target.Center - new Vector2(22f), 44, 44, DustID.RedTorch, 0f, 0f, Alpha: 0, newColor: new Color(255, 0, 0), Scale: Main.rand.NextFloat(1.5f, 2.8f));
                spark.velocity = Main.rand.NextVector2Circular(6f, 6f);
                spark.noGravity = Main.rand.NextBool(2);
            }

            if (Main.myPlayer == Projectile.owner && !ModContent.GetInstance<ClientConfig>().DisableScreenShake)
            {
                Vector2 punchDir = (target.Center - Main.player[Projectile.owner].Center).SafeNormalize(Vector2.UnitY);
                Main.instance.CameraModifiers.Add(new PunchCameraModifier(target.Center, punchDir, 7f, 9f, 7, 1000f, FullName + "Hit"));
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("TheSludgeMod/Content/Items/Weapons/CoolSword", AssetRequestMode.ImmediateLoad).Value;
            Vector2 origin = tex.Size() / 2f;
            SpriteBatch sb = Main.spriteBatch;

            for (int i = Projectile.oldPos.Length - 1; i >= 1; i--)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;

                float age = i / (float)Projectile.oldPos.Length;
                float alpha = (1f - age) * 0.55f;
                float scale = Projectile.scale * (1f + age * 0.15f);
                sb.Draw(tex, Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition, null, new Color(255, 0, 0) * alpha, Projectile.oldRot[i], origin, scale, SpriteEffects.None, 0f);
            }

            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            float glowPulse = 0.38f + 0.12f * (float)Math.Sin(Main.timeForVisualEffects * 0.35f);
            sb.Draw(tex, Projectile.Center - Main.screenPosition, null, new Color(255, 0, 0) * glowPulse, Projectile.rotation, origin, Projectile.scale * 1.18f, SpriteEffects.None, 0f);
            sb.Draw(tex, Projectile.Center - Main.screenPosition, null, new Color(255, 0, 0) * (glowPulse * 0.55f), Projectile.rotation, origin, Projectile.scale * 1.02f, SpriteEffects.None,0f);
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            sb.Draw(tex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);

            return false;
        }
    }
}