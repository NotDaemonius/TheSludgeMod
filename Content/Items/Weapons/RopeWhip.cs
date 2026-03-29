using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class RopeWhip : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToWhip(ModContent.ProjectileType<RopeWhipProjectile>(), 8, 2, 4);
        Item.reuseDelay = 14;
        Item.rare = ItemRarityID.White;
        Item.channel = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Rope, 100);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override bool MeleePrefix() => true;
}
