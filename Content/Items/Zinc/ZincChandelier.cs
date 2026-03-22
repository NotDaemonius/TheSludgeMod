using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Zinc;

public class ZincChandelier : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Chandeliers>(), 0);
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.buyPrice(silver: 12);
        Item.maxStack = Item.CommonMaxStack;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ZincBar>(4);
        recipe.AddIngredient(ItemID.Torch, 4);
        recipe.AddIngredient(ItemID.Chain);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}