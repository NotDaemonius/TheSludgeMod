using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheSludgeMod.Content.Items.RoseQuartz;

public class RoseQuartzSapling : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.SwaysInWindBasic[Type] = true;
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 2;
        TileObjectData.newTile.Origin = new Point16(0, 1);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
        TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
        TileObjectData.newTile.AnchorValidTiles = [TileID.Dirt, TileID.Grass];
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.DrawFlipHorizontal = true;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.RandomStyleRange = 3;
        TileObjectData.addTile(Type);
        AddMapEntry(new Color(50, 50, 255), Terraria.Localization.Language.GetText("MapObject.Sapling"));
        DustType = DustID.GlowingMushroom;
    }

    public override void RandomUpdate(int i, int j)
    {
        bool isPlayerNear = WorldGen.PlayerLOS(i, j);
        bool success = WorldGen.AttemptToGrowTreeFromSapling(i, j, false);
        if (success && isPlayerNear) WorldGen.TreeGrowFXCheck(i, j);
    }

    public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
    {
        if (i % 2 == 1) effects = SpriteEffects.FlipHorizontally;
    }
}
