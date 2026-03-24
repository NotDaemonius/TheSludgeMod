using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using TheSludgeMod.Common.Systems;

namespace TheSludgeMod.Content.Biomes.Mushroom;

public class MushroomBiome : ModBiome
{
    // Select all the scenery
    public override ModWaterStyle WaterStyle => ModContent.GetInstance<MushroomWaterStyle>(); // Sets a water style for when inside this biome
    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<MushroomBiomeSurfaceBackgroundStyle>();
    public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

    //public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MysteriousMystery");

    //public override int BiomeTorchItemType => ModContent.ItemType<ExampleTorch>();
    //public override int BiomeCampfireItemType => ModContent.ItemType<ExampleCampfire>();

    // Populate the Bestiary Filter
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override Color? BackgroundColor => Color.Brown;
    public override string MapBackground => BackgroundPath; // Re-uses Bestiary Background for Map Background

    // Calculate when the biome is active.
    public override bool IsBiomeActive(Player player)
    {
        // First, we will use the exampleBlockCount from our added ModSystem for our first custom condition
        bool b1 = ModContent.GetInstance<MushroomBiomeTileCount>().GrassTileCount >= 40;
        return b1;
    }

    public override void SpecialVisuals(Player player, bool isActive)
    {
        string biomeName = "TheSludgeMod:MushroomSky";

        if (SkyManager.Instance[biomeName] != null && isActive != SkyManager.Instance[biomeName].IsActive())
        {
            if (isActive)
            {
                SkyManager.Instance.Activate(biomeName);
            } else
            {
                SkyManager.Instance.Deactivate(biomeName);
            }
        }
    }

    // Declare biome priority. The default is BiomeLow so this is only necessary if it needs a higher priority.
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
}
