using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;
using TheSludgeMod.Content.Items.Placeable;

namespace TheSludgeMod.Content.Items.Iridium
{
	public class IridiumBar : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 25;
			ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(Type, 1, ItemID.PlatinumBar, 1);
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<IridiumBarTile>());
			Item.width = 20;
			Item.height = 20;
			Item.value = Item.buyPrice(silver: 24);
			Item.rare = ItemRarityID.White;
            Item.material = true;
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<IridiumOre>(4);
			recipe.AddTile(TileID.Furnaces);
			recipe.Register();
		}
	}
}
