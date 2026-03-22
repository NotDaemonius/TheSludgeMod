using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz;

public class PinkPhaseblade : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.BluePhaseblade);
        Item.material = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<RoseQuartz>(), 15);
        recipe.AddIngredient(ItemID.MeteoriteBar, 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
