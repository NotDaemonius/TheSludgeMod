using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class LeadPaint : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.damage = 18;
        Item.DamageType = DamageClass.Ranged;
        Item.knockBack = 2f;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.consumable = false;
        Item.shoot = ModContent.ProjectileType<LeadPaintProj>();
        Item.shootSpeed = 7.5f;
        Item.value = Item.buyPrice(silver: 80);
        Item.rare = ItemRarityID.Blue;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddIngredient(ItemID.WhitePaint, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
    }
}