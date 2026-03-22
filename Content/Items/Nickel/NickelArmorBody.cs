using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Nickel;

[AutoloadEquip(EquipType.Body)]

public class NickelArmorBody : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 20);
        Item.rare = ItemRarityID.White;
        Item.defense = 3;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<NickelBar>(25);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}