using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class ElderStaffProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Type] = false;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 7;
            Projectile.timeLeft = 480;
            Projectile.light = 0.6f;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Terra, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, Alpha: 0, default, Scale: Main.rand.NextFloat(1.2f, 1.8f));
                Main.dust[dust].noGravity = true;
                Main.dust[dust].color = new Color(57, 255, 20);
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ElderInsanityDebuff>(), 8 * 60);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 18; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Terra, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), Alpha: 0, default, Scale: Main.rand.NextFloat(1.5f, 2.4f));
                Main.dust[dust].noGravity = true;
                Main.dust[dust].color = new Color(57, 255, 20);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Vector2 origin = tex.Size() / 2f;
            Color drawColor = new Color(57, 255, 20) * 0.9f;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);

            return false;
        }
    }
}