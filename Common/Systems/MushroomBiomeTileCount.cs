using System;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.MushroomBiome;

namespace TheSludgeMod.Common.Systems;

public class MushroomBiomeTileCount : ModSystem
{
    public int GrassTileCount;
    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
        GrassTileCount = tileCounts[ModContent.TileType<MushroomGrass>()];
    }
}
