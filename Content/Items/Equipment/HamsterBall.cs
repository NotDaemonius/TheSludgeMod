using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TheSludgeMod.Content.Items.Materials;

namespace TheSludgeMod.Content.Items.Equipment;

public class HamsterBall : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;

        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(gold: 5);

        Item.useStyle = ItemUseStyleID.Swing;

        Item.useTime = 20;
        Item.useAnimation = 20;
        
        Item.noMelee = true;

        Item.mountType = ModContent.MountType<Mounts.HamsterBallMount>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Bubble, 50);
        recipe.AddIngredient(ModContent.ItemType<Plastic>(), 50);
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}