using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Zinc;

public class ZincShortswordProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(18);
        Projectile.aiStyle = ProjAIStyleID.ShortSword;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1f;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 360;
        Projectile.hide = true;
    }

    public override void AI()
    {
        SetVisualOffsets();

        if (Projectile.spriteDirection == 1) Projectile.rotation -= MathHelper.ToRadians(45f);
        else Projectile.rotation += MathHelper.ToRadians(45f);
    }

    private void SetVisualOffsets()
    {
        const int HalfSpriteWidth = 32 / 2;
        const int HalfSpriteHeight = 32 / 2;
        int HalfProjWidth = Projectile.width / 2;
        int HalfProjHeight = Projectile.height / 2;
        DrawOriginOffsetX = 0;
        DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
        DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
    }
}