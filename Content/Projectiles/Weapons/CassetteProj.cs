using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class CassetteProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.ThornChakram);
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.DamageType = DamageClass.Summon;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.NextBool(2)) target.AddBuff(BuffID.Poisoned, 420);
    }
}

