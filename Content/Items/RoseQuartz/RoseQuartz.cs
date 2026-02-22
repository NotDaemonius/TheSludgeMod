using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz
{
    public class RoseQuartz : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Sapphire);
            Item.createTile = ModContent.TileType<RoseQuartzTile>();
        }
    }
}
