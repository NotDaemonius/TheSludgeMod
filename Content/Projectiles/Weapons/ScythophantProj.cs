using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class ScythophantProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 80;
        Projectile.height = 80;
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

        if (player.dead || !player.active || !player.channel)
        {
            Projectile.Kill();
            return;
        }

        player.itemTime = 2;
        player.itemAnimation = 2;
        player.direction = (Main.MouseWorld.X > player.Center.X) ? 1 : -1;

        if (Projectile.localAI[1] == 0f)
        {
            Projectile.localAI[0] = -MathHelper.PiOver2 + MathHelper.PiOver4;
            Projectile.localAI[1] = 1f;
        }

        float spinSpeed = 0.2f;
        Projectile.localAI[0] += spinSpeed * player.direction;
        float angleOffset = MathHelper.PiOver4;
        float orbitRadius = 48f;
        float orbitAngle = Projectile.localAI[0];
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - float.Pi);
        float visualOffset = (player.direction == -1) ? MathHelper.PiOver2 : 0f;
        Projectile.rotation = orbitAngle + visualOffset;
        Projectile.spriteDirection = player.direction;
        Vector2 offset = (orbitAngle - angleOffset).ToRotationVector2() * orbitRadius;
        Projectile.Center = player.Center + offset;
        Projectile.ai[0]++;
        int fireRate = 6;

        if (Projectile.ai[0] >= fireRate)
        {
            Projectile.ai[0] = 0;

            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 shootVelocity = (orbitAngle - angleOffset).ToRotationVector2() * 12f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, shootVelocity, ModContent.ProjectileType<ScythophantProj2>(), Projectile.damage / 2, Projectile.knockBack * 0.5f, Projectile.owner);
            }
        }
    }
}