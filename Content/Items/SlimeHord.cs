using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items
{
    public class SlimeHord : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item44;

            Item.damage = 32;
            Item.knockBack = 1f;
            Item.shoot = ModContent.ProjectileType<Projectiles.SlimeHoardProjectile>();
            Item.shootSpeed = 10f;
            Item.buffType = ModContent.BuffType<Buffs.SlimeHoardBuff>();
        }



        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            // Spawn 4 slimes, slightly spread apart so they don't stack exactly
            for (int i = 0; i < 4; i++)
            {
                Vector2 spawnPos = position + new Vector2(i * 20f - 30f, 0f);
                Projectile.NewProjectile(source, spawnPos, velocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}