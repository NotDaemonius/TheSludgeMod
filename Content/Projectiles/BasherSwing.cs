using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles
{
    public class BasherSwing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Setup trail cache for the afterimages
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // Records rotation for smooth sword trails
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1; // Infinite pierce
            Projectile.tileCollide = false; // Swings through walls
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true; // Prevents hitting things through walls you can't see
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Kill projectile if player is dead or CC'd
            if (player.dead || !player.active || player.CCed)
            {
                Projectile.Kill();
                return;
            }

            int duration = player.itemAnimationMax;
            Projectile.timeLeft = 2; // Keep alive as long as we update it

            // Progress timer
            Projectile.ai[0]++;
            float progress = Projectile.ai[0] / duration;

            if (Projectile.ai[0] >= duration)
            {
                Projectile.Kill();
            }

            // Calculate Swing Arc
            float baseAngle = Projectile.velocity.ToRotation();
            float swingAngle = MathHelper.Lerp(-MathHelper.PiOver2, MathHelper.PiOver2, progress);

            // Flip the swing direction depending on which way the player faces
            float playerDirection = Projectile.ai[1];
            swingAngle *= playerDirection;

            float currentAngle = baseAngle + swingAngle;

            // Lock the sword to the player's shoulder/arm area
            Vector2 armPosition = player.RotatedRelativePoint(player.MountedCenter);
            float swordDistance = 45f; // How far out the sword is held

            Projectile.Center = armPosition + currentAngle.ToRotationVector2() * swordDistance;

            // Sprite rotation (Add Pi/4 if your sword sprite points up and right diagonally)
            Projectile.rotation = currentAngle + MathHelper.PiOver4;

            // Animate the player's arm
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, currentAngle - MathHelper.PiOver2);
            player.heldProj = Projectile.whoAmI;

            // Cyan and Red Dust Particles
            if (Main.rand.NextBool(2))
            {
                int dustType = Main.rand.NextBool() ? DustID.IceTorch : DustID.RedTorch;
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.scale = 1.2f;
            }
        }

        // Custom drawing for the alternating Cyan/Red afterimages
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);

            // Draw Trails
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);

                // Fade out the trail the further back it is
                float trailAlpha = (Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length;

                // Alternate between Cyan and Red
                Color color = k % 2 == 0 ? Color.Cyan : Color.Crimson;
                color *= trailAlpha * 0.7f; // Multiply by 0.7 to make it slightly transparent

                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            // Draw the actual sword on top of the trails
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

            return false; // Prevent vanilla from trying to draw the sprite again
        }
    }
}