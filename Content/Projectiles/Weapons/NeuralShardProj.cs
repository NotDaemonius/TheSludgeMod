using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    /// <summary>
    /// Minion projectile for NeuralShardStaff.
    /// Replicates Blade Staff (Enchanted Dagger) movement:
    ///   - State 0 (Idle)   : slow orbital hover near player
    ///   - State 1 (Lunge)  : fast melee dash at nearest enemy, pierces all
    ///   - State 2 (Return) : accelerate back to player after lunge ends
    /// </summary>
    public class NeuralShardProj : ModProjectile
    {
        // ── Tuning ────────────────────────────────────────────────────────────
        private const float DetectRange = 700f;  // enemy detection radius (px)
        private const float LungeSpeed = 22f;   // dash speed (px/tick)
        private const float IdleOrbitRadius = 60f;   // hover radius around player
        private const float IdleMaxSpeed = 8f;    // max speed while orbiting
        private const float IdleSmoothing = 0.12f; // acceleration factor toward orbit point
        private const float ReturnMaxSpeed = 20f;   // max speed when returning post-lunge
        private const float ReturnSmoothing = 0.30f; // acceleration factor toward player
        private const int LungeDuration = 35;    // ticks before abandoning a lunge

        // ── AI slot aliases ───────────────────────────────────────────────────
        // Using refs so reading/writing Projectile.ai[] stays readable.
        private ref float State => ref Projectile.ai[0]; // 0 idle | 1 lunge | 2 return
        private ref float Timer => ref Projectile.ai[1]; // lunge countdown

        // ─────────────────────────────────────────────────────────────────────

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1; // single frame — no animation

            // Enables player right-click manual targeting for this minion.
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            // Allows the minion to be "sacrificed" when the player summons more minions
            // than their slot count allows.
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            // Stops the Lunatic Cultist from being immune to our hits.
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

            // penetrate = -1 means unlimited pierce.
            // usesLocalNPCImmunity + localNPCHitCooldown means each individual enemy
            // gets its own 10-tick immunity window rather than a shared global one,
            // so the shard can hit multiple enemies during a single lunge pass.
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;

            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 18000;
        }

        // Grass, vines, banners, etc. — minion shouldn't cut them.
        public override bool? CanCutTiles() => false;

        // Only deal contact damage while actively lunging.
        // During idle and return the shard passes through enemies harmlessly.
        public override bool MinionContactDamage() => State == 1f;

        // ─────────────────────────────────────────────────────────────────────
        // Main AI entry point
        // ─────────────────────────────────────────────────────────────────────

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner))
                return;

            NPC target = FindTarget(owner);

            switch ((int)State)
            {
                case 0: DoIdle(owner, target); break;
                case 1: DoLunge(target); break;
                case 2: DoReturn(owner); break;
            }

            // Sprite is oriented top-right (+45° / π/4 from horizontal).
            // During a lunge or return an extra 90° is added to match the attack orientation.
            if (Projectile.velocity.LengthSquared() > 0.01f)
            {
                float rotationOffset = State == 0f
                    ? -MathHelper.PiOver4                       // idle:         -45°
                    : -MathHelper.PiOver4 + MathHelper.PiOver2; // lunge/return: +45°
                Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // State 0 — Idle: slow orbital hover near player
        // ─────────────────────────────────────────────────────────────────────

        private void DoIdle(Player owner, NPC target)
        {
            Vector2 toOwner = owner.Center - Projectile.Center;

            // Hard teleport if the player somehow got very far away.
            if (toOwner.Length() > 2000f)
            {
                Projectile.Center = owner.Center;
                Projectile.netUpdate = true;
            }

            // Count how many NeuralShardProj minions this player currently has active
            // so the orbit spacing stays even regardless of summon count:
            // 2 summons = 180°, 3 = 120°, 4 = 90°, 5 = 72°, etc.
            int minionCount = 0;
            int minionIndex = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == Projectile.owner && p.type == Projectile.type)
                {
                    if (p.whoAmI == Projectile.whoAmI)
                        minionIndex = minionCount;
                    minionCount++;
                }
            }
            if (minionCount < 1) minionCount = 1;

            float angleOffset = minionIndex * (MathHelper.TwoPi / minionCount);
            float orbitAngle = Main.GameUpdateCount * 0.04f + angleOffset;

            Vector2 orbitPoint = owner.Center
                + new Vector2(MathF.Cos(orbitAngle), MathF.Sin(orbitAngle)) * IdleOrbitRadius;

            Vector2 toOrbit = orbitPoint - Projectile.Center;

            // Smoothly accelerate toward the orbit point — same feel as Blade Staff idle drift.
            float targetSpeed = MathHelper.Min(toOrbit.Length() * 0.2f, IdleMaxSpeed);
            Projectile.velocity = Vector2.Lerp(
                Projectile.velocity,
                toOrbit.SafeNormalize(Vector2.Zero) * targetSpeed,
                IdleSmoothing
            );

            // Transition to lunge if we have a valid target.
            if (target != null)
            {
                State = 1f;
                Timer = LungeDuration;
                Projectile.netUpdate = true;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // State 1 — Lunge: fast melee dash directly at target
        // ─────────────────────────────────────────────────────────────────────

        private void DoLunge(NPC target)
        {
            Timer--;

            // Abort conditions: timer expired, or target gone/dead.
            bool targetGone = target == null || !target.active || target.life <= 0;
            if (Timer <= 0f || targetGone)
            {
                State = 2f;
                Projectile.netUpdate = true;
                return;
            }

            Vector2 toTarget = target.Center - Projectile.Center;

            // If we've reached the target's hitbox, snap back.
            if (toTarget.Length() < 20f)
            {
                State = 2f;
                Projectile.netUpdate = true;
                return;
            }

            // Set velocity directly — no smoothing, full lunge speed every tick.
            Projectile.velocity = toTarget.SafeNormalize(Vector2.Zero) * LungeSpeed;
        }

        // ─────────────────────────────────────────────────────────────────────
        // State 2 — Return: accelerate back to player after lunge ends
        // ─────────────────────────────────────────────────────────────────────

        private void DoReturn(Player owner)
        {
            Vector2 toOwner = owner.Center - Projectile.Center;
            float dist = toOwner.Length();

            if (dist < 40f)
            {
                // Close enough — bleed off speed and resume idle orbit.
                Projectile.velocity *= 0.7f;
                State = 0f;
                Projectile.netUpdate = true;
                return;
            }

            // Scale return speed with distance so it starts slow and builds momentum,
            // matching the snappy feel of the Blade Staff returning.
            float speed = MathHelper.Min(dist * 0.15f, ReturnMaxSpeed);
            Projectile.velocity = Vector2.Lerp(
                Projectile.velocity,
                toOwner.SafeNormalize(Vector2.Zero) * speed,
                ReturnSmoothing
            );
        }

        // ─────────────────────────────────────────────────────────────────────
        // Helpers
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Standard minion keep-alive check. Kills the projectile when the owner
        /// dies or loses the summon buff, and refreshes timeLeft each tick otherwise.
        /// </summary>
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<Buffs.NeuralShardBuff>());
                return false;
            }

            // Keeping timeLeft at 2 each tick prevents the projectile expiring naturally.
            if (owner.HasBuff(ModContent.BuffType<Buffs.NeuralShardBuff>()))
                Projectile.timeLeft = 2;

            return true;
        }

        /// <summary>
        /// Finds the best NPC to attack. Respects the player's manual right-click
        /// minion target first, then falls back to the closest visible enemy.
        /// </summary>
        private NPC FindTarget(Player owner)
        {
            // Manual target overrides auto-targeting entirely.
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC manual = Main.npc[owner.MinionAttackTargetNPC];
                if (manual.CanBeChasedBy())
                    return manual;
            }

            NPC best = null;
            float closest = DetectRange;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.CanBeChasedBy())
                    continue;

                float dist = Projectile.Distance(npc.Center);
                if (dist >= closest)
                    continue;

                // Line-of-sight check — won't chase enemies through solid walls.
                if (!Collision.CanHitLine(
                        Projectile.position, Projectile.width, Projectile.height,
                        npc.position, npc.width, npc.height))
                    continue;

                closest = dist;
                best = npc;
            }

            return best;
        }
    }
}