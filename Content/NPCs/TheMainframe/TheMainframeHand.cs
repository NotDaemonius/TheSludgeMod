using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.NPCs.TheMainframe
{
    public class TheMainframeHand : ModNPC
    {
        public NPC Parent
        {
            get => Main.npc[(int)NPC.ai[0]];
        }

        public float PositionOffset
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 7;
            NPC.DiscourageDespawn(1000);
            NPC.dontTakeDamage = true;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
            
        }

        public override void AI()
        {
            Main.NewText("HI");
            if (Despawn())
            {
                return;
            }

            NPC.Center = Parent.Center + new Vector2(100, 0) * PositionOffset;
        }
        private bool Despawn()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient || Parent == null)
            {
                // * Not spawned by the boss body (didn't assign a position and parent) or
                // * Parent isn't active or
                // * Parent isn't the body
                // => invalid, kill itself without dropping any items
                NPC.active = false;
                NPC.life = 0;
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                return true;
            }
            return false;
        }
    }
}
