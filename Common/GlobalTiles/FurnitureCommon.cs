using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheSludgeMod.Common.GlobalTiles;

internal static class FurnitureCommon
{
    internal static void SetUpBathtub(this ModTile mt, int itemDropID, bool lavaImmune = false)
    {
        mt.RegisterItemDrop(itemDropID);
        Main.tileLighted[mt.Type] = true;
        Main.tileFrameImportant[mt.Type] = true;
        Main.tileLavaDeath[mt.Type] = !lavaImmune;
        Main.tileWaterDeath[mt.Type] = false;
        TileObjectData.newTile.Width = 4;
        TileObjectData.newTile.Height = 2;
        TileObjectData.newTile.CoordinateHeights = [16, 18];
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.Origin = new Point16(1, 1);
        TileObjectData.newTile.UsesCustomCanPlace = true;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 4, 0);
        TileObjectData.newTile.LavaDeath = !lavaImmune;
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
        TileObjectData.newTile.LavaPlacement = lavaImmune ? LiquidPlacement.Allowed : LiquidPlacement.NotAllowed;
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
        TileObjectData.addAlternate(1);
        TileObjectData.addTile(mt.Type);
        mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
        mt.AddMapEntry(new Color(144, 148, 144), Language.GetText("ItemName.Bathtub"));
    }

    internal static void SetUpPlatform(this ModTile mt, int itemDropID, bool lavaImmune = false)
    {
        mt.RegisterItemDrop(itemDropID);
        Main.tileLighted[mt.Type] = true;
        Main.tileFrameImportant[mt.Type] = true;
        Main.tileSolidTop[mt.Type] = true;
        Main.tileSolid[mt.Type] = true;
        Main.tileNoAttach[mt.Type] = true;
        Main.tileTable[mt.Type] = true;
        Main.tileLavaDeath[mt.Type] = !lavaImmune;
        TileID.Sets.Platforms[mt.Type] = true;
        TileID.Sets.DisableSmartCursor[mt.Type] = true;
        TileObjectData.newTile.CoordinateHeights = [16];
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleMultiplier = 27;
        TileObjectData.newTile.StyleWrapLimit = 27;
        TileObjectData.newTile.UsesCustomCanPlace = false;
        TileObjectData.newTile.LavaDeath = !lavaImmune;
        TileObjectData.newTile.LavaPlacement = lavaImmune ? LiquidPlacement.Allowed : LiquidPlacement.NotAllowed;
        TileObjectData.addTile(mt.Type);
        mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
        mt.AddMapEntry(new Color(191, 142, 111));
        mt.AdjTiles = [TileID.Platforms];
    }

    internal static void SetUpWorkBench(this ModTile mt, int itemDropID, bool lavaImmune = false)
    {
        mt.RegisterItemDrop(itemDropID);
        Main.tileSolidTop[mt.Type] = true;
        Main.tileFrameImportant[mt.Type] = true;
        Main.tileNoAttach[mt.Type] = true;
        Main.tileTable[mt.Type] = true;
        Main.tileLavaDeath[mt.Type] = !lavaImmune;
        Main.tileWaterDeath[mt.Type] = false;
        TileID.Sets.DisableSmartCursor[mt.Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
        TileObjectData.newTile.CoordinateHeights = [18];
        TileObjectData.newTile.LavaDeath = !lavaImmune;
        TileObjectData.newTile.LavaPlacement = lavaImmune ? LiquidPlacement.Allowed : LiquidPlacement.NotAllowed;
        TileObjectData.addTile(mt.Type);
        mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
        mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.WorkBench"));
        mt.AdjTiles = [TileID.WorkBenches];
    }
}
