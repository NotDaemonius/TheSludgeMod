using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium;

[AutoloadEquip(EquipType.Legs)]

public class AluminiumArmorLegs : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 40);
        Item.rare = ItemRarityID.White;
        Item.defense = 4;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AluminiumBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}