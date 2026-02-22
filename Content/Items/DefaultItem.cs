using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items
{
	public class DefaultItem : ModItem
	{
		
		public override void SetStaticDefaults() 
		{
			Item.ResearchUnlockCount = 23;
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 0;
			Item.rare = ItemRarityID.White;
			Item.maxStack = 9999;
			Item.holdStyle = ItemHoldStyleID.HoldFront;
		}
        // How far (in pixels) NPCs start getting repelled
        private const float RepelRadius = 300f;
        // How strongly NPCs are pushed away
        private const float RepelForce = 8f;
        public override void HoldItem(Player player)
        {
            // Only run repel logic on the owning client (or server in MP)
            if (player.whoAmI != Main.myPlayer && Main.netMode != Terraria.ID.NetmodeID.Server)
                return;

            Vector2 playerCenter = player.Center;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (!npc.active)
                    continue;

                // Skip friendly town NPCs if desired — remove this check to repel everyone

                float distance = Vector2.Distance(playerCenter, npc.Center);

                if (distance < RepelRadius && distance > 0f)
                {
                    // Direction pointing away from the player
                    Vector2 repelDirection = Vector2.Normalize(npc.Center - playerCenter);

                    // Scale force: stronger when closer
                    float strength = RepelForce * (1f - distance / RepelRadius);

                    npc.velocity += repelDirection * strength;

                    // Clamp velocity so NPCs don't fly off at absurd speeds
                    float maxSpeed = 16f;
                    if (npc.velocity.Length() > maxSpeed)
                        npc.velocity = Vector2.Normalize(npc.velocity) * maxSpeed;
                }
            }
        }

	}
}
