using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.GlobalNPCs;

public class PainterNameOverride : GlobalNPC
{
    public override void ModifyNPCNameList(NPC npc, List<string> nameList)
    {
        if (npc.type == NPCID.Painter)
        {
            nameList.Clear();
            nameList.Add("Painter");
        }
    }
}