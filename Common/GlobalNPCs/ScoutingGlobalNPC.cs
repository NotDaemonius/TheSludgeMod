using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Common.GlobalNPCs
{
    public class ScoutingGlobalNPC : GlobalNPC
    {
        private static readonly int[] BoundNPCs =
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

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (!player.HasBuff(ModContent.BuffType<ScoutingBuff>()))
                return;

            foreach (int id in BoundNPCs)
            {
                // Temporarily not applicable here — spawn rate editing is global.
                // See EditSpawnPool for per-NPC weight adjustments.
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (!spawnInfo.Player.HasBuff(ModContent.BuffType<ScoutingBuff>()))
                return;

            foreach (int id in BoundNPCs)
            {
                if (pool.TryGetValue(id, out float currentWeight))
                    pool[id] = currentWeight * 10f;
            }
        }
    }
}