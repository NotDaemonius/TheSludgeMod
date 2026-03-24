using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.MushroomBiome;

namespace TheSludgeMod.Common.GlobalTiles;

public class SpreadMushroomGrassToMud : GlobalTile
{
    public override void RandomUpdate(int i, int j, int type)
    {
        if (type == TileID.Mud)
        {
            Tile up = Main.tile[i, j - 1];
            Tile down = Main.tile[i, j + 1];
            Tile left = Main.tile[i - 1, j];
            Tile right = Main.tile[i + 1, j];
            if (WorldGen.genRand.NextBool(3) && (up.TileType == ModContent.TileType<MushroomGrass>() || down.TileType == ModContent.TileType<MushroomGrass>() || left.TileType == ModContent.TileType<MushroomGrass>() || right.TileType == ModContent.TileType<MushroomGrass>()))
            {
                WorldGen.SpreadGrass(i, j, type, ModContent.TileType<MushroomGrass>(), false);
            }
        }
    }
}
