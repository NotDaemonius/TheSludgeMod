using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class RoseQuartz_Bolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.AmethystBolt);
        }

        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                int dustIndex = Dust.NewDust(
                    new Vector2(Projectile.position.X, Projectile.position.Y),
                    Projectile.width,
                    Projectile.height,
                    DustID.GemDiamond,
                    Projectile.velocity.X,
                    Projectile.velocity.Y,
                    50,  
                    new Color(205, 113, 151),
                    1.2f 
                );
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.3f;
            }

            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, new Vector2(Projectile.position.X, Projectile.position.Y));
            for (int i = 0; i < 6; i++)
            {
                int dustIndex = Dust.NewDust(
                    new Vector2(Projectile.position.X, Projectile.position.Y),
                    Projectile.width,
                    Projectile.height,
                    DustID.GemDiamond,
                    Projectile.velocity.X,
                    Projectile.velocity.Y,
                    50,
                    new Color(205, 113, 151),
                    1.1f
                );

                Main.dust[dustIndex].position =
                    (Main.dust[dustIndex].position + Projectile.Center) / 2f;
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.3f;
            }
        }

    }
}
