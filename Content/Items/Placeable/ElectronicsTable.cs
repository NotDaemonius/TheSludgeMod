using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Aluminium;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items.Placeable
{
	public class ElectronicsTable : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ElectronicsTable>());
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(silver: 50);
			Item.rare = ItemRarityID.LightRed;
			Item.autoReuse = true;
			Item.useTurn = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AluminiumBar>(), 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
