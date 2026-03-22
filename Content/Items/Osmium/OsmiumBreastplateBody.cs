using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium;

[AutoloadEquip(EquipType.Body)]

public class OsmiumBreastplateBody : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 3, silver: 60);
        Item.rare = ItemRarityID.LightRed;
        Item.defense = 15;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.05f;
        player.GetCritChance(DamageClass.Generic) += 0.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<OsmiumBar>(25);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}