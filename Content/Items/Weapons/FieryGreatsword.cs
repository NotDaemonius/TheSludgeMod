using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.GlobalItems;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class FieryGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Glowmasks.AddGlowMask(this);
        }
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 96;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 400;
            Item.crit = 36;
            Item.knockBack = 8f;
            Item.value = Item.buyPrice(gold: 6);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 10 * 60);
            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), target.Center, Vector2.Zero, ProjectileID.Volcano, hit.Damage / 2, 0f, player.whoAmI);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            float angle = player.itemRotation;
            Vector2 bladeDir = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            Vector2 perpDir = new Vector2(-bladeDir.Y, bladeDir.X);
            float perpAngle = angle + MathHelper.PiOver2;
            Vector2 outwardDir = new Vector2((float)Math.Cos(perpAngle), (float)Math.Sin(perpAngle));
            Vector2 center = hitbox.Center.ToVector2();

            for (int i = 0; i < 4; i++)
            {
                float alongBlade = Main.rand.NextFloat(-Item.height / 2f, Item.height / 2f);
                float acrossBlade = Main.rand.NextFloat(-Item.width / 2f, Item.width / 2f);
                Vector2 spawnPos = center + bladeDir * alongBlade + perpDir * acrossBlade;
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Torch);
                float speed = 2.5f;
                dust.velocity = outwardDir * speed + new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.5f, 0.5f));
                dust.scale = Main.rand.NextFloat(1.4f, 1.8f);
                dust.noGravity = true;
                dust.fadeIn = 0.5f;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FieryGreatsword)
            .AddIngredient(ItemID.FireFeather)
            .AddIngredient(ItemID.HellstoneBar, 50)
            .AddIngredient(ItemID.Obsidian, 200)
            .AddTile(TileID.MythrilAnvil);
        }
    }
}
