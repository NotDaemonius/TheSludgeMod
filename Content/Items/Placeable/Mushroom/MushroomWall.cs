using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles.Mushroom;

namespace TheSludgeMod.Content.Items.Placeable.Mushroom;

public class MushroomWall : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 400;

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableWall(ModContent.WallType<MushroomWallTile>());
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(4);
        recipe.AddIngredient(ItemID.Mushroom);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();

        Recipe recipe2 = CreateRecipe(4);
        recipe2.AddIngredient(ModContent.ItemType<MushroomBlock>());
        recipe2.AddTile(TileID.WorkBenches);
        recipe2.Register();
    }
}