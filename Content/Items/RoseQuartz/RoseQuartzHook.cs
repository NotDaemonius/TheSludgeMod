using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class RoseQuartzHook : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.useStyle = ItemUseStyleID.None;
            Item.useTime = 0;
            Item.useAnimation = 0;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<RoseQuartzHookProjectile>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<RoseQuartz>(), 15)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
    public class RoseQuartzHookProjectile : ModProjectile
    {
        private static Asset<Texture2D> chainTexture;
        public override void Load()
        {
            chainTexture = ModContent.Request<Texture2D>("TheSludgeMod/Content/Items/RoseQuartz/RoseQuartzHookChain");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
        }
        public override bool? CanUseGrapple(Player player)
        {
            int hooksOut = 0;

            foreach (var projectile in Main.ActiveProjectiles)
            {
                if (projectile.owner == Main.myPlayer && projectile.type == Projectile.type)
                {
                    hooksOut++;
                }
            }

            return hooksOut <= 2;
        }
        public override float GrappleRange()
        {
            return 375f;
        }
        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 1;
        }
        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            speed = 18f;
        }
        public override void GrapplePullSpeed(Player player, ref float speed)
        {
            speed = 10;
        }
        public override bool PreDrawExtras()
        {
            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 directionToPlayer = playerCenter - Projectile.Center;
            float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
            float distanceToPlayer = directionToPlayer.Length();

            while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
            {
                directionToPlayer /= distanceToPlayer;
                directionToPlayer *= chainTexture.Height();
                center += directionToPlayer;
                directionToPlayer = playerCenter - center;
                distanceToPlayer = directionToPlayer.Length();
                Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));
                Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition, chainTexture.Value.Bounds, drawColor, chainRotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            }

            return false;
        }
    }
}