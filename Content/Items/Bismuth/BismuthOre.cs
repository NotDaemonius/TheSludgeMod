using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth;

public class BismuthOre : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 100;

    public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.DefaultToPlaceableTile(ModContent.TileType<BismuthOreTile>());
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = Item.buyPrice(silver: 19);
		Item.rare = ItemRarityID.Orange;
		Item.autoReuse = true;
		Item.useTurn = true;
		Item.material = true;
	}
}
