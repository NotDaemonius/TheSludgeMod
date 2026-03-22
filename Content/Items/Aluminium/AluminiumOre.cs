using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium;

public class AluminiumOre : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 100;

    public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.DefaultToPlaceableTile(ModContent.TileType<AluminiumOreTile>());
		Item.useStyle = ItemUseStyleID.Swing;
		Item.value = Item.buyPrice(silver: 3);
		Item.rare = ItemRarityID.White;
		Item.autoReuse = true;
		Item.useTurn = true;
        Item.material = true;
    }
}
