using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class CoolSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 47;
            Item.DamageType = DamageClass.Melee;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 8;
            Item.value = Item.buyPrice(silver: 80);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item152;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<CoolSwordSwingProj>();
            Item.shootSpeed = 12f;
            Item.crit = 44;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, velocity,
            ModContent.ProjectileType<CoolSwordSwingProj>(), damage, knockback, player.whoAmI);

            float numberProjectiles = 2 + Main.rand.Next(3);
            float rotation = MathHelper.ToRadians(10);
            Vector2 spawnPos = position + Vector2.Normalize(velocity) * 10f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(
                    MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(source, spawnPos, perturbedSpeed,
                    ModContent.ProjectileType<CoolSwordProj>(), damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}