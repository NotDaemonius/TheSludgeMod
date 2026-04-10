using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.MushroomBiome;

public class MushroomTip : ModTile
{

    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileBlockLight[Type] = false;
        AddMapEntry(new Color(237, 160, 69));
        Main.tileAxe[Type] = true;
    }

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        // Draw width matches whichever cap variant is selected
        width = 108;
        height = 78;
        offsetY = -62; // lift the cap up above the stalk

    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        return false;
    }
}
