using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Tiles.MushroomBiome;

public class MushroomStalk : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileFrameImportant[Type] = true; // CRITICAL — without this frames aren't saved or synced
        Main.tileBlockLight[Type] = false;
        AddMapEntry(new Color(237, 160, 69));
        TileID.Sets.IsATreeTrunk[Type] = true;
        Main.tileAxe[Type] = true;
        // Tell Terraria each sprite frame is 18px tall, not the default 16px
        // This is what causes the "bottom half only" problem if missing

        // Register valid tile anchors so the tree doesn't immediately break
        TileID.Sets.IsVine[Type] = false;
    }

    // Required for correct visual height when Main.tileHeight != 16
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        width = 16;
        height = 18;
        offsetY = 2; // shift down 2px to align correctly with the tile grid
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        // Prevent vanilla framing logic from overwriting our custom frames
        return false;
    }
}
