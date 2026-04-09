using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Items.Weapons;

public class TheBowne : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 22;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 1.5f;
        Item.value = Item.buyPrice(gold: 1);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 9.5f;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type == ProjectileID.WoodenArrowFriendly) type = ProjectileID.BoneArrowFromMerchant;
    }
}