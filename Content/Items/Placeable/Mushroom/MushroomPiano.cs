using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.Mushroom;

namespace TheSludgeMod.Content.Items.Placeable.Mushroom;

public class MushroomPiano : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<MushroomPianoTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Bone, 4);
        recipe.AddIngredient(ItemID.Mushroom, 15);
        recipe.AddIngredient(ItemID.Book);
        recipe.AddTile(TileID.Sawmill);
        recipe.Register();
    }
}