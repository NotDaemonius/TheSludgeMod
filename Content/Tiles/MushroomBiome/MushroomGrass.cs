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

        if (WorldGen.genRand.NextBool(10) && !up.HasTile && !up2.HasTile && !(up.LiquidAmount > 0 && up2.LiquidAmount > 0) && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
        {
            GrowMyShroom(i, j);
        }
    }

    private static bool HasLava(int i, int y) =>
    Main.tile[i, y].LiquidAmount > 0 && Main.tile[i, y].LiquidType == LiquidID.Lava;

    private static readonly short[] CapFrameXValues = { 4, 64, 124 };

    public static bool GrowMyShroom(int i, int y)
    {
        int groundType = ModContent.TileType<MushroomGrass>();
        int stalkType = ModContent.TileType<MushroomStalk>();
        int capType = ModContent.TileType<MushroomTip>();

        if (HasLava(i - 1, y - 1) || HasLava(i, y - 1) || HasLava(i + 1, y - 1))
            return false;

        bool validGround = Main.tile[i, y].HasTile && !Main.tile[i, y].IsActuated
                        && Main.tile[i, y].TileType == groundType
                        && Main.tile[i - 1, y].HasTile && Main.tile[i - 1, y].TileType == groundType
                        && Main.tile[i + 1, y].HasTile && Main.tile[i + 1, y].TileType == groundType
                        && Main.tile[i, y - 1].WallType == 0;

        bool enoughSpace = WorldGen.EmptyTileCheck(i - 2, i + 2, y - 13, y - 3, groundType)
                        && WorldGen.EmptyTileCheck(i - 1, i + 1, y - 3, y - 1, groundType);

        if (!validGround || !enoughSpace)
            return false;

        if (WorldGen.gen && WorldGen.genRand.Next(3) != 0)
        {
            Tile center = Main.tile[i, y];
            center.IsHalfBlock = false;
            center.Slope = SlopeType.Solid;
        }

        if (Main.tile[i, y].IsHalfBlock || Main.tile[i, y].Slope != SlopeType.Solid)
            return false;

        int shroomHeight = WorldGen.genRand.Next(4, 11);
        int topTileY = y - shroomHeight;

        // All stalk tiles use frameY=0 (regular stalk row) when a cap is present.
        // frameY=19 (tip row) is only for stalk-only trees with no cap tile above them.
        for (int j = topTileY + 1; j < y; j++)
        {
            Tile stalk = Main.tile[i, j];
            stalk.HasTile = true;
            stalk.TileType = (ushort) stalkType;
            stalk.TileFrameX = (short) (WorldGen.genRand.Next(3) * 18); // 0, 18, or 36
            stalk.TileFrameY = 0;
        }

        // Cap tile
        Tile cap = Main.tile[i, topTileY];
        cap.HasTile = true;
        cap.TileType = (ushort) capType;
        cap.TileFrameX = CapFrameXValues[WorldGen.genRand.Next(3)];
        cap.TileFrameY = 0;

        WorldGen.RangeFrame(i - 2, topTileY - 1, i + 2, y + 1);

        if (Main.netMode == NetmodeID.Server)
            NetMessage.SendTileSquare(-1, i - 1, topTileY, 3, shroomHeight, TileChangeType.None);

        return true;
    }
}
