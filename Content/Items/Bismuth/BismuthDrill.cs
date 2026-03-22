using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth;

public class BismuthDrill : ModItem
{
    public override void SetStaticDefaults() => ItemID.Sets.IsDrill[Type] = true;

    public override void SetDefaults()
    {
        Item.damage = 30;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.width = 20;
        Item.height = 12;
        Item.useTime = 4;
        Item.useAnimation = 14;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 1;
        Item.value = Item.buyPrice(gold: 4, silver: 32);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item23;
        Item.shoot = ModContent.ProjectileType<BismuthDrillProj>();
        Item.shootSpeed = 32f;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.pick = 195;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<BismuthBar>(18);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}