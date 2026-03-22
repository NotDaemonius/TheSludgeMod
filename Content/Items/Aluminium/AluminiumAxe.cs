using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium;

public class AluminiumAxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 7;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 18;
        Item.useAnimation = 26;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5;
        Item.value = Item.buyPrice(silver: 16);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.axe = 11; 
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AluminiumBar>(8);
        recipe.AddRecipeGroup("Wood", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
