using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth;

public class BismuthRepeater : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 45;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 3f;
        Item.value = Item.buyPrice(gold: 4, silver: 80);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.crit = 2;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 11f;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BismuthBar>(15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}