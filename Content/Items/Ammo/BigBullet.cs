using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles.Weapons;

namespace TheSludgeMod.Content.Items.Ammo;

public class BigBullet : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 99;

    public override void SetDefaults()
    {
        Item.damage = 25;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 8;
        Item.height = 8;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 1.5f;
        Item.value = 10;
        Item.rare = ItemRarityID.Green;
        Item.shoot = ModContent.ProjectileType<BigBulletProjectile>();
        Item.shootSpeed = 4.5f;
        Item.ammo = AmmoID.Bullet;
    }
}
