using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class NeuralShardProj : ModProjectile
{
    private const float DetectRange = 700f;
    private const float LungeSpeed = 22f;
    private const float IdleOrbitRadius = 60f;
    private const float IdleMaxSpeed = 8f;
    private const float IdleSmoothing = 0.12f;
    private const float ReturnMaxSpeed = 20f;
    private const float ReturnSmoothing = 0.30f;
    private const int LungeDuration = 35;

    private ref float State => ref Projectile.ai[0];

    private ref float Timer => ref Projectile.ai[1];

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 1;
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.friendly = true;
        Projectile.minion = true;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.minionSlots = 1f;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 10;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.timeLeft = 18000;
    }

    public override bool? CanCutTiles() => false;

    public override bool MinionContactDamage() => State == 1f;

    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];
        if (!CheckActive(owner)) return;
        NPC target = FindTarget(owner);

        switch ((int)State)
        {
            case 0: DoIdle(owner, target); break;
            case 1: DoLunge(target); break;
            case 2: DoReturn(owner); break;
        }

        if (Projectile.velocity.LengthSquared() > 0.01f)
        {
            float rotationOffset = State == 0f ? -MathHelper.PiOver4 : -MathHelper.PiOver4 + MathHelper.PiOver2;
            Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
        }
    }

    private void DoIdle(Player owner, NPC target)
    {
        Vector2 toOwner = owner.Center - Projectile.Center;

        if (toOwner.Length() > 2000f)
        {
            Projectile.Center = owner.Center;
            Projectile.netUpdate = true;
        }

        int minionCount = 0;
        int minionIndex = 0;

        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile p = Main.projectile[i];

            if (p.active && p.owner == Projectile.owner && p.type == Projectile.type)
            {
                if (p.whoAmI == Projectile.whoAmI) minionIndex = minionCount;
                minionCount++;
            }
        }

        if (minionCount < 1) minionCount = 1;
        float angleOffset = minionIndex * (MathHelper.TwoPi / minionCount);
        float orbitAngle = Main.GameUpdateCount * 0.04f + angleOffset;
        Vector2 orbitPoint = owner.Center + new Vector2(MathF.Cos(orbitAngle), MathF.Sin(orbitAngle)) * IdleOrbitRadius;
        Vector2 toOrbit = orbitPoint - Projectile.Center;
        float targetSpeed = MathHelper.Min(toOrbit.Length() * 0.2f, IdleMaxSpeed);
        Projectile.velocity = Vector2.Lerp(Projectile.velocity, toOrbit.SafeNormalize(Vector2.Zero) * targetSpeed, IdleSmoothing);

        if (target != null)
        {
            State = 1f;
            Timer = LungeDuration;
            Projectile.netUpdate = true;
        }
    }

    private void DoLunge(NPC target)
    {
        Timer--;
        bool targetGone = target == null || !target.active || target.life <= 0;

        if (Timer <= 0f || targetGone)
        {
            State = 2f;
            Projectile.netUpdate = true;
            return;
        }

        Vector2 toTarget = target.Center - Projectile.Center;

        if (toTarget.Length() < 20f)
        {
            State = 2f;
            Projectile.netUpdate = true;
            return;
        }

        Projectile.velocity = toTarget.SafeNormalize(Vector2.Zero) * LungeSpeed;
    }

    private void DoReturn(Player owner)
    {
        Vector2 toOwner = owner.Center - Projectile.Center;
        float dist = toOwner.Length();

        if (dist < 40f)
        {
            Projectile.velocity *= 0.7f;
            State = 0f;
            Projectile.netUpdate = true;
            return;
        }

        float speed = MathHelper.Min(dist * 0.15f, ReturnMaxSpeed);
        Projectile.velocity = Vector2.Lerp(Projectile.velocity, toOwner.SafeNormalize(Vector2.Zero) * speed, ReturnSmoothing);
    }

    private bool CheckActive(Player owner)
    {
        if (owner.dead || !owner.active)
        {
            owner.ClearBuff(ModContent.BuffType<Buffs.NeuralShardBuff>());
            return false;
        }

        if (owner.HasBuff(ModContent.BuffType<Buffs.NeuralShardBuff>())) Projectile.timeLeft = 2;
        return true;
    }

    private NPC FindTarget(Player owner)
    {
        if (owner.HasMinionAttackTargetNPC)
        {
            NPC manual = Main.npc[owner.MinionAttackTargetNPC];
            if (manual.CanBeChasedBy()) return manual;
        }

        NPC best = null;
        float closest = DetectRange;

        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            if (!npc.CanBeChasedBy()) continue;
            float dist = Projectile.Distance(npc.Center);
            if (dist >= closest) continue;
            if (!Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height)) continue;
            closest = dist;
            best = npc;
        }

        return best;
    }
}