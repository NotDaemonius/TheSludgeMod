using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium;

public class UraniumBident : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 44;
        Item.DamageType = DamageClass.Melee;
        Item.width = 66;
        Item.height = 66;
        Item.useTime = 27;
        Item.useAnimation = 27;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 5f;
        Item.value = Item.sellPrice(gold: 1, silver: 80);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<UraniumBidentProj>();
        Item.shootSpeed = 4.4f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<UraniumBar>(), 15);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}