using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheSludgeMod.Content.Tiles;

public class ElectronicsTable : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolidTop[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.IgnoredByNpcStepUp[Type] = true;
        DustType = DustID.Silver;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
        TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
        TileObjectData.addTile(Type);
        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(200, 200, 200), name);
    }
    public override void NumDust(int x, int y, bool fail, ref int num) => num = fail ? 1 : 3;
}