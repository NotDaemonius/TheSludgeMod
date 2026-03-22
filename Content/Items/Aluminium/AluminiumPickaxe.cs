using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium;

public class AluminiumPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 7;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 10;
        Item.useAnimation = 17;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 2;
        Item.value = Item.buyPrice(silver: 20);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.pick = 54; 
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AluminiumBar>(10);
        recipe.AddRecipeGroup("Wood", 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
