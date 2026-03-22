using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class WebRopeWhip : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToWhip(ModContent.ProjectileType<WebRopeWhipProjectile>(), 8, 2, 4);
        Item.reuseDelay = 14;
        Item.rare = ItemRarityID.White;
        Item.channel = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WebRope, 100);
        recipe.AddTile(TileID.WorkBenches);
    }

    public override bool MeleePrefix() => true;
}
