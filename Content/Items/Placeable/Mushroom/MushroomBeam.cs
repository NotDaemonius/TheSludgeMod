using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.Mushroom;

namespace TheSludgeMod.Content.Items.Placeable.Mushroom;

public class MushroomBeam : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 400;

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<MushroomBeamTile>());
    }
}