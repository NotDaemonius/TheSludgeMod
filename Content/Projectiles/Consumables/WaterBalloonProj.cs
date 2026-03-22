using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Consumables
{
    public class WaterBalloonProj : ModProjectile
    {
        private float Hue => Projectile.ai[0];
        private static Color HueToColor(float hue, float saturation)
        {
            float h = hue * 6f;
            int sector = (int)h % 6;
            float f = h - (int)h;
            float q = 1f - saturation * f;
            float t = saturation * f;
            float p = 1f - saturation;
            const float v = 1f;

            return sector switch
            {
                0 => new Color(v, t, p),
                1 => new Color(q, v, p),
                2 => new Color(p, v, t),
                3 => new Color(p, q, v),
                4 => new Color(t, p, v),
                _ => new Color(v, p, q),
            };
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GelBalloon);
            AIType = ProjectileID.GelBalloon;
            Projectile.friendly = false;
            Projectile.damage = 0;
            Projectile.alpha = 80;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[0] = Main.rand.NextFloat(1f);
            Projectile.ai[1] = Main.rand.NextFloat(0.6f, 1f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = texture.Size() / 2f;
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color hueColor = HueToColor(Hue, Projectile.ai[1]);
            Vector4 hue4 = hueColor.ToVector4();
            Vector4 light4 = lightColor.ToVector4();
            Color finalColor = new Color(hue4 * light4) * ((255f - Projectile.alpha) / 255f);
            Main.EntitySpriteDraw(texture, drawPos, null, finalColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Splash, Projectile.position);
            SoundEngine.PlaySound(SoundID.NPCDeath63, Projectile.position);
            Color hueColor = HueToColor(Hue, Projectile.ai[1]);

            for (int i = 0; i < 20; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Water, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-6f, -1f), 0, Color.White, Main.rand.NextFloat(0.8f, 1.4f));
                Main.dust[dustIndex].noGravity = false;
                Main.dust[dustIndex].velocity *= 1.5f;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.active)
                    continue;

                if (Projectile.Center.Distance(npc.Center) <= 64f)
                    npc.AddBuff(BuffID.Wet, 300);
            }

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (!player.active || player.dead)
                    continue;

                if (Projectile.Center.Distance(player.Center) <= 64f)
                    player.AddBuff(BuffID.Wet, 300);
            }
        }
    }
}