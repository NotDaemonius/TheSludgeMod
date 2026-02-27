using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium
{
    public class OsmiumAnvil : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MythrilAnvil);
            Item.createTile = ModContent.TileType<OsmiumAnvilTile>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<OsmiumBar>(), 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}