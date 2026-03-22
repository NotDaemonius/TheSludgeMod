using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

[AutoloadEquip(EquipType.Head)]

public class UraniumHeadgearHead : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 3);
        Item.rare = ItemRarityID.LightRed;
        Item.defense = 5;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<UraniumBreastplateBody>() && legs.type == ModContent.ItemType<UraniumLeggingsLegs>();

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Ranged) += 0.12f;
        player.GetCritChance(DamageClass.Ranged) += 0.12f;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Set bonus: 25% chance to save ammo";
        player.ammoCost75 = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<UraniumBar>(15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}