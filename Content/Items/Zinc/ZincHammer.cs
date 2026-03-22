using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Zinc;

public class ZincHammer : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 6;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 21;
        Item.useAnimation = 31;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;
        Item.value = Item.buyPrice(silver: 1, copper: 60);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.hammer = 39; 
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<ZincBar>(8);
        recipe.AddRecipeGroup("Wood", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
