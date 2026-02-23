using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items.Materials
{
	public class Electronics : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.Green;
            Item.maxStack = 9999;
            Item.material = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.CopperOre, 1);
            recipe.AddIngredient(ItemID.GoldOre, 1);
            recipe.AddIngredient(ItemID.SandBlock, 5);
            recipe.AddIngredient<Plastic>(5);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
