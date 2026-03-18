using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheSludgeMod.Common.GlobalNPCs
{
    public class GuideGlobalNPC : GlobalNPC
    {
        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
            return npc.type == NPCID.Guide;
        }
        public override void AI(NPC npc)
        {
            npc.scale = 1.5f;
            npc.color = Color.Green;
            npc.GravityMultiplier *= 0.03f;
        }
        public override void EmoteBubblePosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects)
        {
            spriteEffects = npc.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            position.X += npc.width * npc.spriteDirection;
        }
        public override void PartyHatPosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects)
        {
            position.Y -= npc.height / 2f;
            position.X += 4 * npc.spriteDirection;
        }
    }
}
