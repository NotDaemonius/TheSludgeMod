using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth;

public class BismuthForge : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.AdamantiteForge);
        Item.createTile = ModContent.TileType<BismuthForgeTile>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<BismuthOre>(), 30);
        recipe.AddIngredient(ItemID.Furnace);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}