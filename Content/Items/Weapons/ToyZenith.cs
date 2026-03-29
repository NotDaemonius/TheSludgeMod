using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons;

public class ToyZenith : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 56;
        Item.height = 56;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 400;
        Item.crit = 14;
        Item.knockBack = 6.5f;
        Item.value = Item.buyPrice(gold: 20);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item1;
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Zenith);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}
