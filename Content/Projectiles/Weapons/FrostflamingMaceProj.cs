using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles.Weapons;

public class FrostflamingMaceProj : ModProjectile
{
    private static Asset<Texture2D> _chainTex;
    private static Asset<Texture2D> _chainExtra1Tex;
    private static Asset<Texture2D> _chainExtra2Tex;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Mace);
        AIType = ProjectileID.Mace;
    }

    public override void Load()
    {
        _chainTex = ModContent.Request<Texture2D>("TheSludgeMod/Content/Projectiles/Weapons/FrostflamingMaceChain");
        _chainExtra1Tex = ModContent.Request<Texture2D>("TheSludgeMod/Content/Projectiles/Weapons/FrostflamingMaceChainExtra1");
        _chainExtra2Tex = ModContent.Request<Texture2D>("TheSludgeMod/Content/Projectiles/Weapons/FrostflamingMaceChainExtra2");
    }

    public override void Unload()
    {
        _chainTex = null;
        _chainExtra1Tex = null;
        _chainExtra2Tex = null;
    }

    public override void PostAI()
    {
        if (Main.rand.NextBool(2)) Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => target.AddBuff(BuffID.Frostburn, 180);

    public override void OnHitPlayer(Player target, Player.HurtInfo info) => target.AddBuff(BuffID.Frostburn, 180);

    public override bool PreDrawExtras() => false;

    public override bool PreDraw(ref Color lightColor)
    {
        DrawChain(lightColor);
        DrawHead(lightColor);
        return false;
    }

    private void DrawHead(Color lightColor)
    {
        Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = tex.Bounds;
        Vector2 origin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Main.EntitySpriteDraw(tex, drawPos, frame, lightColor, Projectile.rotation, origin, 1f, SpriteEffects.None);
    }

    private void DrawChain(Color lightColor)
    {
        Texture2D chainTex = _chainTex.Value;
        Texture2D chainExtra1Tex = _chainExtra1Tex.Value;
        Texture2D chainExtra2Tex = _chainExtra2Tex.Value;
        Player player = Main.player[Projectile.owner];
        Vector2 ballPos = Projectile.Center;
        Vector2 playerPos = player.MountedCenter;
        Vector2 toPlayer = playerPos - ballPos;
        float totalDist = toPlayer.Length();
        float rotation = toPlayer.ToRotation() + MathHelper.PiOver2;
        if (totalDist <= 0f) return;
        int segmentHeight = chainTex.Height;
        float traveled = segmentHeight * 0.5f;
        int index = 0;

        while (traveled < totalDist - segmentHeight * 0.5f)
        {
            Texture2D tex;
            if (index < 3) tex = chainExtra2Tex;
            else if (index < 5) tex = chainExtra1Tex;
            else tex = chainTex;
            Rectangle frame = tex.Bounds;
            Vector2 origin = frame.Size() / 2f;
            Vector2 drawPos = ballPos + Vector2.Normalize(toPlayer) * traveled - Main.screenPosition;
            Main.EntitySpriteDraw(tex, drawPos, frame, lightColor, rotation, origin, 1f, SpriteEffects.None);
            traveled += segmentHeight;
            index++;
        }
    }
}