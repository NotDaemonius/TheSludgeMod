using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Nickel;

public class NickelShortsword : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 9;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 11;
        Item.useAnimation = 11;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.knockBack = 5;
        Item.value = Item.buyPrice(silver: 5, copper: 60);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = false;
        Item.crit = 1;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<NickelShortswordProj>();
        Item.shootSpeed = 2.1f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<NickelBar>(6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}