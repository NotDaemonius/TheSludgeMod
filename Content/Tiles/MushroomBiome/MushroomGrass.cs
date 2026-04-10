using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.MushroomBiome;

public class MushroomGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Grass"]);

        Main.tileMerge[Type][TileID.Grass] = true;
        Main.tileMerge[Type][TileID.Mud] = true;

        TileID.Sets.Grass[Type] = true;
        TileID.Sets.Conversion.Grass[Type] = true;

        //Grass framing (<3 terraria devs)
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.NeedsGrassFramingDirt[Type] = TileID.Mud;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        AddMapEntry(new Color(204, 127, 63));

    }

    public override void RandomUpdate(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        Tile up = Main.tile[i, j - 1];
        Tile up2 = Main.tile[i, j - 2];

        //place Astral Wild Grass
        if (WorldGen.genRand.NextBool(10) && !up.HasTile && !up2.HasTile && !(up.LiquidAmount > 0 && up2.LiquidAmount > 0) && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
        {

            up.TileType = (ushort) ModContent.TileType<RedMushroomPlants>();
            up.HasTile = true;
            up.TileFrameY = 0;

            //20 different frames, choose a random one
            up.TileFrameX = (short) (WorldGen.genRand.Next(5) * 18);
            WorldGen.SquareTileFrame(i, j - 1, true);
            if (Main.dedServ)
            {
                NetMessage.SendTileSquare(-1, i, j - 1, 3, TileChangeType.None);
            }
        }

        /*
        if (WorldGen.genRand.NextBool(10) && !up.HasTile && !up2.HasTile && !(up.LiquidAmount > 0 && up2.LiquidAmount > 0) && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
        {
            GrowMyShroom(i, j);
        }
        */
    }

    private static bool HasLava(int i, int y) =>
    Main.tile[i, y].LiquidAmount > 0 && Main.tile[i, y].LiquidType == LiquidID.Lava;

    private static readonly short[] CapFrameXValues = { 4, 64, 124 };


}
