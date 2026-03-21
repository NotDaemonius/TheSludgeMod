using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class WoodTreeWand : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;

            // --- Damage & Class ---
            Item.damage = 7;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 1.5f;

            // --- Mana ---
            Item.mana = 2;

            // --- Use ---
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.noMelee = true;                      // Damage comes from the projectile, not the swing

            // --- Projectile ---
            Item.shoot = ModContent.ProjectileType<WoodTreeWandProj>();
            Item.shootSpeed = 8f;

            // --- Misc ---
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item8;            // Wand of Sparking uses Item8
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source,
            Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Add a small random spread on top of the projectile's own curving,
            // so bursts feel organic even before the leaf starts drifting.
            float spread = MathHelper.ToRadians(1f);   // ±6 degrees
            float angle = velocity.ToRotation() + Main.rand.NextFloat(-spread, spread);
            velocity = angle.ToRotationVector2() * velocity.Length();

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false; // We spawned it manually, so tell the engine not to spawn a second one
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 10)
                .AddIngredient(ItemID.FallenStar, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}