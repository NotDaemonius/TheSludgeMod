using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium;

public class OsmiumCorseque : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 47;
        Item.DamageType = DamageClass.Melee;
        Item.width = 66;
        Item.height = 66;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 6f;
        Item.value = Item.sellPrice(gold: 2, silver: 70);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<OsmiumCorsequeProj>();
        Item.shootSpeed = 4.5f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<OsmiumBar>(), 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}