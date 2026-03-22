using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

public class UraniumChainsawProj : ModProjectile
{
    public override void SetStaticDefaults() => ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;

    public override void SetDefaults()
    {
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ownerHitCheck = true;
        Projectile.aiStyle = ProjAIStyleID.Drill;
        Projectile.hide = true;
    }

    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        int spriteHalfLength = 26;
        Vector2 offset = Vector2.Normalize(Projectile.velocity) * spriteHalfLength;
        hitbox.X += (int)offset.X;
        hitbox.Y += (int)offset.Y;
    }
}