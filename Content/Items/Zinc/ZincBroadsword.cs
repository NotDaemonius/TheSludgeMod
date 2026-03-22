using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Zinc;

public class ZincBroadsword : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.DamageType = DamageClass.Melee;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;
        Item.value = Item.buyPrice(silver: 1, copper: 80);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.crit = 1;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ZincBar>(6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}