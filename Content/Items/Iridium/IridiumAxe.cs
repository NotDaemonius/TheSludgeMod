using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium;

public class IridiumAxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 8;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 17;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5;
        Item.value = Item.buyPrice(silver: 32);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.axe = 13; 
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<IridiumBar>(8);
        recipe.AddRecipeGroup("Wood", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
