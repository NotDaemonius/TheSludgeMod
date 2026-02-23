using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;
using TheSludgeMod.Content.Items.Placeable;

namespace TheSludgeMod.Content.Items.Materials
{
	public class ShadowvoidBar : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 25;
			ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(Type, 1, ItemID.LunarBar, 1);
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShadowvoidBar>());
			Item.width = 20;
			Item.height = 20;
			Item.value = Item.buyPrice(gold: 15);
			Item.rare = ItemRarityID.Purple;
            Item.material = true;
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<ShadowvoidOre>(9);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
