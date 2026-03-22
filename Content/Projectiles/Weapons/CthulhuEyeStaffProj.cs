using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class CthulhuEyeStaffProj : ModProjectile
{
    private const float DASH_SPEED = 12f;
    private const float DETECT_RANGE = 800f;
    private const int DASH_DURATION = 15;
    private const int DASH_COOLDOWN = 20;
    private const float IDLE_SPEED = 1.5f;
    private const float MAX_SPEED_MULT = 16f;
    private const float MAX_WANDER_DIST = 88f;
    private const float WANDER_TARGET_RADIUS = MAX_WANDER_DIST * 0.75f;
    private const float SOFTCAP_START = 0.75f;
    private bool _returning = false;
    private Vector2 _wanderStartVel = Vector2.Zero;
    private Vector2 _wanderTargetVel = Vector2.Zero;
    private int _wanderDuration = 1;

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 3;
        Main.projPet[Type] = true;
        ProjectileID.Sets.MinionSacrificable[Type] = true;
        ProjectileID.Sets.CultistIsResistantTo[Type] = true;
        ProjectileID.Sets.MinionTargettingFeature[Type] = true;
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
        Projectile.aiStyle = 0;
    }

    public override bool MinionContactDamage() => true;

    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];

        if (!owner.active || owner.dead)
        {
            Projectile.active = false;
            return;
        }

        if (owner.HasBuff(ModContent.BuffType<Buffs.CthulhuEyeBuff>())) Projectile.timeLeft = 2;
        if (Projectile.ai[0] > 0) Projectile.ai[0]--;
        Vector2 toOwner = owner.Center - Projectile.Center;
        float distToOwner = toOwner.Length();
        bool tooFar = distToOwner > DETECT_RANGE;
        if (tooFar) _returning = true;
        else if (distToOwner <= MAX_WANDER_DIST) _returning = false;

        if (_returning)
        {
            if (Projectile.localAI[0] > 0)
            {
                Projectile.localAI[0] = 0;
                Projectile.velocity *= 0.3f;
                Projectile.ai[0] = DASH_COOLDOWN;
                Projectile.ai[1] = -1f;
                Projectile.localAI[2] = 0;
            }

            float distT = MathHelper.Clamp((distToOwner - MAX_WANDER_DIST) / (MAX_WANDER_DIST * 3f), 0f, 1f);
            float currentSpeed = IDLE_SPEED * MathHelper.Lerp(1f, MAX_SPEED_MULT, distT * distT);
            Projectile.velocity = toOwner.SafeNormalize(Vector2.Zero) * currentSpeed;
            SnapRotation();
            TickAnimation();
            return;
        }

        if (Projectile.localAI[0] > 0)
        {
            Projectile.localAI[0]--;

            if (tooFar)
            {
                Projectile.localAI[0] = 0;
                Projectile.velocity *= 0.3f;
                Projectile.ai[0] = DASH_COOLDOWN;
                Projectile.ai[1] = -1f;
                Projectile.localAI[2] = 0;
            } 
            else if (Projectile.localAI[0] <= 0)
            {
                Projectile.velocity *= 0.3f;
                Projectile.ai[0] = DASH_COOLDOWN;
                Projectile.ai[1] = -1f;
                Projectile.localAI[2] = 0;
            }

            SnapRotation();
            TickAnimation();
            return;
        }

        if (Projectile.ai[0] <= 0 && !tooFar)
        {
            NPC target = FindTarget(owner);

            if (target != null)
            {
                Vector2 dir = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                Projectile.velocity = dir * DASH_SPEED;
                Projectile.localAI[0] = DASH_DURATION;
                Projectile.ai[1] = target.whoAmI;
                SnapRotation();
                TickAnimation();
                return;
            }
        }

        if (Projectile.ai[0] > 0 && !tooFar)
        {
            SnapRotation();
            TickAnimation();
            return;
        }

        if (distToOwner > 2000f)
        {
            Projectile.Center = owner.Center;
            Projectile.velocity = Vector2.Zero;
            Projectile.localAI[2] = 0;
            _wanderDuration = 1;
        } 

        else
        {
            float distT = MathHelper.Clamp((distToOwner - MAX_WANDER_DIST) / (MAX_WANDER_DIST * 3f), 0f, 1f);
            float currentSpeed = IDLE_SPEED * MathHelper.Lerp(1f, MAX_SPEED_MULT, distT * distT);

            if (distToOwner > MAX_WANDER_DIST)
            {
                Projectile.velocity = toOwner.SafeNormalize(Vector2.Zero) * currentSpeed;
                Projectile.localAI[2] = 0;
            } 
            else
            {
                float softcapT = MathHelper.Clamp((distToOwner / MAX_WANDER_DIST - SOFTCAP_START) / (1f - SOFTCAP_START), 0f, 1f);

                if (softcapT > 0f)
                {
                    float dot = Vector2.Dot(_wanderTargetVel.SafeNormalize(Vector2.Zero), toOwner.SafeNormalize(Vector2.Zero));
                    if (dot < 0f) Projectile.localAI[2] = 0;
                }

                if (Projectile.localAI[2] <= 0)
                {
                    float angle = Main.rand.NextFloat(MathHelper.TwoPi);
                    float radius = Main.rand.NextFloat(WANDER_TARGET_RADIUS);
                    Vector2 targetPoint = owner.Center + new Vector2((float) System.Math.Cos(angle), (float) System.Math.Sin(angle)) * radius;
                    Vector2 toTarget = targetPoint - Projectile.Center;
                    Vector2 dir = toTarget.SafeNormalize(Vector2.Zero);
                    _wanderStartVel = Projectile.velocity;
                    _wanderTargetVel = dir * currentSpeed;
                    int baseDuration = (int) (toTarget.Length() / currentSpeed);
                    _wanderDuration = (int) MathHelper.Max(8, baseDuration * Main.rand.NextFloat(0.9f, 1.2f));
                    Projectile.localAI[2] = _wanderDuration;
                } 
                else
                {
                    Projectile.localAI[2]--;
                    float t = 1f - Projectile.localAI[2] / (float) _wanderDuration;
                    float smoothstep = t * t * (3f - 2f * t);
                    Vector2 targetDir = _wanderTargetVel.SafeNormalize(Vector2.Zero);
                    Projectile.velocity = Vector2.Lerp(_wanderStartVel, targetDir * currentSpeed, smoothstep);
                }
            }
        }

        SnapRotation();
        TickAnimation();
    }

    private void SnapRotation()
    {
        if (Projectile.velocity.LengthSquared() > 0.01f) Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
    }

    private NPC FindTarget(Player owner)
    {
        if (owner.HasMinionAttackTargetNPC)
        {
            NPC manual = Main.npc[owner.MinionAttackTargetNPC];
            if (manual.CanBeChasedBy() && Vector2.Distance(Projectile.Center, manual.Center) <= DETECT_RANGE) return manual;
        }

        NPC closest = null;
        float bestDist = DETECT_RANGE * DETECT_RANGE;

        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            if (!npc.CanBeChasedBy()) continue;
            float dist = Vector2.DistanceSquared(Projectile.Center, npc.Center);

            if (dist < bestDist)
            {
                bestDist = dist;
                closest = npc;
            }
        }

        return closest;
    }

    private void TickAnimation()
    {
        if (++Projectile.frameCounter >= 5f)
        {
            Projectile.frameCounter = 0;
            Projectile.localAI[1] = (Projectile.localAI[1] + 1) % Main.projFrames[Type];
        }

        Projectile.frame = (int) Projectile.localAI[1];
    }
}