using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;
using TheSludgeMod.Content.Rarities;

namespace TheSludgeMod.Content.Items.Placeable
{
	public class ShadowvoidBrick : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShadowvoidBrick>());
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(silver: 2);
			Item.rare = ModContent.RarityType<Indigo>();
            Item.autoReuse = true;
			Item.useTurn = true;
            Item.material = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.StoneBlock, 5);
            recipe.AddIngredient<ShadowvoidOre>(1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
