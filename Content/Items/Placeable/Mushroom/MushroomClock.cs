using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.Mushroom;

namespace TheSludgeMod.Content.Items.Placeable.Mushroom;

public class MushroomClock : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<MushroomClockTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup("IronBar", 3);
        recipe.AddIngredient(ItemID.Glass, 6);
        recipe.AddIngredient(ItemID.Mushroom, 10);
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();
    }
}