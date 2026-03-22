using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium;

public class AluminiumBullet : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 99;

    public override void SetDefaults()
    {
        Item.damage = 10; 
        Item.DamageType = DamageClass.Ranged;
        Item.width = 8;
        Item.height = 8;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 4f;
        Item.value = 30;
        Item.rare = ItemRarityID.White;
        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 4.5f;
        Item.ammo = AmmoID.Bullet;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(70);
        recipe.AddIngredient(ItemID.MusketBall, 70);
        recipe.AddIngredient(ModContent.ItemType<AluminiumBar>());
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}