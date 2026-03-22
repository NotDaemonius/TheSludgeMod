using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium;

[AutoloadEquip(EquipType.Body)]

public class AluminiumArmorBody : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 50);
        Item.rare = ItemRarityID.White;
        Item.defense = 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AluminiumBar>(25);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}