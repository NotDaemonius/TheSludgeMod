using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.GlobalNPCs;

public class SkeletronHitboxes : GlobalNPC
{
    public override void SetDefaults(NPC npc)
    {
        if (npc.type == NPCID.SkeletronHead)
        {
            npc.width = 160;
            npc.height = 204;
        }
    }
}
