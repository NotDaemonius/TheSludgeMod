using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Zinc;

[AutoloadEquip(EquipType.Head)]

public class ZincArmorHead : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 3);
        Item.rare = ItemRarityID.White;
        Item.defense = 2;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<ZincArmorBody>() && legs.type == ModContent.ItemType<ZincArmorLegs>();

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Set bonus: 2 defense";
        player.statDefense += 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ZincBar>(12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}