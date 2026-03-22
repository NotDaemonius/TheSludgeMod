using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium;

public class AluminiumShortsword : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 11;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.knockBack = 5;
        Item.value = Item.buyPrice(silver: 14);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = false;
        Item.crit = 1;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<AluminiumShortswordProj>();
        Item.shootSpeed = 2.1f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AluminiumBar>(6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}