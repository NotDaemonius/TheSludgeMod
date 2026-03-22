using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium;

[AutoloadEquip(EquipType.Head)]

public class OsmiumHelmHead : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 4, silver: 50);
        Item.rare = ItemRarityID.LightRed;
        Item.defense = 7;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<OsmiumBreastplateBody>() && legs.type == ModContent.ItemType<OsmiumLeggingsLegs>();

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.12f;
        player.GetDamage(DamageClass.Ranged) += 0.15f;
        player.GetCritChance(DamageClass.Ranged) += 0.15f;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Set bonus: 25% chance to save ammo";
        player.ammoCost75 = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<OsmiumBar>(15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}