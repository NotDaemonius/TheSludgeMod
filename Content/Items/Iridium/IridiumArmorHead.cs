using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium;

[AutoloadEquip(EquipType.Head)]

public class IridiumArmorHead : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 60);
        Item.rare = ItemRarityID.White;
        Item.defense = 6;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<IridiumArmorBody>() && legs.type == ModContent.ItemType<IridiumArmorLegs>();

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Set bonus: 5 defense";
        player.statDefense += 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<IridiumBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}