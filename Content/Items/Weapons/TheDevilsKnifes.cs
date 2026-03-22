using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons;

public class TheDevilsKnifes : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true;
        Item.UseSound = SoundID.DD2_GoblinBomberThrow;
        Item.noUseGraphic = true;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 12;
        Item.knockBack = 3f;
        Item.noMelee = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 15f;
        Item.crit = 6;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        const int NumProjectiles = 10;

        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(30));
            newVelocity *= 1f - Main.rand.NextFloat(0.2f);
            Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<DevilKnife>(), damage, knockback, player.whoAmI);
        }

        return false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HellstoneBar, 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
