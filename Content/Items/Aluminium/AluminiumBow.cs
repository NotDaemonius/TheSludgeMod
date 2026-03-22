using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium;

public class AluminiumBow : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 0;
        Item.value = Item.buyPrice(silver: 14);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.crit = 1;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 6.6f;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AluminiumBar>(7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}