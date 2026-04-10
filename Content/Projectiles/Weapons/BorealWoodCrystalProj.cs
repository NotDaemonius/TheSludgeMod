using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class BorealWoodCrystalProj : ModProjectile
{
    private const float HoverHeight = 36f;
    private const float DetectRange = 600f;
    private const int ShootInterval = 160;
    private const float LeafSpeed = 11f;
    private ref float ShootTimer => ref Projectile.localAI[0];

    public override void SetStaticDefaults()
    {
        Main.projPet[Projectile.type] = true;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.minion = true;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.minionSlots = 1f;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 18000;
        Projectile.ignoreWater = true;
        Projectile.netImportant = true;
        Projectile.extraUpdates = 3;
    }

    public override bool? CanCutTiles() => false;
    public override bool MinionContactDamage() => false;

    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];
        if (!CheckActive(owner)) return;
        Projectile.minionSlots = owner.maxMinions;
        float bob = (float)Math.Sin(Main.GameUpdateCount * 0.05f) * 5f;
        Projectile.Center = owner.Center - new Vector2(-4f, HoverHeight + bob);
        Projectile.velocity = Vector2.Zero;

        if (Projectile.owner == Main.myPlayer)
        {
            ShootTimer++;

            if (ShootTimer >= ShootInterval)
            {
                NPC target = FindNearestEnemy();

                if (target != null)
                {
                    ShootTimer = 0f;
                    FireLeaf(target);
                }
            }
        }
    }

    private bool CheckActive(Player owner)
    {
        if (!owner.active || owner.dead)
        {
            owner.ClearBuff(ModContent.BuffType<BorealWoodCrystalBuff>());
            Projectile.active = false;
            return false;
        }

        if (owner.HasBuff(ModContent.BuffType<BorealWoodCrystalBuff>())) Projectile.timeLeft = 2;
        return true;
    }

    private NPC FindNearestEnemy()
    {
        NPC closest = null;
        float closestDist = DetectRange;

        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            if (!npc.active || npc.friendly || npc.lifeMax <= 5 || npc.dontTakeDamage || npc.type == NPCID.TargetDummy) continue;
            float dist = Vector2.Distance(Projectile.Center, npc.Center);

            if (dist < closestDist)
            {
                closestDist = dist;
                closest = npc;
            }
        }
        return closest;
    }

    private void FireLeaf(NPC target)
    {
        Vector2 direction = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitY);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, direction * LeafSpeed, ModContent.ProjectileType<BorealWoodTreeWandProj>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
    }
}