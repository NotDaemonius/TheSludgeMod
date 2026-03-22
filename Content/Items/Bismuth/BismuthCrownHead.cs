using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth;

[AutoloadEquip(EquipType.Head)]

public class BismuthCrownHead : ModItem
{
    public override void SetStaticDefaults() => ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 6);
        Item.rare = ItemRarityID.LightRed;
        Item.defense = 5;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<BismuthBreastplateBody>() && legs.type == ModContent.ItemType<BismuthLeggingsLegs>();

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 80;
        player.GetDamage(DamageClass.Magic) += 0.18f;
        player.GetAttackSpeed(DamageClass.Magic) += 0.18f;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Set bonus: 25% reduced mana costs";
        player.manaCost = 0.75f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BismuthBar>(15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}