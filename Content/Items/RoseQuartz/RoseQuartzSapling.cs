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

            // This tells the game engine "this is a sapling"
            //TileID.Sets.TreeSapling[Type] = true;
            TileID.Sets.SwaysInWindBasic[Type] = true; // Makes it sway in the breeze

            // Setting up the 1x2 dimensions and placement rules
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(0, 1); // Anchor at the bottom block
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);

            // Allow placement in water if your mushroom tree grows in swamps
            TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;

            // WHAT SOIL CAN IT BE PLACED ON? (Add your custom grass here if needed)
            TileObjectData.newTile.AnchorValidTiles = new int[] {
                TileID.Dirt,
                TileID.Grass,          // Normal Green Grass
            };

            // Sprite and framing data
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.DrawFlipHorizontal = true;
            TileObjectData.newTile.StyleHorizontal = true; // Use if you have multiple sapling styles
            TileObjectData.newTile.RandomStyleRange = 3; // Number of different sapling sprites

            TileObjectData.addTile(Type);

            // Give it a name on the map
            AddMapEntry(new Color(50, 50, 255), Terraria.Localization.Language.GetText("MapObject.Sapling"));
            DustType = DustID.GlowingMushroom;
        }

        // --- NATURAL GROWTH LOGIC ---
        // This makes the sapling naturally attempt to grow over time
        public override void RandomUpdate(int i, int j)
        {
            bool isPlayerNear = WorldGen.PlayerLOS(i, j);
            bool success = WorldGen.AttemptToGrowTreeFromSapling(i, j, false);

            // If it successfully grew and a player is watching, spawn some dust effects
            if (success && isPlayerNear)
            {
                WorldGen.TreeGrowFXCheck(i, j);
            }
        }

        // --- WIND SWAY LOGIC ---
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
        {
            if (i % 2 == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
        }
    }
}
