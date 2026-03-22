using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

[AutoloadEquip(EquipType.Head)]

public class UraniumCowlHead : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(gold: 3);
        Item.rare = ItemRarityID.LightRed;
        Item.defense = 3;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<UraniumBreastplateBody>() && legs.type == ModContent.ItemType<UraniumLeggingsLegs>();

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 40;
        player.GetDamage(DamageClass.Magic) += 0.12f;
        player.GetAttackSpeed(DamageClass.Magic) += 0.12f;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Set bonus: 15% reduced mana costs";
        player.manaCost = 0.85f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<UraniumBar>(15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}