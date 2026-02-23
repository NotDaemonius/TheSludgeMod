using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items;

namespace TheSludgeMod.Common.GlobalTiles
{
    public class TileDrops : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (type == TileID.Dirt && !fail && Main.rand.NextBool(2000))
            {
                Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(i, j),  i * 16,  j * 16,  16,  16, ModContent.ItemType<SpeckOfDust>());
            }
        }
    }
}