using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Common.GlobalNPCs
{
    public class ScoutingGlobalNPC : GlobalNPC
    {
        private static readonly int[] BoundTownNPCs =
        {
            NPCID.BoundGoblin,
            NPCID.BoundWizard,
            NPCID.BoundMechanic,
            NPCID.SleepingAngler,
            NPCID.GolferRescue,
            NPCID.BartenderUnconscious,
            NPCID.WebbedStylist,
            NPCID.SkeletonMerchant
        };

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.HasBuff(ModContent.BuffType<ScoutingBuff>()))
            {
                foreach (int npcID in BoundTownNPCs)
                {
                    if (pool.ContainsKey(npcID))
                    {
                        pool[npcID] *= 10f;
                    }
                }
            }
        }
    }
}