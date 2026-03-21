using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class ThePurifier : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 66;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(gold: 7);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 18f;
            Item.useAmmo = AmmoID.Bullet;
            Item.crit = 20;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LihzahrdBrick, 100)
                .AddIngredient(ItemID.BeetleHusk, 2)
                .AddIngredient(ItemID.GoldDust, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.66f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 offset = velocity.RotatedBy(MathHelper.PiOver2);
            offset.Normalize();
            offset *= 4f;

            Vector2 position1 = position + offset;
            Vector2 position2 = position - offset;

            int proj1 = Projectile.NewProjectile(source, position1, velocity, type, damage, knockback, player.whoAmI);
            int proj2 = Projectile.NewProjectile(source, position2, velocity, type, damage, knockback, player.whoAmI);
            int proj3 = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

            Main.projectile[proj1].usesLocalNPCImmunity = true;
            Main.projectile[proj1].localNPCHitCooldown = -1;

            Main.projectile[proj2].usesLocalNPCImmunity = true;
            Main.projectile[proj2].localNPCHitCooldown = -1;

            Main.projectile[proj3].usesLocalNPCImmunity = true;
            Main.projectile[proj3].localNPCHitCooldown = -1;

            return false;
        }
    }
}