using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class AshWoodCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;

            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 50);

            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 36;
            Item.useAnimation = 36;

            Item.DamageType = DamageClass.Summon;
            Item.damage = 19;
            Item.knockBack = 3f;

            Item.shoot = ModContent.ProjectileType<AshWoodCrystalProj>();
            Item.shootSpeed = 1f;

            Item.buffType = ModContent.BuffType<AshWoodCrystalBuff>();

            Item.UseSound = SoundID.Item44;

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            foreach (Projectile p in Main.projectile)
            {
                if (p.active && p.owner == player.whoAmI && p.type == type) p.Kill();
            }

            player.AddBuff(Item.buffType, 2);
            Projectile.NewProjectile(source, player.Center - new Vector2(0f, 80f), Vector2.Zero, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AshWood, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}