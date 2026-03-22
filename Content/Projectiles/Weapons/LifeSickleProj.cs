using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class LifeSickleProj : ModProjectile
    {
        public static new Asset<Texture2D> GlowTexture;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DeathSickle);
            AIType = ProjectileID.DeathSickle;
            Projectile.light = 0f;
        }

        public override bool PreDraw(ref Color lightColor) => false;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            if (owner == null || !owner.active || owner.dead) return;
            owner.Heal(Main.rand.Next(1, 3));
        }

        public override void PostDraw(Color lightColor)
        {
            GlowTexture ??= ModContent.Request<Texture2D>("TheSludgeMod/Content/Projectiles/Weapons/LifeSickleProjGlowmask");
            Texture2D texture = GlowTexture.Value;
            Vector2 origin = texture.Size() / 2f;
            Vector2 drawOffset = Projectile.Center - Main.screenPosition;
            Main.EntitySpriteDraw(texture, drawOffset, null, Color.White, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);
        }

        public override void PostAI() => Lighting.AddLight(Projectile.Center, 1.0f, 0.4f, 0.7f);

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath3);

            for (int i = 0; i < 40; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkTorch, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-3f, -1f), 0, Color.White, Main.rand.NextFloat(1.5f, 2f));
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.5f;
            }
        }
    }
}