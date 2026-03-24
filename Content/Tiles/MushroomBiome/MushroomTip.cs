using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.MushroomBiome;

public class MushroomTip : ModTile
{
    private static readonly int[] CapWidths = { 52, 56, 60 };

    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileBlockLight[Type] = false;
    }

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        // Draw width matches whichever cap variant is selected
        width = tileFrameX switch
        {
            4 => CapWidths[0], // 52px
            64 => CapWidths[1], // 56px
            124 => CapWidths[2], // 60px
            _ => CapWidths[0]
        };
        height = 44;
        offsetY = -28; // lift the cap up above the stalk
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        return false;
    }
}
