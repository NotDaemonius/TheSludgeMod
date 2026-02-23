using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
{
	public class Paper : ModItem
	{
		
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.White;
            Item.maxStack = 9999;
            Item.material = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
