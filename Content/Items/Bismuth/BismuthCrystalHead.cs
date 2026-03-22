using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth;

[AutoloadEquip(EquipType.Head)]

public class BismuthCrystalHead : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 6);
        Item.rare = ItemRarityID.LightRed;
        Item.defense = 24;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<BismuthBreastplateBody>() && legs.type == ModContent.ItemType<BismuthLeggingsLegs>();

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.15f;
        player.GetDamage(DamageClass.Melee) += 0.18f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.18f;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Set bonus: 18% increased melee critical strike chance";
        player.GetCritChance(DamageClass.Melee) += 0.18f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BismuthBar>(15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}