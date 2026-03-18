using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;
using TheSludgeMod.Content.Items.Placeable;

namespace TheSludgeMod.Content.Items.Osmium
{
	public class OsmiumBar : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 25;
			ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(Type, 1, ItemID.OrichalcumBar, 1);
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<OsmiumBarTile>());
			Item.width = 20;
			Item.height = 20;
			Item.value = Item.buyPrice(silver: 45);
			Item.rare = ItemRarityID.Orange;
            Item.material = true;
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<OsmiumOre>(4);
			recipe.AddTile(TileID.Furnaces);
			recipe.Register();
		}
	}
}
