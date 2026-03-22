using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class TungstenEnforcedUmbrellaProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(44);
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

    public override void AI() => SetVisualOffsets();

    private void SetVisualOffsets()
    {
        const int HalfSpriteWidth = 44 / 2;
        const int HalfSpriteHeight = 44 / 2;
        int HalfProjWidth = Projectile.width / 2;
        int HalfProjHeight = Projectile.height / 2;
        DrawOriginOffsetX = 0;
        DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
        DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
    }
}
