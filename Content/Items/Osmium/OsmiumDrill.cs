using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium;

public class OsmiumDrill : ModItem
{
    public override void SetStaticDefaults() => ItemID.Sets.IsDrill[Type] = true;
    public override void SetDefaults()
    {
        Item.damage = 19;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.width = 20;
        Item.height = 12;
        Item.useTime = 5;
        Item.useAnimation = 14;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 1;
        Item.value = Item.buyPrice(gold: 3, silver: 24);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item23;
        Item.shoot = ModContent.ProjectileType<OsmiumDrillProj>();
        Item.shootSpeed = 32f;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.pick = 170;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<OsmiumBar>(18);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}