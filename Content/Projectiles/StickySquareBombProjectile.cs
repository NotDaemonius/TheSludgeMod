using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles
{
    public class StickySquareBombProjectile : ModProjectile
    {
        private const int ExplosionRadius = 4;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.CloneDefaults(ProjectileID.Bomb);
            Projectile.aiStyle = 16;
            Projectile.timeLeft = 180;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 1f)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.tileCollide = false;
                bool isStillStuck = false;
                int minX = (int)((Projectile.position.X - 2) / 16f);
                int maxX = (int)((Projectile.position.X + Projectile.width + 2) / 16f);
                int minY = (int)((Projectile.position.Y - 2) / 16f);
                int maxY = (int)((Projectile.position.Y + Projectile.height + 2) / 16f);

                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        if (WorldGen.InWorld(x, y))
                        {
                            Tile tile = Main.tile[x, y];

                            if (tile.HasUnactuatedTile && (Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType]))
                            {
                                isStillStuck = true;
                                break;
                            }
                        }
                    }
                    if (isStillStuck) break;
                }

                if (!isStillStuck)
                {
                    Projectile.ai[1] = 0f;
                    Projectile.tileCollide = true;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 1f;
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 2;
            height = 2;
            fallThrough = true;
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            for (int d = 0; d < 30; d++)
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));

            for (int d = 0; d < 20; d++)
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

            int centerTileX = (int)(Projectile.Center.X / 16f);
            int centerTileY = (int)(Projectile.Center.Y / 16f);

            for (int x = centerTileX - ExplosionRadius; x <= centerTileX + ExplosionRadius; x++)
            {
                for (int y = centerTileY - ExplosionRadius; y <= centerTileY + ExplosionRadius; y++)
                {
                    if (!WorldGen.InWorld(x, y, 1)) continue;

                    Tile tile = Main.tile[x, y];

                    if (tile.HasTile && (Main.tileDungeon[tile.TileType] || tile.TileType == TileID.LihzahrdBrick || tile.TileType == TileID.DemonAltar))
                    continue;

                    WorldGen.KillTile(x, y, false, false, false);
                    WorldGen.KillWall(x, y);

                    if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendTileSquare(-1, x, y, 1);
                }
            }
        }
    }
}