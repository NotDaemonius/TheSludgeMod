using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles
{
    public class SlimeHoardProjectile : ModProjectile
    {
        Color color = Color.AliceBlue;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 6;
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
            ProjectileID.Sets.MinionSacrificable[Type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 0.25f;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!owner.active)
            {
                Projectile.active = false;
                return;
            }

            bool lefty = false;
            bool rightyy = false;
            bool walled = false;

            bool ahdfg = Projectile.ai[0] == -1f || Projectile.ai[0] == -2f;
            bool pullRight = Projectile.ai[0] == -1f;
            bool pullStop = Projectile.ai[0] == -2f;

            int buffType = ModContent.BuffType<Buffs.SlimeHoardBuff>();
            if (owner.dead)
                owner.ClearBuff(buffType);
            if (owner.HasBuff(buffType) || ahdfg)
                Projectile.timeLeft = 2;

            // figure out which way to walk toward the player
            int offset = 40 * (Projectile.minionPos + 1) * owner.direction;
            if (owner.Center.X < Projectile.Center.X - 10 + offset) lefty = true;
            else if (owner.Center.X > Projectile.Center.X + 10 + offset) rightyy = true;

            if (pullRight) { lefty = false; rightyy = true; }
            if (pullStop) { lefty = false; rightyy = false; }

            if (Projectile.ai[1] == 0f && !ahdfg)
            {
                int rushDist = 500 + 40 * Projectile.minionPos + (Projectile.localAI[0] > 0f ? 600 : 0);
                float dx = owner.Center.X - Projectile.Center.X;
                float dy = owner.Center.Y - Projectile.Center.Y;
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);

                if (dist > 2000f)
                    Projectile.Center = owner.Center;
                else if (dist > rushDist || (Math.Abs(dy) > 300f && Projectile.localAI[0] <= 0f))
                {
                    if (dy > 0f && Projectile.velocity.Y < 0f) Projectile.velocity.Y = 0f;
                    if (dy < 0f && Projectile.velocity.Y > 0f) Projectile.velocity.Y = 0f;
                    Projectile.ai[0] = 1f;
                }
            }

            // rush mode - fly back to the player
            if (Projectile.ai[0] != 0f && !ahdfg)
            {
                float accel = 0.4f;

                float dx = owner.Center.X - Projectile.Center.X - 40f * owner.direction;
                float dy = owner.Center.Y - Projectile.Center.Y;

                bool enemyClose = false;
                int enemyIdx = -1;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (!Main.npc[i].CanBeChasedBy(Projectile, false)) continue;
                    float d = Math.Abs(owner.Center.X - Main.npc[i].Center.X) + Math.Abs(owner.Center.Y - Main.npc[i].Center.Y);
                    if (d < 700f)
                    {
                        if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                            enemyIdx = i;
                        enemyClose = true;
                        break;
                    }
                }

                if (!enemyClose) dx -= 40f * Projectile.minionPos * owner.direction;
                if (enemyClose && enemyIdx >= 0) Projectile.ai[0] = 0f;

                float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                float topSpeed = Math.Max(12f, Math.Abs(owner.velocity.X) + Math.Abs(owner.velocity.Y));

                if (dist < 100 && owner.velocity.Y == 0f
                    && Projectile.position.Y + Projectile.height <= owner.position.Y + owner.height
                    && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    if (Projectile.velocity.Y < -6f) Projectile.velocity.Y = -6f;
                }

                if (dist < 60f) { dx = Projectile.velocity.X; dy = Projectile.velocity.Y; }
                else { dx *= topSpeed / dist; dy *= topSpeed / dist; }

                if (Projectile.velocity.X < dx) { Projectile.velocity.X += accel; if (Projectile.velocity.X < 0f) Projectile.velocity.X += accel * 1.5f; }
                if (Projectile.velocity.X > dx) { Projectile.velocity.X -= accel; if (Projectile.velocity.X > 0f) Projectile.velocity.X -= accel * 1.5f; }
                if (Projectile.velocity.Y < dy) { Projectile.velocity.Y += accel; if (Projectile.velocity.Y < 0f) Projectile.velocity.Y += accel * 1.5f; }
                if (Projectile.velocity.Y > dy) { Projectile.velocity.Y -= accel; if (Projectile.velocity.Y > 0f) Projectile.velocity.Y -= accel * 1.5f; }
            }

            float range = 40f * Projectile.minionPos;
            Projectile.localAI[0] = Math.Max(0f, Projectile.localAI[0] - 1f);

            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] -= 1f;
            }
            else
            {
                float targetX = Projectile.position.X, targetY = Projectile.position.Y;
                float bestDist = 100000f, fallback = bestDist;
                int targetIdx = -1;

                NPC pointed = Projectile.OwnerMinionAttackTargetNPC;
                if (pointed != null && pointed.CanBeChasedBy(Projectile, false))
                {
                    float d = Math.Abs(Projectile.Center.X - pointed.Center.X) + Math.Abs(Projectile.Center.Y - pointed.Center.Y);
                    if (d < bestDist)
                    {
                        if (d <= fallback) { fallback = d; targetX = pointed.Center.X; targetY = pointed.Center.Y; }
                        if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, pointed.position, pointed.width, pointed.height))
                        { bestDist = d; targetX = pointed.Center.X; targetY = pointed.Center.Y; targetIdx = pointed.whoAmI; }
                    }
                }

                if (targetIdx == -1)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        if (!Main.npc[i].CanBeChasedBy(Projectile, false)) continue;
                        float d = Math.Abs(Projectile.Center.X - Main.npc[i].Center.X) + Math.Abs(Projectile.Center.Y - Main.npc[i].Center.Y);
                        if (d < bestDist)
                        {
                            if (d <= fallback) { fallback = d; targetX = Main.npc[i].Center.X; targetY = Main.npc[i].Center.Y; }
                            if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                            { bestDist = d; targetX = Main.npc[i].Center.X; targetY = Main.npc[i].Center.Y; targetIdx = i; }
                        }
                    }
                }

                if (targetIdx == -1 && fallback < bestDist) bestDist = fallback;

                float alertRange = Projectile.position.Y > Main.worldSurface * 16.0 ? 150f : 300f;

                if (bestDist < alertRange + range && targetIdx == -1)
                {
                    float dx = targetX - Projectile.Center.X;
                    if (dx < -5f) { lefty = true; rightyy = false; }
                    else if (dx > 5f) { rightyy = true; lefty = false; }
                }

                if (targetIdx >= 0 && bestDist < 800f + range)
                {
                    Projectile.localAI[0] = 60f;
                    float dx = targetX - Projectile.Center.X;
                    if (dx < -10f) { lefty = true; rightyy = false; }
                    else if (dx > 10f) { rightyy = true; lefty = false; }

                    if (targetY < Projectile.Center.Y - 100f && dx > -50f && dx < 50f && Projectile.velocity.Y == 0f)
                    {
                        float yd = Math.Abs(targetY - Projectile.Center.Y);
                        if (yd < 120f) Projectile.velocity.Y = -10f;
                        else if (yd < 210f) Projectile.velocity.Y = -13f;
                        else if (yd < 270f) Projectile.velocity.Y = -15f;
                        else if (yd < 310f) Projectile.velocity.Y = -17f;
                        else if (yd < 380f) Projectile.velocity.Y = -18f;
                    }
                }
            }

            if (Projectile.ai[1] != 0f) { lefty = false; rightyy = false; }

            Projectile.rotation = 0f;
            Projectile.tileCollide = true;

            float accelRate = 0.2f;
            float topWalk = 6f;
            float ownerSpd = Math.Abs(owner.velocity.X) + Math.Abs(owner.velocity.Y);
            if (ownerSpd > topWalk) { topWalk = ownerSpd; accelRate = 0.3f; }

            if (lefty)
            {
                if (Projectile.velocity.X > -3.5f) Projectile.velocity.X -= accelRate;
                else Projectile.velocity.X -= accelRate * 0.25f;
            }
            else if (rightyy)
            {
                if (Projectile.velocity.X < 3.5f) Projectile.velocity.X += accelRate;
                else Projectile.velocity.X += accelRate * 0.25f;
            }
            else
            {
                Projectile.velocity.X *= 0.9f;
                if (Math.Abs(Projectile.velocity.X) <= accelRate) Projectile.velocity.X = 0f;
            }

            if (lefty || rightyy)
            {
                int tx = (int)(Projectile.Center.X / 16f) + (lefty ? -1 : 1) + (int)Projectile.velocity.X;
                int ty = (int)(Projectile.Center.Y / 16f);
                if (WorldGen.InWorld(tx, ty, 0) && WorldGen.SolidTile(tx, ty, false) && Main.tile[tx, ty] != null)
                    walled = true;
            }

            Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY, 1, false, 0);

            if (Projectile.velocity.Y == 0f)
            {
                if (walled)
                {
                    int bx = (int)(Projectile.Center.X / 16f);
                    int by = (int)((Projectile.position.Y + Projectile.height) / 16f);

                    if (WorldGen.SolidTileAllowBottomSlope(bx, by) || Main.tile[bx, by].IsHalfBlock || Main.tile[bx, by].Slope > 0)
                    {
                        try
                        {
                            bx = (int)(Projectile.Center.X / 16f) + (lefty ? -1 : 1) + (int)Projectile.velocity.X;
                            by = (int)(Projectile.Center.Y / 16f);
                            if (!WorldGen.SolidTile(bx, by - 1, false) && !WorldGen.SolidTile(bx, by - 2, false)) Projectile.velocity.Y = -5.1f;
                            else if (!WorldGen.SolidTile(bx, by - 2, false)) Projectile.velocity.Y = -7.1f;
                            else if (WorldGen.SolidTile(bx, by - 5, false)) Projectile.velocity.Y = -11.1f;
                            else if (WorldGen.SolidTile(bx, by - 4, false)) Projectile.velocity.Y = -10.1f;
                            else Projectile.velocity.Y = -9.1f;
                        }
                        catch { Projectile.velocity.Y = -9.1f; }
                    }
                }
                else if (lefty || rightyy)
                    Projectile.velocity.Y -= 6f;
            }

            Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -topWalk, topWalk);

            if (Projectile.velocity.X < 0f) Projectile.direction = -1;
            if (Projectile.velocity.X > 0f) Projectile.direction = 1;
            if (Projectile.velocity.X > accelRate && rightyy) Projectile.direction = 1;
            if (Projectile.velocity.X < -accelRate && lefty) Projectile.direction = -1;
            Projectile.spriteDirection = Projectile.direction == 1 ? -1 : 1;

            bool still = Projectile.position.X == Projectile.oldPosition.X;

            if (Projectile.ai[0] != 0f && !ahdfg)
            {
                if (++Projectile.frameCounter > 4) { Projectile.frameCounter = 0; Projectile.frame++; }
                if (Projectile.frame < 2 || Projectile.frame > 5) Projectile.frame = 2;
            }
            else if (Projectile.velocity.Y != 0f)
            {
                Projectile.frame = 1;
                Projectile.frameCounter = 0;
            }
            else
            {
                Projectile.frame = 0;
                Projectile.frameCounter = 0;
                if (still) Projectile.spriteDirection = owner.Center.X < Projectile.Center.X ? 1 : -1;
            }

            if (Projectile.wet && owner.position.Y + owner.height < Projectile.position.Y + Projectile.height && Projectile.localAI[0] == 0f)
            {
                if (Projectile.velocity.Y > -4f) Projectile.velocity.Y -= 0.2f;
                if (Projectile.velocity.Y > 0f) Projectile.velocity.Y *= 0.95f;
            }
            else
                Projectile.velocity.Y += 0.4f;

            Projectile.velocity.Y = Math.Min(Projectile.velocity.Y, 10f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity) => false;

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            int fh = tex.Height / Main.projFrames[Type];
            Rectangle src = new Rectangle(0, Projectile.frame * fh, tex.Width, fh);

            if (color == Color.AliceBlue)
                color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f) * Projectile.Opacity;

            SpriteEffects flip = Projectile.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(-1f, -5f), src, color, Projectile.rotation, new Vector2(src.Width * 0.5f, src.Height * 0.5f), Projectile.scale, flip, 0);

            return false;
        }
    }
}