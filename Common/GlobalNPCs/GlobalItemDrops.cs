using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Consumables;
using TheSludgeMod.Content.Items.Junk;
using TheSludgeMod.Content.Items.Weapons;

namespace TheSludgeMod.Common.GlobalNPCs
{
    public class SkeletronDefeatedCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss3;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => "Drops after Skeletron has been defeated";
    }
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

            if (npc.type == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Scythophant>(), 2));
            }

            if (npc.type == NPCID.Golem)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Autohammer, 1));
            }

            if (npc.type == NPCID.Clown)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Scythophant>(), 10));
            }

            if (npc.type == NPCID.UmbrellaSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SlimyUmbrella>(), 50));
            }

            if (npc.type == NPCID.Skeleton)
            {
                var skeletronDefeated = new LeadingConditionRule(new SkeletronDefeatedCondition());
                skeletronDefeated.OnSuccess(ItemDropRule.Common(ItemID.Bone, 1, 1, 2));
                npcLoot.Add(skeletronDefeated);
            }

            if (npc.type == NPCID.Frog)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.FrogLeg, 200));
            }

            if (npc.type == NPCID.GoldFrog)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.FrogLeg, 4));
            }

            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.TheAxe, 1));
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
