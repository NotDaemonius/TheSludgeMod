using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class GlaiveProj : ModProjectile
{
    private const int MaxChains = 4;
    private const float SearchRange = 600f;
    private const float HomingStrength = 0.8f;
    private const float MinSpeed = 30f;
    private const int TrailLength = 8;
    private const float TrailMaxAlpha = 0.4f;

    private record struct ChainState(int ChainsLeft, int TargetNPC, HashSet<int> HitNPCs);
    private static readonly Dictionary<int, ChainState> State = new();

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = TrailLength;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.WoodenBoomerang);
        Projectile.width = 64;
        Projectile.height = 64;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Vector2 origin = texture.Size() / 2f;

        for (int i = 1; i < TrailLength; i++)
        {
            Vector2 drawPos = Projectile.oldPos[i] + new Vector2(Projectile.width, Projectile.height) / 2f - Main.screenPosition;
            float progress = 1f - (float) i / TrailLength;
            float alpha = TrailMaxAlpha * progress;
            Color color = lightColor * alpha;
            Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[i], origin, Projectile.scale, SpriteEffects.None, 0);
        }

        return true;
    }
    public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
    {
        State[Projectile.whoAmI] = new ChainState(MaxChains, -1, new HashSet<int>());
    }
    public override void PostAI()
    {
        if (!State.TryGetValue(Projectile.whoAmI, out var s) || s.TargetNPC < 0)
            return;

        NPC target = Main.npc[s.TargetNPC];

        if (!target.active)
        {
            State[Projectile.whoAmI] = s with
            {
                TargetNPC = -1
            };
            return;
        }

        Vector2 toTarget = target.Center - Projectile.Center;
        float speed = MathHelper.Max(Projectile.velocity.Length(), MinSpeed);
        Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget.SafeNormalize(Vector2.Zero) * speed, HomingStrength);
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!State.TryGetValue(Projectile.whoAmI, out var s))
            return;

        s.HitNPCs.Add(target.whoAmI);

        if (s.ChainsLeft <= 0)
        {
            State[Projectile.whoAmI] = s with
            {
                TargetNPC = -1
            };
            return;
        }

        NPC next = FindNearestUnhit(target.Center, s.HitNPCs);
        int nextIndex = next?.whoAmI ?? -1;
        State[Projectile.whoAmI] = new ChainState(s.ChainsLeft - 1, nextIndex, s.HitNPCs);
    }
    private static NPC FindNearestUnhit(Vector2 from, HashSet<int> alreadyHit)
    {
        NPC closest = null;
        float closestDist = SearchRange * SearchRange;

        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (npc.friendly || npc.dontTakeDamage || npc.immortal)
                continue;

            if (alreadyHit.Contains(npc.whoAmI))
                continue;

            float dist = Vector2.DistanceSquared(from, npc.Center);

            if (dist < closestDist)
            {
                closestDist = dist;
                closest = npc;
            }
        }

        return closest;
    }
    public override void OnKill(int timeLeft)
    {
        State.Remove(Projectile.whoAmI);
    }
}