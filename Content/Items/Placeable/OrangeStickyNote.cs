using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Placeable;

public class OrangeStickyNote : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<OrangeStickyNoteTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient(ModContent.ItemType<Paper>());
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();
    }
}