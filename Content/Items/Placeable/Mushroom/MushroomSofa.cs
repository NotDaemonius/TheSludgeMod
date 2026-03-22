using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.Mushroom;

namespace TheSludgeMod.Content.Items.Placeable.Mushroom;

public class MushroomSofa : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<MushroomSofaTile>());
    }
}