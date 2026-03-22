using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;
using TheSludgeMod.Content.Projectiles.Weapons;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Content.Items.Weapons;

public class CthulhuEyeStaff : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 15;
        Item.DamageType = DamageClass.Summon;
        Item.mana = 10;
        Item.width = 38;
        Item.height = 38;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.knockBack = 1f;
        Item.value = Item.buyPrice(gold: 2);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item44;
        Item.shoot = ModContent.ProjectileType<CthulhuEyeStaffProj>();
        Item.shootSpeed = 10f;
        Item.buffType = ModContent.BuffType<CthulhuEyeBuff>();
    }  

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        player.AddBuff(Item.buffType, 2);
        return true;
    }
}