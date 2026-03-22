using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz;

public class RoseQuartzStoneBlock : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 100;

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.EmeraldStoneBlock);
        Item.createTile = ModContent.TileType<RoseQuartzStoneTile>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<RoseQuartz>(), 1);
        recipe.AddIngredient(ItemID.StoneBlock, 1);
        recipe.AddTile(TileID.HeavyWorkBench);
        recipe.Register();;
    }
}
