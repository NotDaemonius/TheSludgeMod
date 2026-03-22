using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

[AutoloadEquip(EquipType.Legs)]

public class UraniumLeggingsLegs : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 1, silver: 80);
        Item.rare = ItemRarityID.LightRed;
        Item.defense = 8;
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.08f;
        player.GetDamage(DamageClass.Generic) += 0.02f;
        player.GetCritChance(DamageClass.Generic) += 0.02f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<UraniumBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}