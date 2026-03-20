using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class LeadPaintPuddle : ModProjectile
    {
        private const int LingerTime = 300;
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = LingerTime;
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[0] = Main.rand.NextFloat(-0.25f, 0.25f);
                if (Projectile.ai[0] == 0f) Projectile.ai[0] = 0.1f;
                Projectile.ai[1] = 1f;
            }

            Projectile.velocity.Y += 0.4f;

            if (Projectile.velocity.Y > 12f)
                Projectile.velocity.Y = 12f;

            float speed = Projectile.velocity.Length();
            float spinFactor = MathHelper.Clamp(speed / 7f, 0f, 1f);
            Projectile.rotation += Projectile.ai[0] * spinFactor;

            if (Projectile.timeLeft < 120)
                Projectile.alpha = (int)(255 * (1f - Projectile.timeLeft / 120f));
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 120);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            Color drawColor = lightColor * (1f - Projectile.alpha / 255f);
            Vector2 origin = tex.Size() / 2f;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}