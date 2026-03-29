using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons;

public class StarkaKitchenRules : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 64;
        Item.height = 64;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 318;
        Item.knockBack = 48f;
        Item.value = Item.buyPrice(platinum: 3);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item1;
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Pearlwood, 999);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}
