using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.Mushroom;

namespace TheSludgeMod.Content.Items.Placeable.Mushroom;

public class MushroomPlatform : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 200;

    public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<MushroomPlatformTile>());

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(2);
        recipe.AddIngredient(ItemID.Mushroom);
        recipe.Register();
    }
}