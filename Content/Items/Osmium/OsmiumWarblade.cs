using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium;

public class OsmiumWarblade : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 55;
        Item.DamageType = DamageClass.Melee;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 17;
        Item.useAnimation = 17;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7;
        Item.value = Item.buyPrice(gold: 4, silver: 14);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.crit = 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<OsmiumBar>(12);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}