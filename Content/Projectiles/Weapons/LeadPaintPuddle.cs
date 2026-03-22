using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class LeadPaintPuddle : ModProjectile
{
    private int LingerTime = 300;

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Type] = 8;
        ProjectileID.Sets.TrailingMode[Type] = 3;
    }

    public override void SetDefaults()
    {
        Projectile.width = 4;
        Projectile.height = 4;
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
        if (Projectile.velocity.Y > 12f) Projectile.velocity.Y = 12f;
        float speed = Projectile.velocity.Length();
        float spinFactor = MathHelper.Clamp(speed / 7f, 0f, 1f);
        Projectile.rotation += Projectile.ai[0] * spinFactor;
        if (Projectile.timeLeft < 120) Projectile.alpha = (int) (255 * (1f - Projectile.timeLeft / 120f));
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
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        if (Projectile.velocity != Vector2.Zero)
        {
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, drawPos, null, lightColor * (1f - Projectile.alpha / 255f), Projectile.rotation, drawOrigin, Projectile.scale * ((Projectile.oldPos.Length - k) / (float) Projectile.oldPos.Length), SpriteEffects.None, 0);
            }
        }

        Color drawColor = lightColor * (1f - Projectile.alpha / 255f);
        Vector2 origin = texture.Size() / 2f;
        Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
