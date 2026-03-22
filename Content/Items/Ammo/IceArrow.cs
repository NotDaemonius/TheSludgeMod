using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Ammo;

public class IceArrow : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 99;

    public override void SetDefaults()
    {
        Item.damage = 8; 
        Item.DamageType = DamageClass.Ranged;
        Item.width = 14;
        Item.height = 32;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 3f;
        Item.value = 5;
        Item.rare = ItemRarityID.White;
        Item.shoot = ModContent.ProjectileType<IceArrowProj>();
        Item.shootSpeed = 3.5f;
        Item.ammo = AmmoID.Arrow;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient(ItemID.WoodenArrow, 50);
        recipe.AddIngredient(ItemID.IceBlock);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}