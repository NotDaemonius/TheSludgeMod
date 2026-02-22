using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.GlobalItems
{
    public class ImSuchAChud : GlobalItem
    {
        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.LesserHealingPotion)
            {
                player.Heal(25);
            }
        }
    }
}
