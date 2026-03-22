using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium;

public class IridiumOre : ModItem
{
	public override void SetStaticDefaults() => Item.ResearchUnlockCount = 100;

	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.DefaultToPlaceableTile(ModContent.TileType<IridiumOreTile>());
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = Item.buyPrice(silver: 6);
		Item.rare = ItemRarityID.White;
		Item.autoReuse = true;
		Item.useTurn = true;
		Item.material = true;
	}
}
