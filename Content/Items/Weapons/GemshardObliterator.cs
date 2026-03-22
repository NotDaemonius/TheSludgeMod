using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common;
using TheSludgeMod.Content.NPCs.TheMainframe;

namespace TheSludgeMod.Content.Items.Weapons;

public class GemshardObliterator : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 66;
        Item.height = 24;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 6;
        Item.useAnimation = 6;
        Item.autoReuse = true;
        Item.shootSpeed = 24;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 56;
        Item.knockBack = 2;
        Item.crit = 12;
        Item.value = Item.buyPrice(0, 5, 0, 0);
        Item.rare = ItemRarityID.LightPurple;
        Item.UseSound = SoundID.Item157;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<TheMainframeLaser>();
    }

    public override Vector2? HoldoutOffset() => new Vector2(-10f, 5f);

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int projIndex = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<TheMainframeLaser>(), damage, knockback, player.whoAmI);
        Main.projectile[projIndex].friendly = true;
        Main.projectile[projIndex].hostile = false;
        Main.projectile[projIndex].DamageType = DamageClass.Ranged;
        Main.projectile[projIndex].usesLocalNPCImmunity = true;
        Main.projectile[projIndex].localNPCHitCooldown = 10;
        return false;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) => position = HelperFunctions.AdjustMuzzleOffset(player, ref position, velocity, 20f);
}

