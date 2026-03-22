using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories.Balloons;

[AutoloadEquip(EquipType.Balloon)]

public class ShinySkyBlueBalloon : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 32;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(gold: 1, silver: 50);
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.jumpBoost = true;
}