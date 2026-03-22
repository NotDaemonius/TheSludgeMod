using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Consumables;

namespace TheSludgeMod.Content.Items.Consumables;

public class BouncySquareBomb : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 99;

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.value = Item.buyPrice(silver: 5);
        Item.rare = ItemRarityID.White;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<BouncySquareBombProjectile>();
        Item.shootSpeed = 5f;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(5);
        recipe.AddIngredient(ItemID.BouncyBomb, 3);
        recipe.AddIngredient(ItemID.StoneBlock, 10);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}