using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth;

public class BismuthBar : ModItem
{
	public override void SetStaticDefaults() 
	{
		Item.ResearchUnlockCount = 25;
		ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(Type, 1, ItemID.TitaniumBar, 1);
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<BismuthBarTile>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.buyPrice(silver: 57);
		Item.rare = ItemRarityID.Orange;
		Item.material = true;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient<BismuthOre>(4);
		recipe.AddTile(TileID.AdamantiteForge);
		recipe.Register();
	}
}
