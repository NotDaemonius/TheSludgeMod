using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories
{
    public class DartInABottle : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(20, 26);
            Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(silver: 50));
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetJumpState<SimpleExtraJump>().Enable();
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.DartTrap).AddIngredient(ItemID.Bottle).AddTile(TileID.TinkerersWorkbench);
        }
    }
    public class SimpleExtraJump : ExtraJump
    {
        public override Position GetDefaultPosition() => new After(BlizzardInABottle);

        public override float GetDurationMultiplier(Player player)
        {
            return 0f;
        }
        public override void UpdateHorizontalSpeeds(Player player)
        {
            player.runAcceleration *= 1.75f;
            player.maxRunSpeed *= 2f;
        }
        public override void OnStarted(Player player, ref bool playSound)
        {
            SoundEngine.PlaySound(SoundID.DoubleJump, player.Center);
            int offsetY = player.height;
            if (player.gravDir == -1f)
                offsetY = 0;

            offsetY -= 16;
            player.velocity = new Vector2(player.velocity.X, -9);
            SpawnCloudPoof(player, player.position + new Vector2(-34f, offsetY));

            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(player.position + new Vector2(-34f, offsetY), 102, 32, DustID.Smoke, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 100, Color.White, 1.8f);
                dust.velocity = dust.velocity * 0.5f - player.velocity * new Vector2(0.1f, 0.3f);
            }
        }
        private static void SpawnCloudPoof(Player player, Vector2 position)
        {
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Bottom, new Vector2(0, 10), ProjectileID.PoisonDart, 21, 3, -1);
        }
        public override void ShowVisuals(Player player)
        {
            int offsetY = player.height - 6;
            if (player.gravDir == -1f)
                offsetY = 6;

            Vector2 spawnPos = new Vector2(player.position.X, player.position.Y + offsetY);

            for (int i = 0; i < 2; i++)
            {
                SpawnBlizzardDust(player, spawnPos, 0.1f, i == 0 ? -0.07f : -0.13f);
            }

            for (int i = 0; i < 3; i++)
            {
                SpawnBlizzardDust(player, spawnPos, 0.6f, 0.8f);
            }

            for (int i = 0; i < 3; i++)
            {
                SpawnBlizzardDust(player, spawnPos, 0.6f, -0.8f);
            }
        }
        private static void SpawnBlizzardDust(Player player, Vector2 spawnPos, float dustVelocityMultiplier, float playerVelocityMultiplier)
        {
            Dust dust = Dust.NewDustDirect(spawnPos, player.width, 12, DustID.Snow, player.velocity.X * 0.3f, player.velocity.Y * 0.3f, newColor: Color.Gray);
            dust.fadeIn = 1.5f;
            dust.velocity *= dustVelocityMultiplier;
            dust.velocity += player.velocity * playerVelocityMultiplier;
            dust.noGravity = true;
            dust.noLight = true;
        }
    }
}
