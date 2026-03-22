using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class Pebble : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 99;

    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.damage = 5;
        Item.knockBack = 0.5f;
        Item.DamageType = DamageClass.Ranged;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<PebbleProj>();
        Item.shootSpeed = 6f;
        Item.value = Item.buyPrice(copper: 5);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient(ItemID.StoneBlock, 1);
        recipe.Register();
    }
}