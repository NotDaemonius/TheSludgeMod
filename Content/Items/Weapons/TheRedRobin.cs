using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Weapons;

public class TheRedRobin : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62;
        Item.height = 32;
        Item.scale = 0.75f;
        Item.rare = ItemRarityID.Green;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.autoReuse = true; 
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 112;
        Item.knockBack = 5f;
        Item.noMelee = true;
        Item.UseSound = SoundID.Item14;
        Item.shoot = ProjectileID.CannonballFriendly;
        Item.shootSpeed = 5f;
        Item.crit = 96;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        player.velocity = -velocity * 2.3f;
        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }
}
