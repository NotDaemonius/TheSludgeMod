using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

public class UraniumChainsaw : ModItem
{
    public override void SetStaticDefaults() => ItemID.Sets.IsChainsaw[Type] = true;

    public override void SetDefaults()
    {
        Item.damage = 28;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.width = 30;
        Item.height = 12;
        Item.useTime = 7;
        Item.useAnimation = 14;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(gold: 2, silver: 16);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item23;
        Item.shoot = ModContent.ProjectileType<UraniumChainsawProj>();
        Item.shootSpeed = 32f;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.axe = 16;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<UraniumBar>(15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}