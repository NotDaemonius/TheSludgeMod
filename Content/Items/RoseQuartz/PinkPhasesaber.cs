using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class PinkPhasesaber : ModItem
    {
        public override void SetDefaults() => Item.CloneDefaults(ItemID.OrangePhasesaber);

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PinkPhaseblade>());
            recipe.AddIngredient(ItemID.CrystalShard, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
