using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class SilkRopeWhip : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToWhip(ModContent.ProjectileType<SilkRopeWhipProjectile>(), 9, 2, 4);
        Item.reuseDelay = 14;
        Item.rare = ItemRarityID.White;
        Item.channel = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SilkRope, 100);
        recipe.AddTile(TileID.Loom);
    }

    public override bool MeleePrefix() => true;
}
