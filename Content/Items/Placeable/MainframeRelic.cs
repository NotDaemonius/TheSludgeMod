using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Placeable;

public class MainframeRelic : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<MainframeRelicTile>());
        Item.width = 32;
        Item.height = 32;
        Item.rare = ItemRarityID.Master;
        Item.master = true;
    }
}