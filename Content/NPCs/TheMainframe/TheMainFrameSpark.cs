using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.NPCs.TheMainframe;

public class TheMainFrameSpark : ModDust
{
    public override string Texture => null;

    public override void OnSpawn(Dust dust)
    {
        int desiredVanillaDustTexture = DustID.FireworkFountain_Yellow;
        int frameX = desiredVanillaDustTexture * 10 % 1000;
        int frameY = desiredVanillaDustTexture * 10 / 1000 * 30 + Main.rand.Next(3) * 10;
        dust.frame = new Rectangle(frameX, frameY, 8, 8);
        dust.noGravity = false;
    }

    public override bool Update(Dust dust)
    {
        dust.scale -= 0.05f;
        dust.alpha += 5;
        dust.velocity = new Vector2(dust.velocity.X * 0.95f, dust.velocity.Y);
        if (dust.alpha >= 255) dust.active = false;
        return false;
    }
}
