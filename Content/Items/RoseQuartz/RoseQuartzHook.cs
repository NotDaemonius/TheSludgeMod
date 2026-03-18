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
            if (!Main.dedServ)
                chainTexture = ModContent.Request<Texture2D>("TheSludgeMod/Content/Items/RoseQuartz/RoseQuartzHookChain");
        }

        public override void Unload()
        {
            chainTexture = null;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
        }

        public override bool? CanUseGrapple(Player player)
        {
            foreach (Projectile p in Main.ActiveProjectiles)
            {
                if (p.owner == Main.myPlayer && p.type == Projectile.type)
                    p.Kill();
            }
            return true;
        }

        public override float GrappleRange() => 375f;

        public override void NumGrappleHooks(Player player, ref int numHooks) => numHooks = 1;

        public override void GrappleRetreatSpeed(Player player, ref float speed) => speed = 18f;

        public override void GrapplePullSpeed(Player player, ref float speed) => speed = 10f;

        public override bool PreDrawExtras()
        {
            Texture2D texture = chainTexture.Value;
            int chainSegmentHeight = texture.Height;
            Vector2 chainOrigin = new Vector2(texture.Width * 0.5f, chainSegmentHeight * 0.5f);

            Vector2 center = Projectile.Center;
            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 toPlayer = playerCenter - center;
            float rotation = toPlayer.ToRotation() - MathHelper.PiOver2;

            float dist;
            while ((dist = toPlayer.Length()) > 20f && !float.IsNaN(dist))
            {
                center += Vector2.Normalize(toPlayer) * chainSegmentHeight;
                toPlayer = playerCenter - center;

                Color drawColor = Lighting.GetColor((int)(center.X / 16f), (int)(center.Y / 16f));
                Main.EntitySpriteDraw(texture, center - Main.screenPosition, texture.Bounds, drawColor, rotation, chainOrigin, 1f, SpriteEffects.None, 0);
            }

            return false;
        }
    }
}