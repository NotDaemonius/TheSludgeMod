using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace TheSludgeMod.Common.Systems;

public class MushroomBiomeGeneration : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        int idx = tasks.FindIndex(pass => pass.Name == "Jungle");
        if (idx != -1)
            tasks.Insert(idx + 1, new PassLegacy("Generating MushroomBiome", GenerateMushroomBiome));
    }

    private void GenerateMushroomBiome(GenerationProgress progress, GameConfiguration config)
    {
        int startPos = GenVars.dungeonLocation;
        for (int i = startPos; i < startPos + 100; i++)
        {
            for (int j = 50; j < 100; j++)
            {
                Tile ag = Main.tile[i, j];
                ag.TileType = TileID.AncientBlueBrick;
                ag.HasTile = true;
            }
        }
    }
}
