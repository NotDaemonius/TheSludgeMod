using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using TheSludgeMod.Content.Items.Junk;

namespace TheSludgeMod.Common.GlobalTiles;

public class PotDrops : GlobalTile
{
    public override void Drop(int i, int j, int type)
    {
        if (type == TileID.Pots)
        {
            if (Main.rand.NextBool(1000))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), new Vector2(i * 16, j * 16), ModContent.ItemType<DodgyBuilder>());
            }

            if (Main.rand.NextBool(1000))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), new Vector2(i * 16, j * 16), ModContent.ItemType<SpeckOfDust>());
            }
        }
    }
}