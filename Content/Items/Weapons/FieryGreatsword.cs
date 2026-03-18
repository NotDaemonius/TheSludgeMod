using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Content.Items.Weapons
{
    public class FieryGreatsword : ModItem
    {
        public static Asset<Texture2D> GlowTexture;
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                GlowTexture = ModContent.Request<Texture2D>(Texture + "Glowmask");
            }
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
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(GlowTexture.Value, position, frame, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = GlowTexture.Value;
            spriteBatch.Draw(texture, new Vector2(Item.position.X - Main.screenPosition.X + Item.width / 2, Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height / 2), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
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
    public class FieryGreatswordGlowLayer : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            return player.HeldItem.type == ModContent.ItemType<FieryGreatsword>() && player.itemAnimation > 0 && !player.dead;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Item item = drawInfo.drawPlayer.HeldItem;
            Texture2D texture = FieryGreatsword.GlowTexture.Value;
            Vector2 position = new Vector2((int)(drawInfo.ItemLocation.X - Main.screenPosition.X), (int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y));
            Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 origin = new Vector2(drawInfo.drawPlayer.direction == -1 ? texture.Width : 0, drawInfo.drawPlayer.gravDir == -1 ? 0 : texture.Height);
            DrawData drawData = new DrawData(texture, position, frame, Color.White, drawInfo.drawPlayer.itemRotation, origin, item.scale, drawInfo.playerEffect, 0);
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}
