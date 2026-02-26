using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Tiles.Unused
{
    public class SneakersOToole : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            // This is the key line — it registers the tree with this tile.
        }

        // RandomUpdate fires periodically on loaded tiles.
        // This is what actually attempts to grow the tree in-world.
        public override void RandomUpdate(int i, int j)
        {
            WorldGen.GrowTree(i, j - 1); // j - 1 because GrowTree expects the tile ABOVE the ground

        }
    }
}
