using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheSludgeMod.Content.Tiles
{
    public class Chandeliers : ModTile
    {
        private Asset<Texture2D> flameTexture;

        public enum StyleID
        {
            Zinc,
            Aluminium,
            Iridium
        }

        public override void Load()
        {
            flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
        }

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileLighted[Type] = true;

            TileID.Sets.MultiTileSway[Type] = true;
            TileID.Sets.IsAMechanism[Type] = true;

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Origin = new Point16(1, 0);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.newTile.DrawYOffset = 0;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(235, 166, 135), Language.GetText("MapObject.Chandelier"));
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            int style = Main.tile[i, j].TileFrameY / 54;
            int itemType = style switch
            {
                0 => ModContent.ItemType<Items.Zinc.ZincChandelier>(),
                1 => ModContent.ItemType<Items.Aluminium.AluminiumChandelier>(),
                2 => ModContent.ItemType<Items.Iridium.IridiumChandelier>(),
                _ => 0
            };

            if (itemType != 0)
            {
                yield return new Item(itemType);
            }
        }

        public override void HitWire(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int topX = i - tile.TileFrameX % 54 / 18;
            int topY = j - tile.TileFrameY % 54 / 18;

            short frameAdjustment = (short)(tile.TileFrameX >= 54 ? -54 : 54);

            for (int x = topX; x < topX + 3; x++)
            {
                for (int y = topY; y < topY + 3; y++)
                {
                    Main.tile[x, y].TileFrameX += frameAdjustment;
                    Wiring.SkipWire(x, y);
                }
            }

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendTileSquare(-1, topX, topY, 3, 3);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            if (Main.tile[i, j].TileFrameX >= 54)
            {
                return;
            }

            r = 1f;
            g = 0.95f;
            b = 0.8f;
        }

        public override void EmitParticles(int i, int j, Tile tileCache, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
        {
            if (Main.rand.NextBool(40) && tileFrameX < 54)
            {
                int tileColumn = tileFrameX / 18 % 3;
                int tileRow = tileFrameY % 54 / 18;
                if (tileRow == 1 && tileColumn != 1)
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(i * 16, j * 16 + 2), 14, 6, DustID.Torch, 0f, 0f, 100);
                    if (Main.rand.NextBool(3))
                    {
                        dust.noGravity = true;
                    }

                    dust.velocity *= 0.3f;
                    dust.velocity.Y -= 1.5f;
                }
            }
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];

            if (TileObjectData.IsTopLeft(tile))
            {
                Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
            }

            return false;
        }

        public override void AdjustMultiTileVineParameters(int i, int j, ref float? overrideWindCycle, ref float windPushPowerX, ref float windPushPowerY, ref bool dontRotateTopTiles, ref float totalWindMultiplier, ref Texture2D glowTexture, ref Color glowColor)
        {
            overrideWindCycle = 1f;
            windPushPowerY = 0;
        }

        public override void GetTileFlameData(int i, int j, ref TileDrawing.TileFlameData tileFlameData)
        {
            ulong flameSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);

            tileFlameData.flameTexture = flameTexture.Value;
            tileFlameData.flameSeed = flameSeed;
            tileFlameData.flameCount = 7;
            tileFlameData.flameColor = new Color(100, 100, 100, 0);
            tileFlameData.flameRangeXMin = -10;
            tileFlameData.flameRangeXMax = 11;
            tileFlameData.flameRangeYMin = -10;
            tileFlameData.flameRangeYMax = 1;
            tileFlameData.flameRangeMultX = 0.15f;
            tileFlameData.flameRangeMultY = 0.35f;
        }
    }
}