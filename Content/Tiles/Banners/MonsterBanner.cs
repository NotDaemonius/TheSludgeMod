using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TheSludgeMod.Common.GlobalTiles;
using TheSludgeMod.Content.NPCs;

using static Terraria.ModLoader.ModContent;

namespace TheSludgeMod.Content.Tiles.Banners;

public class MonsterBanner : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.MultiTileSway[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.DrawYOffset = -2;
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
        TileObjectData.newAlternate.DrawYOffset = -10;
        TileObjectData.addAlternate(0);
        TileObjectData.addTile(Type);
        DustType = -1;
        AddMapEntry(new Color(13, 88, 130), Language.GetText("MapObject.Banner"));
    }

    public override void NearbyEffects(int i, int j, bool closer)
    {
        if (closer) return;
        int style = Main.tile[i, j].TileFrameX / 18;
        int npc = GetBannerNPC(style);
        if (npc == -1) return;
        int itemType = TileLoader.GetItemDropFromTypeAndStyle(Type, style);

        if (ItemID.Sets.BannerStrength.IndexInRange(itemType) && ItemID.Sets.BannerStrength[itemType].Enabled)
        {
            Main.SceneMetrics.NPCBannerBuff[npc] = true;
            Main.SceneMetrics.hasBanner = true;
        }
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) => FurnitureCommon.DrawSwayingMultiTile(i, j);

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY) => offsetY += 2;

    public static int GetBannerNPC(int style)
    {
        int npc = -1;
        switch (style)
        {
            case 0:
                npc = NPCType<OrangeSlime>();
                break;
            default:
                break;
        }
        return npc;
    }
}