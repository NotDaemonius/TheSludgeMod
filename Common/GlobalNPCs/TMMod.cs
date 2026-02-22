using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using TheSludgeMod.Content.Items;

namespace TheSludgeMod
{
    public class TMMod : GlobalNPC
    {
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (npc.type == NPCID.TravellingMerchant)
            {
                if (NPC.downedBoss1)
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i] == null || items[i].type == ItemID.None)
                        {
                            items[i] = new Item(ModContent.ItemType<XShotBlaster>());
                            break; 
                        }
                    }
                }
            }
        }
    }
}