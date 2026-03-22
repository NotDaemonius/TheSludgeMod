using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class GungnirBullet : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 14;
        Projectile.height = 14;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 600;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = Terraria.GameContent.TextureAssets.Item[ItemID.Gungnir].Value;
        float rotation = Projectile.rotation + MathHelper.ToRadians(-45f);
        Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, lightColor, rotation, texture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }

    public override void PostAI()
    {
        Vector2 behindDir = -Vector2.Normalize(Projectile.velocity);
        Vector2 perp = new Vector2(-behindDir.Y, behindDir.X);
        float spread = 3f;

        for (int stream = 0; stream < 2; stream++)
        {
            float side = (stream == 0) ? -1f : 1f;
            Vector2 spawnOffset = perp * side * spread;
            int dust = Dust.NewDust(Projectile.Center + spawnOffset - Main.screenPosition * 0f, 0, 0, DustID.HallowedTorch, behindDir.X * 2f + Projectile.velocity.X * 0.1f, behindDir.Y * 2f + Projectile.velocity.Y * 0.1f, 0, default, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].noLight = false;
        }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

        for (int i = 0; i < 20; i++)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedWeapons, oldVelocity.X * -0.5f, oldVelocity.Y * -0.5f, 100, default, 1.5f);
            Main.dust[dust].noGravity = true;
        }

        return true;
    }
}