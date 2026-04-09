using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.Mushroom;

namespace TheSludgeMod.Content.Items.Placeable.Mushroom;

public class MushroomChandelier : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<MushroomChandelierTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Mushroom, 4);
        recipe.AddIngredient(ItemID.Torch, 4);
        recipe.AddIngredient(ItemID.Chain);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}