using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

public class UraniumOre : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 100;

    public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.DefaultToPlaceableTile(ModContent.TileType<UraniumOreTile>());
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = Item.buyPrice(silver: 11);
		Item.rare = ItemRarityID.Orange;
		Item.autoReuse = true;
		Item.useTurn = true;
        Item.material = true;
    }
}
