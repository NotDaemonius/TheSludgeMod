using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Common.GlobalNPCs
{
    public class PainterNameOverride : GlobalNPC
    {
        public override void ModifyNPCNameList(NPC npc, List<string> nameList)
        {
            // Check if the NPC being spawned is the Painter
            if (npc.type == NPCID.Painter)
            {
                // Clear the existing vanilla name list entirely
                nameList.Clear();

                // Add your custom names here
                nameList.Add("Painter");
            }
        }
    }
}