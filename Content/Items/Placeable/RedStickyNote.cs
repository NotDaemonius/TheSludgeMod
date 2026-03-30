using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Placeable;

public class RedStickyNote : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<RedStickyNoteTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient(ModContent.ItemType<Paper>());
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();
    }
}