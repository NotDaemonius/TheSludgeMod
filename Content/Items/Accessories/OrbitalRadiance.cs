using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TheSludgeMod.Content.Items.Accessories
{
    public class OrbitalRadiance : ModItem
    {
        internal static bool SpawningOrb = false;
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 5);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            int projectileType = 731;
            int damage = 20;
            float knockback = 0f;
            float distance = 50f;
            float rotationSpeed = 0.05f;

            for (int i = 0; i < 5; i++)
            {
                float orbitAngle = (Main.GameUpdateCount * rotationSpeed) + (i * MathHelper.TwoPi / 5f);
                Vector2 spawnPos = player.Center + orbitAngle.ToRotationVector2() * distance;

                bool exists = false;
                for (int j = 0; j < Main.maxProjectiles; j++)
                {
                    Projectile p = Main.projectile[j];
                    if (p.active && p.owner == player.whoAmI && p.type == projectileType && p.ai[1] == i + 1)
                    {
                        p.Center = spawnPos;
                        p.timeLeft = 10;
                        p.soundDelay = -1;
                        p.tileCollide = false;
                        p.penetrate = -1;
                        p.usesLocalNPCImmunity = true;
                        p.localNPCHitCooldown = 60;

                        float angleToPlayer = (player.Center - p.Center).ToRotation();
                        p.rotation = angleToPlayer + MathHelper.ToRadians(90f);

                        exists = true;
                        break;
                    }
                }

                if (!exists && player.whoAmI == Main.myPlayer)
                {
                    SpawningOrb = true;
                    int proj = Projectile.NewProjectile(player.GetSource_Accessory(Item), spawnPos, Vector2.Zero, projectileType, damage, knockback, player.whoAmI);
                    SpawningOrb = false;

                    Main.projectile[proj].ai[1] = i + 1;
                    Main.projectile[proj].soundDelay = -1;
                    Main.projectile[proj].tileCollide = false;
                    Main.projectile[proj].penetrate = -1;
                    Main.projectile[proj].usesLocalNPCImmunity = true;
                    Main.projectile[proj].localNPCHitCooldown = 60;
                    Main.projectile[proj].netUpdate = true;
                }
            }
        }
    }
}