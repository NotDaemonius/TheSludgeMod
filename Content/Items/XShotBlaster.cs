using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items;

namespace TheSludgeMod.Content.Items
{
    public class XShotBlaster : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet; 
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.5f; 
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 offset = velocity.RotatedBy(MathHelper.PiOver2);

            offset.Normalize();

            offset *= 4f; 

            Vector2 position1 = position + offset;
            Vector2 position2 = position - offset;

            Projectile.NewProjectile(source, position1, velocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position2, velocity, type, damage, knockback, player.whoAmI);

            return false;
        }
    }
}