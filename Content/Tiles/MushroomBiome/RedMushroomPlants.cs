using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.MushroomBiome;

public class RedMushroomPlants : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileCut[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.SwaysInWindBasic[Type] = true;
        TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
        TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

        //DustType = ModContent.DustType<AstralBasic>();

        HitSound = SoundID.Grass;

        AddMapEntry(new Color(127, 111, 144));

        base.SetStaticDefaults();
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Tile tileBelow = Framing.GetTileSafely(i, j + 1);
        int type = -1;

        if (tileBelow.HasTile)
        {
            type = tileBelow.TileType;
        }

        if (type == ModContent.TileType<MushroomGrass>())
        {
            return true;
        }

        WorldGen.KillTile(i, j);

        return true;
    }

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        offsetY = 2;
    }
}
