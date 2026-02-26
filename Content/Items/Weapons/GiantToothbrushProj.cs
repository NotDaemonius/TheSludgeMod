using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class GiantToothbrushProj : ModProjectile
    {
        private const float MaxReach = 120f;
        private const int o = 12;
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!owner.active || owner.dead || owner.CCed)
            {
                Projectile.Kill();
                return;
            }

            Projectile.direction = owner.direction;
            owner.heldProj = Projectile.whoAmI;
            owner.SetDummyItemTime(2);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
            Projectile.ai[0] += 1f;
            Projectile.timeLeft = 300;
            float halfDuration = 12f;
            float progress;

            if (Projectile.ai[0] <= halfDuration)
            progress = Projectile.ai[0] / halfDuration;
            else
            progress = 1f - ((Projectile.ai[0] - halfDuration) / halfDuration);

            if (Projectile.ai[0] >= halfDuration * 2f)
            {
                Projectile.Kill();
                return;
            }

            Vector2 direction = Vector2.Normalize(Projectile.velocity);
            Projectile.Center = owner.MountedCenter + direction * MathHelper.Lerp(0f, MaxReach, progress);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            bool facingLeft = Projectile.direction == -1;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation + (facingLeft ? -MathHelper.PiOver2 : 0f), facingLeft ? new Vector2(texture.Width - o, o) : new Vector2(o, o), Projectile.scale, facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            return false;
        }
    }
}