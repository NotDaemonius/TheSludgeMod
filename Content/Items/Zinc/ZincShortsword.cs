using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Zinc;

public class ZincShortsword : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 7;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.knockBack = 5;
        Item.value = Item.buyPrice(silver: 1, copper: 40);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = false;
        Item.crit = 1;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<ZincShortswordProj>();
        Item.shootSpeed = 2.1f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ZincBar>(5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}