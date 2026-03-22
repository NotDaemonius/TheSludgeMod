using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Zinc;

[AutoloadEquip(EquipType.Legs)]

public class ZincArmorLegs : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 4);
        Item.rare = ItemRarityID.White;
        Item.defense = 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ZincBar>(16);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}