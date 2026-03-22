using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common;

namespace TheSludgeMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Neck)]

public class DestroyerScarf : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 24;
        Item.value = Item.sellPrice(gold: 5);
        Item.rare = ItemRarityID.LightPurple;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) => player.endurance += 0.30f;

    public override bool CanEquipAccessory(Player player, int slot, bool modded) => !HelperFunctions.PlayerHasAccesory(player, ItemID.WormScarf);

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WormScarf);
        recipe.AddIngredient(ItemID.HallowedBar, 10);
        recipe.AddIngredient(ItemID.SoulofMight, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}
