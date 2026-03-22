using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium;

[AutoloadEquip(EquipType.Head)]

public class IridiumCrownHead : ModItem
{
    public override void SetStaticDefaults() => ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(silver: 40);
        Item.rare = ItemRarityID.White;
        Item.vanity = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<IridiumBar>(5);
        recipe.AddIngredient(ItemID.Ruby);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}