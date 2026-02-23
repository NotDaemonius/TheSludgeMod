using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Projectiles
{
    public class BouncySquareBombProjectile : ModProjectile
    {
        private const int ExplosionRadius = 4;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.aiStyle = 16;
            Projectile.CloneDefaults(ProjectileID.BouncyBomb);
            Projectile.timeLeft = 180;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity) 
        {
            if (Projectile.velocity.X != oldVelocity.X) 
            {
                Projectile.velocity.X = -oldVelocity.X * 0.95f;
            }

            if (Projectile.velocity.Y != oldVelocity.Y) 
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.95f;
            }

            return false;
        }        
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int d = 0; d < 30; d++)
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
            DustID.Smoke, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));

            for (int d = 0; d < 20; d++)
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
            DustID.Torch, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

            int centerTileX = (int)(Projectile.Center.X / 16f);
            int centerTileY = (int)(Projectile.Center.Y / 16f);

            for (int x = centerTileX - ExplosionRadius; x <= centerTileX + ExplosionRadius; x++)
            {
                for (int y = centerTileY - ExplosionRadius; y <= centerTileY + ExplosionRadius; y++)
                {
                    if (!WorldGen.InWorld(x, y, fluff: 1))
                    continue;

                    Tile tile = Main.tile[x, y];

                    if (tile == null)
                    continue;

                    if (tile.HasTile && Main.tileDungeon[tile.TileType])
                    continue;

                    if (tile.HasTile && (tile.TileType == TileID.LihzahrdBrick || tile.TileType == TileID.DemonAltar))
                    continue;

                    WorldGen.KillTile(x, y, fail: false, effectOnly: false, noItem: false);
                    WorldGen.KillWall(x, y);

                    if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendTileSquare(-1, x, y, 1);
                }
            }
        }
    }
}