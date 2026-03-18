using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Materials
{
	public class Battery : ModItem
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
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.CopperOre, 1);
            recipe.AddIngredient(ItemID.LeadOre, 1);
            recipe.AddIngredient<Plastic>(1);
            recipe.AddTile<ElectronicsTable>();
            recipe.Register();
        }
    }
}
