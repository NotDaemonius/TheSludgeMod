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
            // Basic Flags
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.TreeSapling[Type] = true;
            TileID.Sets.CommonSapling[Type] = true;

            // Slicing Math
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 }; // Adds up to 34
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2; // This accounts for the gap in 52px width AND 36px height

            // Horizontal Style Settings
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.DrawFlipHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 3;

            // Placement/Anchor Logic (Prevents instant breaking)
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
            // A random chance to slow down growth
            if (!WorldGen.genRand.NextBool(20))
            {
                return;
            }

            Tile tile = Framing.GetTileSafely(i, j); // Safely get the tile at the given coordinates
            bool growSuccess; // A bool to see if the tree growing was successful.

            // Style 0 is for the ExampleTree sapling, and style 1 is for ExamplePalmTree, so here we check frameX to call the correct method.
            // Any pixels before 54 on the tilesheet are for ExampleTree while any pixels above it are for ExamplePalmTree
            if (tile.TileFrameX < 54)
            {
                growSuccess = WorldGen.GrowTree(i, j);
            }
            else
            {
                growSuccess = WorldGen.GrowPalmTree(i, j);
            }

            // A flag to check if a player is near the sapling
            bool isPlayerNear = WorldGen.PlayerLOS(i, j);

            // If growing the tree was a success and the player is near, show growing effects
            if (growSuccess && isPlayerNear)
            {
                WorldGen.TreeGrowFXCheck(i, j);
            }
        }
    }
}
