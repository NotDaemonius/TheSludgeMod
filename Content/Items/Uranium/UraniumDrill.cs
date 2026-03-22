using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

public class UraniumDrill : ModItem
{
    public override void SetStaticDefaults() => ItemID.Sets.IsDrill[Type] = true;

    public override void SetDefaults()
    {
        Item.damage = 14;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.width = 20;
        Item.height = 12;
        Item.useTime = 7;
        Item.useAnimation = 14;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 1;
        Item.value = Item.buyPrice(gold: 2, silver: 16);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item23;
        Item.shoot = ModContent.ProjectileType<UraniumDrillProj>();
        Item.shootSpeed = 32f;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.pick = 140;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<UraniumBar>(18);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}