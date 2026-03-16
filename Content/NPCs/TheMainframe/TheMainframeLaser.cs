using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.NPCs.TheMainframe;

public class TheMainframeLaser : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Type] = 5; // The length of old position to be recorded
        ProjectileID.Sets.TrailingMode[Type] = 0; // The recording mode
    }


    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.EyeLaser);
        Projectile.width = 4;
        Projectile.height = 4;
        Projectile.aiStyle = 1;
        Projectile.light = 0.5f;

        AIType = ProjectileID.EyeLaser;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
        for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
        {
            Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float) Projectile.oldPos.Length);
            Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
        }

        return true;
    }
}
