using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Zinc;

public class ZincPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 5;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 14;
        Item.useAnimation = 21;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 2;
        Item.value = Item.buyPrice(silver: 2);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.pick = 39; 
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ZincBar>(8);
        recipe.AddRecipeGroup("Wood", 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
