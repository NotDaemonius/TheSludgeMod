using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.GlobalNPCs;

public class BossSpawnSuppressionGlobalNPC : GlobalNPC
{
    private static readonly bool CalamityLoaded = ModLoader.TryGetMod("CalamityMod", out _);

    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
        if (CalamityLoaded) return;
        if (ModContent.GetInstance<ClientConfig>().DisableBossSpawnSuppression) return;
        if (!IsBossActive()) return;
        maxSpawns = 0;
    }

    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        if (CalamityLoaded) return;
        if (ModContent.GetInstance<ClientConfig>().DisableBossSpawnSuppression) return;
        if (!IsBossActive()) return;
        pool.Clear();
    }

    private static bool IsBossActive()
    {
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            if (!npc.active) continue;
            if (npc.type == NPCID.MartianSaucer || npc.type == NPCID.MartianSaucerCannon || npc.type == NPCID.MartianSaucerCore || npc.type == NPCID.MartianSaucerTurret) continue;
            if (npc.boss || IsBossException(npc.type)) return true;
        }

        return false;
    }

    private static bool IsBossException(int type) => type == NPCID.EaterofWorldsBody || type == NPCID.EaterofWorldsTail || type == NPCID.TheDestroyerBody || type == NPCID.TheDestroyerTail;
}