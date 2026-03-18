using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Nickel
{
    public class NickelAnvil : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IronAnvil);
            Item.createTile = ModContent.TileType<NickelAnvilTile>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<NickelBar>(), 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}