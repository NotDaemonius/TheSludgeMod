using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium;

public class IridiumWatch : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.value = Item.buyPrice(silver: 40);
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override void UpdateInfoAccessory(Player player)
    {
        if (player.accWatch < 3) player.accWatch = 3;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<IridiumBar>(10);
        recipe.AddIngredient(ItemID.Chain, 1);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}