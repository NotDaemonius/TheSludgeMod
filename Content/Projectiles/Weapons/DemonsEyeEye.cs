using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class DemonsEyeEye : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.friendly = true;
        Projectile.aiStyle = ProjAIStyleID.Arrow;
        Projectile.hostile = false;
        Projectile.timeLeft = 600;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        AIType = ProjectileID.WoodenArrowFriendly;
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath1);

        for (int i = 0; i < 15; i++)
        {
            int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-3f, -1f), 0, Color.White, Main.rand.NextFloat(0.8f, 1.4f));
            Main.dust[dustIndex].noGravity = false;
            Main.dust[dustIndex].velocity *= 1.5f;
        }

        Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f)), 1);
        Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f)), 2);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D tex = TextureAssets.Projectile[Type].Value;
        Vector2 origin = tex.Size() / 2f;

        SpriteEffects spriteEffects = SpriteEffects.None;
        if (Projectile.velocity.X > 0)
            spriteEffects = SpriteEffects.FlipHorizontally;

        Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
        return false;
    }
}
