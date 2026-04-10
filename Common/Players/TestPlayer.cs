using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.MushroomBiome;

namespace TheSludgeMod.Common.Players;

public class TestPlayer : ModPlayer
{
    public override void OnConsumeMana(Item item, int manaConsumed)
    {
        GrowMyShroom((int) (Player.Center.X / 16), (int) (Player.Center.Y / 16));
    }

    private List<List<int>> stalkGrowShit = new List<List<int>>
    {
        new List<int> { 0, 3, 4, 13, 14, 16, 17, 18, 19, 20 },
        new List<int> { 1, 5, 7, 13, 14, 15, 17, 18, 20, 21, 23, 24 },
        new List<int> { 2, 1, 5, 6, 7, 8, 9, 12, 13, 15, 16, 18, 20, 21, 22, 23, 24, 11 },
        new List<int> { 3, 2, 4, 14, 16, 17, 18, 21, 22 },
        new List<int> { 4, 6, 8, 9, 10, 11, 13, 14, 17, 18, 20, 21 },
        new List<int> { 5, 3, 4, 13, 14, 15, 16, 17, 18, 21, 22 },
        new List<int> { 6, 4, 12, 13, 16, 17, 18, 21, 22 },
        new List<int> { 7, 5, 12, 13, 15, 16, 17, 18, 21, 22, 23, 24 },
        new List<int> { 8, 2, 3, 4, 6, 7, 12, 14, 15, 16, 17, 19, 22 },
        new List<int> { 9, 5, 7, 13, 14, 15, 18, 20, 21, 22, 23, 24 },
        new List<int> { 10, 14, 16, 17, 18, 21, 22 },
        new List<int> { 11, 1, 2, 4, 6, 7, 12, 15, 16, 17, 19, 21, 22, 18 },
        new List<int> { 12, 1, 2, 5, 6, 7, 8, 9, 13, 15, 16, 17, 18, 19, 21, 22, 23, 24 },
        new List<int> { 13, 3, 4, 14, 16, 17, 18, 22 },
        new List<int> { 14, 4, 13, 17, 18, 20, 21 },
        new List<int> { 15, 4, 13, 14, 17, 18, 20, 21 },
        new List<int> { 16, 4, 13, 14, 17, 18, 20, 21 },
        new List<int> { 17, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 18, 20, 21, 22, 23, 24 },
        new List<int> { 18, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17, 19, 21, 22, 23, 24, 20 },
        new List<int> { 19, 5, 13, 15, 16, 18, 21, 22, 23, 24 },
        new List<int> { 20, 1, 2, 3, 4, 6, 7, 12, 14, 15, 16, 17, 19, 22 },
        new List<int> { 21, 3, 4, 14, 16, 17, 18, 19, 22 },
        new List<int> { 22, 4, 13, 14, 17, 18, 20, 21 },
        new List<int> { 23, 1, 2, 4, 6, 7, 12, 15, 16, 17, 19, 21, 22 },
        new List<int> { 24, 1, 2, 6, 7, 12, 15, 16, 17, 19, 21, 22 },
    };

    private List<List<int>> tippyToppy = new List<List<int>>
    {
        new List<int> { 0, 1, 2, 4, 7, 9, 12, 14, 15, 16, 17, 19, 22 },
        new List<int> { 17, 18, 24 },
        new List<int> { 17, 18 },
        new List<int> { 14, 17, 18 },
        new List<int> { 2, 8, 9, 11, 12, 17, 18, 20, 23, 24 },
    };

    private int getCostume(int previous)
    {
        if (previous == -1)
        {
            return 0;
        }

        int chosenIndex = WorldGen.genRand.Next(1, 25);
        if (stalkGrowShit[previous].Contains(chosenIndex))
        {
            return getCostume(previous);
        }

        return chosenIndex;
    }

    private int getTreeTop(int previous)
    {
        int chosenIndex = WorldGen.genRand.Next(0, 5);
        if (stalkGrowShit[chosenIndex].Contains(previous))
        {
            return getTreeTop(previous);
        }
        return chosenIndex;
    }

    public bool GrowMyShroom(int x, int y)
    {
        int stalkType = ModContent.TileType<MushroomStalk>();
        int tipType = ModContent.TileType<MushroomTip>();
        int treeHeight = WorldGen.genRand.Next(8, 15);

        // create stalk
        int previous = -1;
        for (int i = 0; i < treeHeight; i++)
        {
            int chosenIndex = getCostume(previous);

            Tile stalk = Main.tile[x, y - i];
            stalk.HasTile = true;
            stalk.TileType = (ushort) stalkType;
            stalk.TileFrameX = (short) (chosenIndex * 18);
            stalk.TileFrameY = 0;

            previous = chosenIndex;
        }

        // tip
        if (previous == 17)
        {
            return true;
        }

        int chosenTip = getTreeTop(previous);

        Tile ta = Main.tile[x, y - treeHeight];
        ta.HasTile = true;
        ta.TileType = (ushort) tipType;
        ta.TileFrameX = (short) (chosenTip * 108);
        ta.TileFrameY = 0;

        return true;
    }
}
