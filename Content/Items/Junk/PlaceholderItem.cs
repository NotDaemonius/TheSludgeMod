using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using TheSludgeMod.Content.Projectiles;

namespace TheSludgeMod.Content.Items.Junk
{
	public class PlaceholderItem : ModItem
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

        private const float RepelRadius = 300f;
        private const float RepelForce = -2f;
        public override void HoldItem(Player player)
        {
            if (player.whoAmI != Main.myPlayer && Main.netMode != Terraria.ID.NetmodeID.Server)
            return;

            Vector2 playerCenter = player.Center;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (!npc.active)
                continue;

                float distance = Vector2.Distance(playerCenter, npc.Center);

                if (distance < RepelRadius && distance > 0f)
                {
                    Vector2 repelDirection = Vector2.Normalize(npc.Center - playerCenter);
                    float strength = RepelForce * (1f - distance / RepelRadius);
                    npc.velocity += repelDirection * strength;
                    float maxSpeed = 16f;

                    if (npc.velocity.Length() > maxSpeed)
                    npc.velocity = Vector2.Normalize(npc.velocity) * maxSpeed;
                }
            }
        }

	}
}
