using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class RoseQuartzSapling : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.TreeSapling[Type] = true;
            TileID.Sets.CommonSapling[Type] = true;
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.DrawFlipHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 3;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorValidTiles = new int[] { TileID.Stone };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(255, 182, 193), Language.GetText("MapObject.Sapling"));
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void RandomUpdate(int i, int j)
        {
            if (!WorldGen.genRand.NextBool(20))
            {
                return;
            }
            Tile tile = Framing.GetTileSafely(i, j);
            bool growSuccess;

            if (tile.TileFrameX < 54)
            {
                growSuccess = WorldGen.GrowTree(i, j);
            }
            else
            {
                growSuccess = WorldGen.GrowPalmTree(i, j);
            }

            bool isPlayerNear = WorldGen.PlayerLOS(i, j);

            if (growSuccess && isPlayerNear)
            {
                WorldGen.TreeGrowFXCheck(i, j);
            }
        }
    }
}
