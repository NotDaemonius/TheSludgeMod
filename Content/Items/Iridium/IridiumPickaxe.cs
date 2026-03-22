using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium;

public class IridiumPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 8;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 10;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(silver: 40);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.pick = 64; 
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; 
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<IridiumBar>(10);
        recipe.AddRecipeGroup("Wood", 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
