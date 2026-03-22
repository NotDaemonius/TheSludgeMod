using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheSludgeMod.Content.Items.Osmium;

public class OsmiumAnvilTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
        TileObjectData.newTile.CoordinateHeights = [18];
        TileObjectData.addTile(Type);
        AdjTiles = [TileID.MythrilAnvil];
        AddMapEntry(new Color(118, 126, 184), CreateMapEntryName());
        DustType = DustID.Lead;
    }

    public override void NumDust(int x, int y, bool fail, ref int num) => num = fail ? 1 : 3;
}