using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Junk;
using TheSludgeMod.Content.Items.Weapons;

namespace TheSludgeMod.Common.GlobalNPCs
{
    public class GlobalItemDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.DemonEye || npc.type== NPCID.DemonEye2)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DemonsEye>(), 10));
            }

            if (npc.type == NPCID.PirateCrossbower)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheRedRobin>(), 10));
            } 

            if (npc.type == NPCID.Plantera) {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PeaShooter>(), 2));
            }

        }
        public override void OnKill(NPC npc)
        {
            if (Main.eclipse && npc.type == 598)
            {
                if (npc.lastInteraction != -1 && Main.projectile[npc.lastInteraction].type == ProjectileID.Chik)
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DefaultItem>(), Main.rand.Next(10, 51));
                }
            }

            if (Main.dayRate > 30 && npc.type == 423)
            {
                if (npc.lastInteraction != -1 && Main.projectile[npc.lastInteraction].type == ProjectileID.SporeCloud)
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<PlaceholderItem>(), Main.rand.Next(10, 51));
                }
            }
        }

    }
}
