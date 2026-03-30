using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Placeable;

public class PurpleStickyNote : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<PurpleStickyNoteTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient(ModContent.ItemType<Paper>());
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();
    }
}