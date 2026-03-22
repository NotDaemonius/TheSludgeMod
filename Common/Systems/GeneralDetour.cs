using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.RoseQuartz;

namespace TheSludgeMod.Common.Systems;

public class GeneralDetour : ModSystem
{
    private static Asset<Texture2D> _roseQuartzAsset;
    private static int _giantRoseQuartzType;

    public override void Load()
    {
        _roseQuartzAsset = ModContent.Request<Texture2D>("TheSludgeMod/Content/Items/RoseQuartz/GiantRoseQuartz", AssetRequestMode.AsyncLoad);
        On_Player.Update += On_Player_Update;
        On_PlayerDrawLayers.DrawPlayer_36_CTG += On_PlayerDrawLayers_DrawPlayer_36_CTG;
    }

    public override void PostSetupContent() => _giantRoseQuartzType = ModContent.ItemType<GiantRoseQuartz>();

    private static void On_PlayerDrawLayers_DrawPlayer_36_CTG(On_PlayerDrawLayers.orig_DrawPlayer_36_CTG orig, ref PlayerDrawSet drawinfo)
    {
        if (drawinfo.shadow != 0f || drawinfo.drawPlayer.ownedLargeGems == 0) return;
        BitsByte ownedLargeGems = drawinfo.drawPlayer.ownedLargeGems;
        float gemCount = 0f;

        for (int i = 0; i < 8; i++)
        {
            if (ownedLargeGems[i]) gemCount += 1f;
        }

        if (gemCount == 0f) return;
        float orbitScale = 1f - gemCount * 0.06f;
        float orbitRadius = (gemCount - 1f) * 4f;

        switch ((int) gemCount)
        {
            case 2:
                orbitRadius += 10f;
                break;
            case 3:
                orbitRadius += 8f;
                break;
            case 4:
                orbitRadius += 6f;
                break;
            case 5:
                orbitRadius += 6f;
                break;
            case 6:
                orbitRadius += 2f;
                break;
        }

        float angleOffset = drawinfo.drawPlayer.miscCounter / 300f * MathHelper.TwoPi;
        float anglePerGem = MathHelper.TwoPi / gemCount;
        Vector2 centerPos = new Vector2((int) (drawinfo.Position.X - Main.screenPosition.X + drawinfo.drawPlayer.width * 0.5f), (int) (drawinfo.Position.Y - Main.screenPosition.Y + drawinfo.drawPlayer.height - 80f));
        Color gemColor = new Color(250, 250, 250, Main.mouseTextColor / 2);
        float gemScale = (Main.mouseTextColor / 1000f + 0.8f) * orbitScale;
        float skipped = 0f;

        for (int j = 0; j < 8; j++)
        {
            if (!ownedLargeGems[j])
            {
                skipped += 1f;
                continue;
            }

            Vector2 direction = (angleOffset + anglePerGem * (j - skipped)).ToRotationVector2();
            Texture2D texture = (j == 7) ? _roseQuartzAsset.Value : TextureAssets.Gem[j].Value;
            drawinfo.DrawDataCache.Add(new DrawData(texture, centerPos + direction * orbitRadius, sourceRect: null, color: gemColor, rotation: 0f, origin: texture.Size() / 2f, scale: gemScale, effect: SpriteEffects.None, inactiveLayerDepth: 0f));
        }
    }

    private static void On_Player_Update(On_Player.orig_Update orig, Player self, int i)
    {
        self.gem = -1;
        self.ownedLargeGems = 0;
        self.gemCount = 0;

        for (int slot = 0; slot <= 58; slot++)
        {
            ref Item item = ref self.inventory[slot];

            if (item.type == 0 || item.stack == 0)
            {
                item.TurnToAir(false);
                continue;
            }

            int type = item.type;

            if (type >= ItemID.LargeAmethyst && type <= ItemID.LargeDiamond)
            {
                self.gem = type - 1522;
                self.ownedLargeGems[self.gem] = true;
            } 
            else if (type == _giantRoseQuartzType)
            {
                self.gem = 7;
                self.ownedLargeGems[7] = true;
            } 
            else if (type == ItemID.LargeAmber)
            {
                self.gem = 6;
                self.ownedLargeGems[6] = true;
            }
        }

        orig.Invoke(self, i);
    }
}