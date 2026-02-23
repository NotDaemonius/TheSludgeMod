using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class BasherSwing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
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

            if (player.dead || !player.active || player.CCed)
            {
                Projectile.Kill();
                return;
            }

            int duration = player.itemAnimationMax;
            Projectile.timeLeft = 2;
            Projectile.ai[0]++;
            float progress = Projectile.ai[0] / duration;

            if (Projectile.ai[0] >= duration)
            {
                Projectile.Kill();
            }

            float baseAngle = Projectile.velocity.ToRotation();
            float swingAngle = MathHelper.Lerp(-MathHelper.PiOver2, MathHelper.PiOver2, progress);
            float playerDirection = Projectile.ai[1];
            swingAngle *= playerDirection;
            float currentAngle = baseAngle + swingAngle;
            Vector2 armPosition = player.RotatedRelativePoint(player.MountedCenter);
            float swordDistance = 45f;
            Projectile.Center = armPosition + currentAngle.ToRotationVector2() * swordDistance;
            Projectile.rotation = currentAngle + MathHelper.PiOver4;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, currentAngle - MathHelper.PiOver2);
            player.heldProj = Projectile.whoAmI;

            if (Main.rand.NextBool(2))
            {
                int dustType = Main.rand.NextBool() ? DustID.IceTorch : DustID.RedTorch;
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.scale = 1.2f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                float trailAlpha = (Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length;
                Color color = k % 2 == 0 ? Color.Cyan : Color.Crimson;
                color *= trailAlpha * 0.7f;
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

            return false;
        }
    }
}