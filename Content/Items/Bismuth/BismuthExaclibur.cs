using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth;

public class BismuthExaclibur : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 65;
        Item.DamageType = DamageClass.Melee;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 8;
        Item.value = Item.buyPrice(gold: 5, silver: 52);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.crit = 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BismuthBar>(12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}