using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class Basher : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 12;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item153;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<BasherSwing>();
            Item.shootSpeed = 8f;
            Item.crit = 50;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Hellstone, 50);
            recipe.AddIngredient(ItemID.Obsidian, 50);
            recipe.AddIngredient(ItemID.SoulofFlight, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, player.direction);
            float numberProjectiles = 10;
            float rotation = MathHelper.ToRadians(5);
            for (int i = 0; i < numberProjectiles; i++)
            {
                position += Vector2.Normalize(velocity) * 10f;
                Vector2 peturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation * i, rotation * i, i / (numberProjectiles - 1)) * player.direction);
                Projectile.NewProjectile(source, position, peturbedSpeed, 967, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}