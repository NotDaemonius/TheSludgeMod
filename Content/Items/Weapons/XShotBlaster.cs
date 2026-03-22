using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons;

public class XShotBlaster : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 13;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 40;
        Item.height = 20;
        Item.useTime = 13;
        Item.useAnimation = 13;
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
        Item.crit = 2;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.NextFloat() >= 0.5f;

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 offset = velocity.RotatedBy(MathHelper.PiOver2);
        offset.Normalize();
        offset *= 4f; 
        Vector2 position1 = position + offset;
        Vector2 position2 = position - offset;
        int proj1 = Projectile.NewProjectile(source, position1, velocity, type, damage, knockback, player.whoAmI);
        int proj2 = Projectile.NewProjectile(source, position2, velocity, type, damage, knockback, player.whoAmI);
        Main.projectile[proj1].usesLocalNPCImmunity = true;
        Main.projectile[proj1].localNPCHitCooldown = -1;
        Main.projectile[proj2].usesLocalNPCImmunity = true;
        Main.projectile[proj2].localNPCHitCooldown = -1;
        return false;
    }
}