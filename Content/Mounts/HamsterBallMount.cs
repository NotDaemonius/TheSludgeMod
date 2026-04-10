using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Mounts
{
    public class HamsterBallPlayer : ModPlayer
    {
        public float ballRotation = 0f;

        public override void PostUpdateMiscEffects()
        {
            if (Player.mount.Active && Player.mount.Type == ModContent.MountType<HamsterBallMount>()) ballRotation += Player.velocity.X * (MathHelper.TwoPi / 200f);
        }
    }

    public class HamsterBallDrawLayer : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => drawInfo.drawPlayer.mount.Active && drawInfo.drawPlayer.mount.Type == ModContent.MountType<HamsterBallMount>();
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.MountBack);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            HamsterBallPlayer modPlayer = player.GetModPlayer<HamsterBallPlayer>();
            Texture2D texture = ModContent.Request<Texture2D>("TheSludgeMod/Content/Mounts/HamsterBallMount_BackSpin").Value;
            Vector2 drawPos = player.Center - Main.screenPosition;
            drawPos.Y += player.gfxOffY;
            Vector2 origin = new(texture.Width / 2f, texture.Height / 2f);
            Color lightColor = Lighting.GetColor((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));
            float transparency = 0.5f;
            Color drawColor = lightColor * transparency;
            DrawData drawData = new(texture, drawPos, null, drawColor, modPlayer.ballRotation, origin, 1f, SpriteEffects.None, 0);
            drawInfo.DrawDataCache.Add(drawData);
        }
    }

    public class HamsterBallFrontDrawLayer : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => drawInfo.drawPlayer.mount.Active && drawInfo.drawPlayer.mount.Type == ModContent.MountType<HamsterBallMount>();
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.MountFront);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            HamsterBallPlayer modPlayer = player.GetModPlayer<HamsterBallPlayer>();
            Texture2D texture = ModContent.Request<Texture2D>("TheSludgeMod/Content/Mounts/HamsterBallMount_FrontSpin").Value;
            Vector2 drawPos = player.Center - Main.screenPosition;
            drawPos.Y += player.gfxOffY;
            Vector2 origin = new(texture.Width / 2f, texture.Height / 2f);
            Color lightColor = Lighting.GetColor((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));
            DrawData drawData = new(texture, drawPos, null, lightColor, 0f, origin, 1f, SpriteEffects.None, 0);
            drawInfo.DrawDataCache.Add(drawData);
        }
    }

    public class HamsterBallMount : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.spawnDust = DustID.BubbleBurst_Blue;
            MountData.jumpHeight = 12;
            MountData.acceleration = 0.48f;
            MountData.jumpSpeed = 7f;
            MountData.blockExtraJumps = true;
            MountData.constantJump = false;
            MountData.fallDamage = 0f;
            MountData.runSpeed = 12f;
            MountData.dashSpeed = 12f;
            MountData.flightTimeMax = 0;
            MountData.buff = ModContent.BuffType<Buffs.HamsterBallBuff>();
            MountData.totalFrames = 1;
            MountData.playerYOffsets = [0];
            MountData.xOffset = 0;
            MountData.yOffset = 0;
            MountData.playerHeadOffset = 0;
            MountData.bodyFrame = 3;
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 1;
            MountData.runningFrameDelay = 12;
            MountData.runningFrameStart = 0;
            MountData.swimSpeed = 3f;
            MountData.textureWidth = 64;
            MountData.textureHeight = 64;
        }

        public override void UpdateEffects(Player player)
        {
            player.noItems = true;
            bool noInput = !player.controlLeft && !player.controlRight;
            bool grounded = player.velocity.Y == 0f && player.oldVelocity.Y == 0f;

            if (noInput && grounded && player.velocity.X != 0f)
            {
                player.velocity.X *= 0.998f;
                if (MathF.Abs(player.velocity.X) < 0.1f) player.velocity.X = 0f;
            }
        }
    }
}