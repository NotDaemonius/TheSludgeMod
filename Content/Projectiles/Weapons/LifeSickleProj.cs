using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons
{
    public class LifeSickleProj : ModProjectile
    {
        public static Asset<Texture2D> GlowTexture;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DeathSickle);
            AIType = ProjectileID.DeathSickle;
            Projectile.light = 0f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            if (owner == null || !owner.active || owner.dead)
                return;

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
        public override void PostAI()
        {
            Lighting.AddLight(Projectile.Center, 1.0f, 0.4f, 0.7f);
        }
    }
}