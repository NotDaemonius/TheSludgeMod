using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.NPCs.TheMainframe;

namespace TheSludgeMod.Content.Items.Weapons;

public class Laserstorm : ModItem
{
    public override void SetStaticDefaults() => Item.staff[Type] = true;

    public override void SetDefaults()
    {
        Item.damage = 41;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 2;
        Item.crit = 12;
        Item.width = 62;
        Item.height = 62;
        Item.useTime = 4;
        Item.useAnimation = 4;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 1.5f;
        Item.value = Item.buyPrice(0, 5, 0, 0);
        Item.rare = ItemRarityID.LightPurple;
        Item.autoReuse = true;
        Item.noUseGraphic = false;
        Item.shoot = ModContent.ProjectileType<TheMainframeLaser>();
        Item.shootSpeed = 24f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 mouseWorld = Main.MouseWorld;
        Vector2 spawnPos = new Vector2(mouseWorld.X + Main.rand.NextFloat(-200f, 200f), mouseWorld.Y - 900f);
        Vector2 direction = mouseWorld - spawnPos;
        direction.Normalize();
        Vector2 laserVelocity = direction * Item.shootSpeed;
        int projIndex = Projectile.NewProjectile(source, spawnPos, laserVelocity, type, damage, knockback, player.whoAmI);
        Main.projectile[projIndex].friendly = true;
        Main.projectile[projIndex].hostile = false;
        Main.projectile[projIndex].DamageType = DamageClass.Magic;
        Main.projectile[projIndex].usesLocalNPCImmunity = true;
        Main.projectile[projIndex].localNPCHitCooldown = 10;
        SoundEngine.PlaySound(SoundID.Item12, mouseWorld);
        return false;
    }
}